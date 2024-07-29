namespace MonoHeapSnapshotViewer {
    partial class MainWindow {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            MenuBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            OpenDialog = new OpenFileDialog();
            splitContainer1 = new SplitContainer();
            TypesGrid = new DataGridView();
            TypeName = new DataGridViewTextBoxColumn();
            ObjectCount = new DataGridViewTextBoxColumn();
            ShallowSize = new DataGridViewTextBoxColumn();
            Depth2Size = new DataGridViewTextBoxColumn();
            SubtreeSize = new DataGridViewTextBoxColumn();
            InstancesGrid = new DataGridView();
            Root = new DataGridViewCheckBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            MenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TypesGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InstancesGrid).BeginInit();
            SuspendLayout();
            // 
            // MenuBar
            // 
            MenuBar.ImageScalingSize = new Size(24, 24);
            MenuBar.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            MenuBar.Location = new Point(0, 0);
            MenuBar.Name = "MenuBar";
            MenuBar.Size = new Size(1258, 33);
            MenuBar.TabIndex = 1;
            MenuBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(170, 34);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // OpenDialog
            // 
            OpenDialog.DefaultExt = "mono-heap";
            OpenDialog.Filter = "Mono Heap Snapshots|*.mono-heap";
            OpenDialog.ShowHiddenFiles = true;
            OpenDialog.Title = "Open Snapshot";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(TypesGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(InstancesGrid);
            splitContainer1.Size = new Size(1258, 911);
            splitContainer1.SplitterDistance = 604;
            splitContainer1.TabIndex = 2;
            // 
            // TypesGrid
            // 
            TypesGrid.AllowUserToAddRows = false;
            TypesGrid.AllowUserToDeleteRows = false;
            TypesGrid.AllowUserToOrderColumns = true;
            TypesGrid.AllowUserToResizeRows = false;
            TypesGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            TypesGrid.BorderStyle = BorderStyle.None;
            TypesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TypesGrid.Columns.AddRange(new DataGridViewColumn[] { TypeName, ObjectCount, ShallowSize, Depth2Size, SubtreeSize });
            TypesGrid.Dock = DockStyle.Fill;
            TypesGrid.Location = new Point(0, 0);
            TypesGrid.Name = "TypesGrid";
            TypesGrid.RowHeadersVisible = false;
            TypesGrid.RowHeadersWidth = 62;
            TypesGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            TypesGrid.Size = new Size(1258, 604);
            TypesGrid.TabIndex = 1;
            TypesGrid.VirtualMode = true;
            TypesGrid.CellValueNeeded += TypesGrid_CellValueNeeded;
            TypesGrid.ColumnHeaderMouseClick += TypesGrid_ColumnHeaderMouseClick;
            TypesGrid.SelectionChanged += TypesGrid_SelectionChanged;
            // 
            // TypeName
            // 
            TypeName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TypeName.HeaderText = "Type Name";
            TypeName.MinimumWidth = 150;
            TypeName.Name = "TypeName";
            // 
            // ObjectCount
            // 
            ObjectCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ObjectCount.HeaderText = "Count";
            ObjectCount.MinimumWidth = 8;
            ObjectCount.Name = "ObjectCount";
            ObjectCount.Width = 96;
            // 
            // ShallowSize
            // 
            ShallowSize.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ShallowSize.HeaderText = "Shallow Size";
            ShallowSize.MinimumWidth = 8;
            ShallowSize.Name = "ShallowSize";
            ShallowSize.Width = 145;
            // 
            // Depth2Size
            // 
            Depth2Size.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Depth2Size.HeaderText = "Depth 2 Size";
            Depth2Size.MinimumWidth = 8;
            Depth2Size.Name = "Depth2Size";
            Depth2Size.Width = 148;
            // 
            // SubtreeSize
            // 
            SubtreeSize.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            SubtreeSize.HeaderText = "Subtree Size";
            SubtreeSize.MinimumWidth = 8;
            SubtreeSize.Name = "SubtreeSize";
            SubtreeSize.Width = 145;
            // 
            // InstancesGrid
            // 
            InstancesGrid.AllowUserToAddRows = false;
            InstancesGrid.AllowUserToDeleteRows = false;
            InstancesGrid.AllowUserToOrderColumns = true;
            InstancesGrid.AllowUserToResizeRows = false;
            InstancesGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            InstancesGrid.BorderStyle = BorderStyle.None;
            InstancesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InstancesGrid.Columns.AddRange(new DataGridViewColumn[] { Root, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            InstancesGrid.Dock = DockStyle.Fill;
            InstancesGrid.Location = new Point(0, 0);
            InstancesGrid.Name = "InstancesGrid";
            InstancesGrid.RowHeadersVisible = false;
            InstancesGrid.RowHeadersWidth = 62;
            InstancesGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            InstancesGrid.Size = new Size(1258, 303);
            InstancesGrid.TabIndex = 2;
            InstancesGrid.VirtualMode = true;
            InstancesGrid.CellValueNeeded += InstancesGrid_CellValueNeeded;
            // 
            // Root
            // 
            Root.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Root.HeaderText = "Root";
            Root.MinimumWidth = 8;
            Root.Name = "Root";
            Root.ReadOnly = true;
            Root.Width = 56;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn1.HeaderText = "Address";
            dataGridViewTextBoxColumn1.MinimumWidth = 150;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewTextBoxColumn3.HeaderText = "Shallow Size";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 145;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewTextBoxColumn4.HeaderText = "Depth 2 Size";
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 148;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewTextBoxColumn5.HeaderText = "Subtree Size";
            dataGridViewTextBoxColumn5.MinimumWidth = 8;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 145;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 944);
            Controls.Add(splitContainer1);
            Controls.Add(MenuBar);
            MainMenuStrip = MenuBar;
            Name = "MainWindow";
            Text = "Heap Snapshot Viewer";
            MenuBar.ResumeLayout(false);
            MenuBar.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TypesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)InstancesGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip MenuBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private OpenFileDialog OpenDialog;
        private SplitContainer splitContainer1;
        private DataGridView TypesGrid;
        private DataGridViewTextBoxColumn TypeName;
        private DataGridViewTextBoxColumn ObjectCount;
        private DataGridViewTextBoxColumn ShallowSize;
        private DataGridViewTextBoxColumn Depth2Size;
        private DataGridViewTextBoxColumn SubtreeSize;
        private DataGridView InstancesGrid;
        private DataGridViewCheckBoxColumn Root;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
