// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoHeapSnapshotViewer {
    public static class Utils {
        public static void Assert (bool condition, [CallerArgumentExpression("condition")]string? message = null) {
            if (condition)
                return;

            throw new Exception($"Assertion failed: {message}");
        }
        private static ThreadLocal<byte[]> LEBBuffer = new ThreadLocal<byte[]>(() => new byte[10]);

        public unsafe static T ReadValue<T> (ref ReadOnlySpan<byte> source)
            where T : unmanaged {
            var result = MemoryMarshal.Read<T>(source);
            source = source.Slice(sizeof(T));
            return result;
        }

        public unsafe static bool ReadLEBUInt (
            ref ReadOnlySpan<byte> source, out ulong result
        ) {
            result = 0;
            int bytesRead = 0, shift = 0;

            for (int i = 0; i < source.Length; i++) {
                var b = source[i];
                var shifted = (ulong)(b & 0x7F) << shift;
                result |= shifted;

                if ((b & 0x80) == 0) {
                    bytesRead = i + 1;
                    source = source.Slice(bytesRead);
                    return true;
                }

                shift += 7;
            }

            return false;
        }
        
        public unsafe static bool ReadLEBInt (
            ref ReadOnlySpan<byte> source, out long result
        ) {
            int shift = 0, bytesRead = 0;
            byte b = 0;

            result = 0;

            for (int i = 0; i < source.Length; i++) {
                b = source[i];
                var shifted = (long)(b & 0x7F) << shift;
                result |= shifted;
                shift += 7;

                if ((b & 0x80) == 0) {
                    bytesRead = i + 1;
                    if ((b & 0x40) != 0)
                        result |= (((long)-1) << shift);
                    source = source.Slice(bytesRead);
                    return true;
                }
            }

            return false;
        }

        public static ReadOnlySpan<byte> SlicePString (ref ReadOnlySpan<byte> source) {
            if (!ReadLEBUInt(ref source, out var length))
                throw new EndOfStreamException();
            var result = source.Slice(0, (int)length);
            source = source.Slice((int)length);
            return result;
        }

        public static string DecodePString (ref ReadOnlySpan<byte> source) {
            var bytes = SlicePString(ref source);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
