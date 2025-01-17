﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using static MonoHeapSnapshotViewer.Utils;
using System.Diagnostics.CodeAnalysis;

namespace MonoHeapSnapshotViewer {
    public unsafe class Snapshot : IDisposable {
        public MemoryMappedFile File;
        public MemoryMappedViewAccessor View;
        public byte* Data;

        public Dictionary<string, double> Counters = new ();
        public Dictionary<UInt32, string> StringTable = new() {
            { 0, "" }
        };
        public SnapshotClass[] Classes;
        public SnapshotObject[] Objects;
        public SnapshotRoot[] Roots;
        public Task Initializing;

        public Snapshot (string path) {
            File = MemoryMappedFile.CreateFromFile(path, FileMode.Open);
            View = File.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
            Data = null;
            View.SafeMemoryMappedViewHandle.AcquirePointer(ref Data);
            Initializing = Task.Run(Initialize);
            // Nullable fields are initialized by the async handler
        }

        internal void Initialize () {
            var data = new ReadOnlySpan<byte>(Data, (int)View.Capacity);
            int i = 0;

            // Load counters (there may be more than one chunk)
            while (i < (View.Capacity - 8)) {
                var chunk = new RiffChunk(data, ref i);
                var chunkId = Encoding.UTF8.GetString(chunk.ChunkId);
                switch (chunkId) {
                    case "CNTR":
                        LoadCounters(chunk);
                        break;
                }
            }

            Classes = new SnapshotClass[(int)Counters["snapshot/num-classes"]];
            Objects = new SnapshotObject[(int)Counters["snapshot/num-objects"]];
            Roots = new SnapshotRoot[(int)Counters["snapshot/num-roots"]];
            var refAddresses = new ArraySegment<UInt32>(new UInt32[(int)Counters["snapshot/num-refs"]]);

            int classOffset = 0, objectOffset = 0, rootOffset = 0;

            // First decoding pass
            i = 0;
            while (i < (View.Capacity - 8)) {
                var chunk = new RiffChunk(data, ref i);
                var chunkId = Encoding.UTF8.GetString(chunk.ChunkId);
                switch (chunkId) {
                    case "STBL":
                        DecodeStringTable(chunk);
                        break;
                    case "TYPE":
                        DecodeClasses(chunk, ref classOffset);
                        break;
                    case "OBJH":
                        DecodeObjectHeaders(chunk, ref objectOffset);
                        break;
                    default:
                        Debug.WriteLine($"{chunkId} {chunk.Data.Length}b");
                        break;
                }
            }

            Array.Sort(Classes, 0, classOffset, SnapshotClass.AddressComparer.Instance);
            Array.Sort(Objects, 0, objectOffset, SnapshotObject.AddressComparer.Instance);

            // Ref scanning pass
            i = 0;
            while (i < (View.Capacity - 8)) {
                var chunk = new RiffChunk(data, ref i);
                var chunkId = Encoding.UTF8.GetString(chunk.ChunkId);
                switch (chunkId) {
                    case "REFS":
                        // Determine how many refs total each object has so we can reserve enough space to store them
                        DecodeRefsPass1(chunk);
                        break;
                    case "ROOT":
                        DecodeRootsPass1(chunk, ref rootOffset);
                        break;
                    default:
                        Debug.WriteLine($"{chunkId} {chunk.Data.Length}b");
                        break;
                }
            }

            // Ref assembly pass
            i = 0;
            while (i < (View.Capacity - 8)) {
                var chunk = new RiffChunk(data, ref i);
                var chunkId = Encoding.UTF8.GetString(chunk.ChunkId);
                switch (chunkId) {
                    case "REFS":
                        // Now build lists of refs for each object
                        DecodeRefsPass2(chunk, ref refAddresses);
                        break;
                    default:
                        Debug.WriteLine($"{chunkId} {chunk.Data.Length}b");
                        break;
                }
            }

            BuildSummaryData();
        }

