namespace MonoHeapSnapshotViewer {
    partial class CountersWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            CountersGrid = new DataGridView();
            NameColumn = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)CountersGrid).BeginInit();
            SuspendLayout();
            // 
            // CountersGrid
            // 
            CountersGrid.AllowUserToAddRows = false;
            CountersGrid.AllowUserToDeleteRows = false;
            CountersGrid.AllowUserToResizeRows = false;
            CountersGrid.BorderStyle = BorderStyle.None;
            CountersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CountersGrid.Columns.AddRange(new DataGridViewColumn[] { NameColumn, Value });
            CountersGrid.Dock = DockStyle.Fill;
            CountersGrid.Location = new Point(0, 0);
            CountersGrid.Name = "CountersGrid";
            CountersGrid.RowHeadersVisible = false;
            CountersGrid.RowHeadersWidth = 62;
            CountersGrid.Size = new Size(878, 544);
            CountersGrid.TabIndex = 0;
            // 
            // Name
            // 
            NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            NameColumn.HeaderText = "Name";
            NameColumn.MinimumWidth = 8;
            NameColumn.Name = "Name";
            NameColumn.ReadOnly = true;
            // 
            // Value
            // 
            Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Value.HeaderText = "Value";
            Value.MinimumWidth = 8;
            Value.Name = "Value";
            Value.ReadOnly = true;
            // 
            // CountersWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 544);
            Controls.Add(CountersGrid);
            Name = "CountersWindow";
            Text = "Counters";
            Shown += CountersWindow_Shown;
            ((System.ComponentModel.ISupportInitialize)CountersGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView CountersGrid;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn Value;
    }
}