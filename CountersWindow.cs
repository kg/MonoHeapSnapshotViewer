using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoHeapSnapshotViewer {
    public partial class CountersWindow : Form {
        public readonly Snapshot Snapshot;

        public CountersWindow (Snapshot snapshot) {
            Snapshot = snapshot;
            InitializeComponent();
        }

        private void CountersWindow_Shown (object sender, EventArgs e) {
            CountersGrid.RowCount = Snapshot.Counters.Count;
            int i = 0;
            foreach (var kvp in Snapshot.Counters.OrderBy(kvp => kvp.Key)) {
                CountersGrid.Rows[i].Cells[0].Value = kvp.Key;
                CountersGrid.Rows[i].Cells[1].Value = kvp.Value;
                i++;
            }
        }
    }
}
