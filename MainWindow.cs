// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MonoHeapSnapshotViewer {
    public partial class MainWindow : Form {
        public Snapshot? Snapshot;

        public MainWindow () {
            InitializeComponent();
        }

        private async void openToolStripMenuItem_Click (object sender, EventArgs e) {
            if (OpenDialog.ShowDialog() != DialogResult.OK)
                return;

            TypesGrid.Enabled = false;
            TypesGrid.RowCount = 1;
            Snapshot = new Snapshot(OpenDialog.FileName);
            await Snapshot.Initializing;
            Refresh();
        }

        public override void Refresh () {
            base.Refresh();
            if (Snapshot == null) {
                TypesGrid.RowCount = 1;
                TypesGrid.Enabled = false;
                return;
            }
            TypesGrid.RowCount = Snapshot.Classes.Length;
            TypesGrid.Enabled = true;
        }

        private void TypesGrid_CellValueNeeded (object sender, DataGridViewCellValueEventArgs e) {
            if (Snapshot == null)
                return;

            ref var klass = ref Snapshot.Classes[e.RowIndex];

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
    }
}
