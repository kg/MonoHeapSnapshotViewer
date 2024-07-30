// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MonoHeapSnapshotViewer {
    public partial class MainWindow : Form {
        public Snapshot? Snapshot;
        private List<int> TypeIndices = new();
        private List<int> InstanceIndices = new();

        public int TypeSortColumn = 0, InstanceSortColumn = 0;
        public int TypeSortDirection = 1, InstanceSortDirection = 1;

        public MainWindow () {
            InitializeComponent();
        }

        private async void openToolStripMenuItem_Click (object sender, EventArgs e) {
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            Snapshot = null;
            Refresh();
            Snapshot = new Snapshot(OpenDialog.FileName);
            await Snapshot.Initializing;
            Refresh();
        }

        public override void Refresh () {
            base.Refresh();
            if (Snapshot == null) {
                InstancesGrid.Enabled = TypesGrid.Enabled = false;
                InstancesGrid.RowCount = TypesGrid.RowCount = 0;
                return;
            }
            RefreshTypes();
            RefreshInstances();
        }

        private int CompareTypes (int lhs, int rhs) {
            ref var cl = ref Snapshot.Classes[lhs];
            ref var cr = ref Snapshot.Classes[rhs];

            switch (TypeSortColumn) {
                case 0:
                    return cl.GetFullName(Snapshot).CompareTo(cr.GetFullName(Snapshot)) * TypeSortDirection;
                case 1:
                    return cl.Count.CompareTo(cr.Count) * TypeSortDirection;
                case 2:
                    return cl.ShallowSizeSum.CompareTo(cr.ShallowSizeSum) * TypeSortDirection;
                case 3:
                    return cl.Depth2SizeSum.CompareTo(cr.Depth2SizeSum) * TypeSortDirection;
                case 4:
                    return cl.ReachableSizeSum.CompareTo(cr.ReachableSizeSum) * TypeSortDirection;
                default:
                    throw new NotImplementedException();
            }
        }

        private void RefreshTypes () {
            string? filter = TypeFilter.Text.Trim();
            if (filter.Length == 0)
                filter = null;

            TypeIndices.Clear();
            for (int i = 0; i < Snapshot.Classes.Length; i++) {
                if (Snapshot.Classes[i].Count == 0)
                    continue;

                ref var klass = ref Snapshot.Classes[i];

                if ((filter != null) && !klass.GetFullName(Snapshot).Contains(filter, StringComparison.OrdinalIgnoreCase))
                    continue;

                TypeIndices.Add(i);
            }

            TypeIndices.Sort(CompareTypes);
            TypesGrid.RowCount = TypeIndices.Count;
            TypesGrid.Enabled = true;
        }

        private int CompareInstances (int lhs, int rhs) {
            ref var cl = ref Snapshot.Objects[lhs];
            ref var cr = ref Snapshot.Objects[rhs];

            switch (InstanceSortColumn) {
                case 0:
                    return cl.DirectRootCount.CompareTo(cr.DirectRootCount) * InstanceSortDirection;
                case 1:
                    return cl.Object.CompareTo(cr.Object) * InstanceSortDirection;
                case 2:
                    return cl.ShallowSize.CompareTo(cr.ShallowSize) * InstanceSortDirection;
                case 3:
                    return cl.Depth2Size.CompareTo(cr.Depth2Size) * InstanceSortDirection;
                case 4:
                    return cl.GetReachableSize(Snapshot).CompareTo(cr.GetReachableSize(Snapshot)) * InstanceSortDirection;
                default:
                    throw new NotImplementedException();
            }
        }

        private void RefreshInstances () {
            InstancesGrid.RowCount = 0;
            InstancesGrid.Enabled = false;

            var types = new HashSet<uint>();
            for (int c = 0; c < TypesGrid.SelectedCells.Count; c++)
                types.Add(Snapshot.Classes[TypeIndices[TypesGrid.SelectedCells[c].RowIndex]].Klass);

            InstanceIndices.Clear();
            for (int i = 0; i < Snapshot.Objects.Length; i++) {
                ref var obj = ref Snapshot.Objects[i];
                if (!types.Contains(obj.Klass))
                    continue;
                InstanceIndices.Add(i);
            }

            InstanceIndices.Sort(CompareInstances);

            InstancesGrid.RowCount = InstanceIndices.Count;
            InstancesGrid.Enabled = true;
        }

        private void TypesGrid_CellValueNeeded (object sender, DataGridViewCellValueEventArgs e) {
            if (Snapshot == null)
                return;

            var index = TypeIndices[e.RowIndex];
            ref var klass = ref Snapshot.Classes[index];

            switch (e.ColumnIndex) {
                case 0:
                    e.Value = klass.GetFullName(Snapshot);
                    break;
                case 1:
                    e.Value = klass.Count.ToString();
                    break;
                case 2:
                    e.Value = klass.ShallowSizeSum.ToString();
                    break;
                case 3:
                    e.Value = klass.Depth2SizeSum.ToString();
                    break;
                case 4:
                    e.Value = klass.ReachableSizeSum.ToString();
                    break;
            }
        }

        private static readonly object _False = false, _True = true;

        private void InstancesGrid_CellValueNeeded (object sender, DataGridViewCellValueEventArgs e) {
            if (Snapshot == null)
                return;

            var index = InstanceIndices[e.RowIndex];
            ref var obj = ref Snapshot.Objects[index];

            switch (e.ColumnIndex) {
                case 0:
                    e.Value = obj.DirectRootCount > 0 ? _True : _False;
                    break;
                case 1:
                    e.Value = obj.Object.ToString("X8");
                    break;
                case 2:
                    e.Value = obj.ShallowSize.ToString();
                    break;
                case 3:
                    e.Value = obj.Depth2Size.ToString();
                    break;
                case 4:
                    e.Value = obj.GetReachableSize(Snapshot);
                    break;
            }
        }

        private void TypesGrid_SelectionChanged (object sender, EventArgs e) {
            RefreshInstances();
        }

        private void TypesGrid_ColumnHeaderMouseClick (object sender, DataGridViewCellMouseEventArgs e) {
            TypesGrid.Columns[TypeSortColumn].HeaderCell.SortGlyphDirection = SortOrder.None;
            TypesGrid.RowCount = 0;
            if (TypeSortColumn == e.ColumnIndex)
                TypeSortDirection = -TypeSortDirection;
            else
                TypeSortColumn = e.ColumnIndex;
            TypesGrid.Columns[TypeSortColumn].HeaderCell.SortGlyphDirection = TypeSortDirection > 0 ? SortOrder.Ascending : SortOrder.Descending;
            RefreshTypes();
        }

        private void InstancesGrid_ColumnHeaderMouseClick (object sender, DataGridViewCellMouseEventArgs e) {
            InstancesGrid.Columns[InstanceSortColumn].HeaderCell.SortGlyphDirection = SortOrder.None;
            InstancesGrid.RowCount = 0;
            if (InstanceSortColumn == e.ColumnIndex)
                InstanceSortDirection = -InstanceSortDirection;
            else
                InstanceSortColumn = e.ColumnIndex;
            InstancesGrid.Columns[InstanceSortColumn].HeaderCell.SortGlyphDirection = InstanceSortDirection > 0 ? SortOrder.Ascending : SortOrder.Descending;
            RefreshInstances();
        }

        private void TypeFilter_TextChanged (object sender, EventArgs e) {
            RefreshTypes();
        }

        private void countersToolStripMenuItem_Click (object sender, EventArgs e) {
            var window = new CountersWindow(Snapshot);
            window.Show();
        }
    }
}