        private void DecodeRootsPass1 (RiffChunk chunk, ref int rootOffset) {
            var data = chunk.Data;
            while (data.Length > 0) {
                Assert(ReadLEBUInt(ref data, out var kind));
                Assert(ReadLEBUInt(ref data, out var rootCount));
                for (uint i = 0; i < rootCount; i++) {
                    ref var root = ref Roots[rootOffset++];
                    root = new SnapshotRoot();
                    root.Address = ReadValue<UInt32>(ref data);
                    root.Object = ReadValue<UInt32>(ref data);
                    ref var obj = ref Object(root.Object);
                    obj.DirectRootCount++;
                }
            }
        }

        private void BuildSummaryData () {
            for (int i = 0; i < Objects.Length; i++) {
                ref var obj = ref Objects[i];
                ref var klass = ref Class(obj.Klass);
                klass.Count += 1;
                klass.ShallowSizeSum += obj.ShallowSize;
            }

            for (int i = 0; i < Objects.Length; i++) {
                ref var obj = ref Objects[i];
                ref var klass = ref Class(obj.Klass);
                UpdateSummaryDataForObject(ref klass, ref obj, 0);
            }

            for (int i = 0; i < Classes.Length; i++) {
                ref var klass = ref Classes[i];
                klass.Depth2HashSet = null;
                klass.ReachableHashSet = null;
            }
        }

        private void UpdateSummaryDataForObject (ref SnapshotClass klass, ref SnapshotObject obj, int depth) {
            UpdateSummaryDataForObjectRefs(ref klass, ref obj, 0);
        }

        private void UpdateSummaryDataForObjectRefs (
            ref SnapshotClass klass, ref SnapshotObject obj, int depth
        ) {
            klass.Depth2HashSet ??= new ();
            klass.ReachableHashSet ??= new ();
            if (depth <= 1) {
                if (!klass.Depth2HashSet.Contains(obj.Object)) {
                    klass.Depth2HashSet.Add(obj.Object);
                    klass.Depth2SizeSum += obj.ShallowSize;
                }
            }

            if (!klass.ReachableHashSet.Contains(obj.Object)) {
                klass.ReachableHashSet.Add(obj.Object);
                klass.ReachableSizeSum += obj.ShallowSize;
            }

            if (depth == 0)
                obj.Depth2Size = obj.ShallowSize;

            if (obj.RefCount < 1)
                return;

            foreach (var r in obj.Refs) {
                ref var refTarget = ref Object(r);

                if (depth == 0)
                    obj.Depth2Size += refTarget.ShallowSize;

                if (!klass.ReachableHashSet.Contains(refTarget.Object))
                    UpdateSummaryDataForObjectRefs(ref klass, ref refTarget, depth + 1);
            }
        }

        private void DecodeStringTable (RiffChunk chunk) {
            var data = chunk.Data;
            while (data.Length > 0) {
                Assert(ReadLEBUInt(ref data, out var index));
                var value = DecodePString(ref data);
                StringTable[(uint)index] = value;
            }
        }

        private void DecodeClasses (RiffChunk chunk, ref int classOffset) {
            var data = chunk.Data;
            while (data.Length > 0) {
                ref var klass = ref Classes[classOffset++];
                klass = new SnapshotClass();
                klass.Klass = ReadValue<UInt32>(ref data);
                klass.ElementKlass = ReadValue<UInt32>(ref data);
                klass.NestingKlass = ReadValue<UInt32>(ref data);
                klass.Assembly = ReadValue<UInt32>(ref data);
                Assert(ReadLEBUInt(ref data, out var rank));
                klass.Rank = (int)rank;
                Assert(ReadLEBUInt(ref data, out var kindName));
                klass.Kind = kindName;
                Assert(ReadLEBUInt(ref data, out var ns));
                klass.Namespace = ns;
                Assert(ReadLEBUInt(ref data, out var name));
                klass.Name = name;
                Assert(ReadLEBUInt(ref data, out var numGps));
                // FIXME: Slices of a single big array for better density
                // ALTERNATELY: On-demand constructed ReadOnlySpan over the mmap'd view
                var gps = new UInt32[numGps];
                for (uint i = 0; i < numGps; i++)
                    gps[i] = ReadValue<UInt32>(ref data);
                klass.GenericParameters = gps;
            }
        }

