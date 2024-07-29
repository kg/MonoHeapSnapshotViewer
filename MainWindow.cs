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
            TypesGrid.RowCount = TypeIndices.Count;
            TypesGrid.Enabled = true;
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
                    return cl.SubtreeSizeSum.CompareTo(cr.SubtreeSizeSum) * TypeSortDirection;
                default: 
                    throw new NotImplementedException();
            }
        }

        private void RefreshTypes () {
            TypeIndices.Clear();
            for (int i = 0; i < Snapshot.Classes.Length; i++)
                if (Snapshot.Classes[i].Count > 0)
                    TypeIndices.Add(i);

            TypeIndices.Sort(CompareTypes);
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
                    e.Value = klass.Count;
                    break;
                case 2:
                    e.Value = klass.ShallowSizeSum;
                    break;
                case 3:
                    e.Value = klass.Depth2SizeSum;
                    break;
                case 4:
                    e.Value = klass.SubtreeSizeSum;
                    break;
            }
        }

        private void InstancesGrid_CellValueNeeded (object sender, DataGridViewCellValueEventArgs e) {
            if (Snapshot == null)
                return;

            var index = InstanceIndices[e.RowIndex];
            ref var obj = ref Snapshot.Objects[index];

            switch (e.ColumnIndex) {
                case 0:
                    e.Value = obj.DirectRootCount > 0;
                    break;
                case 1:
                    e.Value = obj.Object;
                    break;
                case 2:
                    e.Value = obj.ShallowSize;
                    break;
                case 3:
                    e.Value = obj.Depth2Size;
                    break;
                case 4:
                    e.Value = obj.GetSubtreeSize(Snapshot);
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
            TypesGrid.RowCount = TypeIndices.Count;
        }
    }
}