        private void DecodeObjectHeaders (RiffChunk chunk, ref int objectOffset) {
            var data = chunk.Data;
            while (data.Length > 0) {
                ref var header = ref Objects[objectOffset++];
                header = new SnapshotObject();
                header.Object = ReadValue<UInt32>(ref data);
                header.Klass = ReadValue<UInt32>(ref data);
                Assert(ReadLEBUInt(ref data, out var shallowSize));
                header.ShallowSize = (uint)shallowSize;
            }
        }

        private void DecodeRefsPass1 (RiffChunk chunk) {
            var data = chunk.Data;
            while (data.Length > 0) {
                var obj = ReadValue<UInt32>(ref data);
                ref var header = ref Object(obj);
                Assert(ReadLEBUInt(ref data, out var count));
                header.RefCount += (uint)count;
                // Skip the actual refs, we'll handle them in pass 2
                data = data.Slice((int)count * 4);
            }
        }

        private void DecodeRefsPass2 (RiffChunk chunk, ref ArraySegment<uint> refAddresses) {
            var data = chunk.Data;
            while (data.Length > 0) {
                var obj = ReadValue<UInt32>(ref data);
                ref var header = ref Object(obj);
                Assert(ReadLEBUInt(ref data, out var count));
                int j = header.Refs.Count;
                if (header.Refs.Count == 0) {
                    header.Refs = new ArraySegment<uint>(refAddresses.Array!, refAddresses.Offset, (int)count);
                    refAddresses = new ArraySegment<uint>(refAddresses.Array!, refAddresses.Offset + (int)header.RefCount, refAddresses.Count - (int)header.RefCount);
                } else {
                    header.Refs = new ArraySegment<uint>(header.Refs.Array!, header.Refs.Offset, header.Refs.Count + (int)count);
                }
                for (int i = 0; i < (int)count; i++)
                    header.Refs[j + i] = ReadValue<UInt32>(ref data);
            }
        }

        internal void LoadCounters (RiffChunk chunk) {
            var data = chunk.Data;
            while (data.Length > 0) {
                var name = DecodePString(ref data);
                var value = ReadValue<double>(ref data);
                Counters[name] = value;
            }
        }

        public ref SnapshotClass Class (uint klass) {
            var needle = new SnapshotClass {
                Klass = klass,
            };
            var index = Array.BinarySearch(Classes, needle, SnapshotClass.AddressComparer.Instance);
            if (index < 0)
                throw new Exception($"Class not found: {klass}");
            return ref Classes[index];
        }

        public ref SnapshotObject Object (uint obj) {
            var needle = new SnapshotObject {
                Object = obj,
            };
            var index = Array.BinarySearch(Objects, needle, SnapshotObject.AddressComparer.Instance);
            if (index < 0)
                throw new Exception($"Object not found: {obj}");
            return ref Objects[index];
        }

        public void Dispose () {
            Data = null;
            View.SafeMemoryMappedViewHandle.ReleasePointer();
            View.Dispose();
            File.Dispose();
        }
    }

    public struct Counter {
        public string Name;
        public double Value;
    }

    public ref struct RiffChunk {
        public ReadOnlySpan<byte> ChunkId;
        public ReadOnlySpan<byte> Data;

        public RiffChunk (ReadOnlySpan<byte> data, ref int offset) {
            ChunkId = data.Slice(offset, 4);
            var lengthSpan = data.Slice(offset + 4, 4);
            var length = MemoryMarshal.Read<UInt32>(lengthSpan);
            Data = data.Slice(offset + 8, (int)length);
            offset += ((int)length + 8);
        }
    }

    public struct StringTableKey {
        public UInt32 Index;

        public string Get (Snapshot snapshot) => 
            snapshot.StringTable.TryGetValue(Index, out var value)
                ? value
                : "missing";

        public static implicit operator StringTableKey (uint value) =>
            new StringTableKey { Index = value };

        public static implicit operator StringTableKey (ulong value) =>
            new StringTableKey { Index = (uint)value };

        public override string ToString () => Index.ToString();
        public override int GetHashCode () => Index.GetHashCode();
        public bool Equals (StringTableKey rhs) =>
            Index == rhs.Index;
        public override bool Equals ([NotNullWhen(true)] object? obj) {
            if (obj is StringTableKey stk)
                return Equals(stk);
            else
                return false;
        }
    }

    public struct SnapshotClass {
        public class AddressComparer : IComparer<SnapshotClass> {
            public static readonly AddressComparer Instance = new();

            public int Compare (SnapshotClass x, SnapshotClass y) =>
                x.Klass.CompareTo(y.Klass);
        }

        public UInt32 Klass, ElementKlass, NestingKlass, Assembly;
        public int Rank;
        public StringTableKey Kind, Namespace, Name;
        public ArraySegment<UInt32> GenericParameters;

        public HashSet<UInt32>? Depth2HashSet, ReachableHashSet;

        public uint Count, ShallowSizeSum, Depth2SizeSum, ReachableSizeSum;

        private string? _FullName;

        // FIXME: Use a stringbuilder
        public string GetFullName (Snapshot snapshot) {
            if (_FullName != null)
                return _FullName;

            if (Namespace.Index > 0)
                _FullName = $"{Namespace.Get(snapshot)}.{Name.Get(snapshot)}";
            else
                _FullName = Name.Get(snapshot);

            if (NestingKlass > 0)
                _FullName = $"{snapshot.Class(NestingKlass).GetFullName(snapshot)}.{_FullName}";

            // FIXME: Optimize this
            if (GenericParameters.Count > 0)
                _FullName += $"<{string.Join(", ", from gp in GenericParameters select snapshot.Class(gp).GetFullName(snapshot))}>";

            if (Rank > 0)
                _FullName += "[]";

            return _FullName;
        }
    }

    public struct SnapshotObject {
        public class AddressComparer : IComparer<SnapshotObject> {
            public static readonly AddressComparer Instance = new();

            public int Compare (SnapshotObject x, SnapshotObject y) =>
                x.Object.CompareTo(y.Object);
        }

        public UInt32 Object, Klass;
        public uint ShallowSize, RefCount, Depth2Size, DirectRootCount;
        public ArraySegment<UInt32> Refs;

        private uint _ReachableSize;

        public uint GetReachableSize (Snapshot snapshot) {
            if (_ReachableSize > 0)
                return _ReachableSize;

            if (Refs.Count <= 0)
                return ShallowSize;

            var set = new HashSet<UInt32> { Object };
            var result = ShallowSize;
            if (RefCount > 0)
                AccumulateRefs(snapshot, set, Refs, ref result);
            _ReachableSize = result;
            return result;
        }

        private void AccumulateRefs (Snapshot snapshot, HashSet<UInt32> set, ArraySegment<uint> refs, ref uint result) {
            foreach (var r in refs) {
                if (set.Contains(r))
                    continue;

                ref var refTarget = ref snapshot.Object(r);
                set.Add(r);
                result += refTarget.ShallowSize;

                if (refTarget.RefCount > 0)
                    AccumulateRefs(snapshot, set, refTarget.Refs, ref result);
            }
        }
    }

    public struct SnapshotRoot {
        public StringTableKey Kind;
        public UInt32 Address, Object;
    }
}
