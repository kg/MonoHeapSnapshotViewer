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
            viewToolStripMenuItem = new ToolStripMenuItem();
            gCHandlesToolStripMenuItem = new ToolStripMenuItem();
            jSObjectsToolStripMenuItem = new ToolStripMenuItem();
            jSCSHandlesToolStripMenuItem = new ToolStripMenuItem();
            countersToolStripMenuItem = new ToolStripMenuItem();
            OpenDialog = new OpenFileDialog();
            splitContainer1 = new SplitContainer();
            TypeFilter = new TextBox();
            TypesGrid = new DataGridView();
            InstancesGrid = new DataGridView();
            TypeName = new DataGridViewTextBoxColumn();
            ObjectCount = new DataGridViewTextBoxColumn();
            ShallowSize = new DataGridViewTextBoxColumn();
            Depth2Size = new DataGridViewTextBoxColumn();
            ReachableSize = new DataGridViewTextBoxColumn();
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
            MenuBar.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem });
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
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gCHandlesToolStripMenuItem, jSObjectsToolStripMenuItem, jSCSHandlesToolStripMenuItem, countersToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(65, 29);
            viewToolStripMenuItem.Text = "&View";
            // 
            // gCHandlesToolStripMenuItem
            // 
            gCHandlesToolStripMenuItem.Enabled = false;
            gCHandlesToolStripMenuItem.Name = "gCHandlesToolStripMenuItem";
            gCHandlesToolStripMenuItem.Size = new Size(270, 34);
            gCHandlesToolStripMenuItem.Text = "&GC Handles";
            // 
            // jSObjectsToolStripMenuItem
            // 
            jSObjectsToolStripMenuItem.Enabled = false;
            jSObjectsToolStripMenuItem.Name = "jSObjectsToolStripMenuItem";
            jSObjectsToolStripMenuItem.Size = new Size(270, 34);
            jSObjectsToolStripMenuItem.Text = "&JS Objects";
            // 
            // jSCSHandlesToolStripMenuItem
            // 
            jSCSHandlesToolStripMenuItem.Enabled = false;
            jSCSHandlesToolStripMenuItem.Name = "jSCSHandlesToolStripMenuItem";
            jSCSHandlesToolStripMenuItem.Size = new Size(270, 34);
            jSCSHandlesToolStripMenuItem.Text = "JS->CS &Handles";
            // 
            // countersToolStripMenuItem
            // 
            countersToolStripMenuItem.Name = "countersToolStripMenuItem";
            countersToolStripMenuItem.Size = new Size(270, 34);
            countersToolStripMenuItem.Text = "&Counters";
            countersToolStripMenuItem.Click += countersToolStripMenuItem_Click;
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
            splitContainer1.Panel1.Controls.Add(TypeFilter);
            splitContainer1.Panel1.Controls.Add(TypesGrid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(InstancesGrid);
            splitContainer1.Size = new Size(1258, 911);
            splitContainer1.SplitterDistance = 604;
            splitContainer1.TabIndex = 2;
            // 
            // TypeFilter
            // 
            TypeFilter.Dock = DockStyle.Top;
            TypeFilter.Location = new Point(0, 0);
            TypeFilter.Name = "TypeFilter";
            TypeFilter.Size = new Size(1258, 31);
            TypeFilter.TabIndex = 2;
            TypeFilter.TextChanged += TypeFilter_TextChanged;
            // 
            // TypesGrid
            // 
            TypesGrid.AllowUserToAddRows = false;
            TypesGrid.AllowUserToDeleteRows = false;
            TypesGrid.AllowUserToOrderColumns = true;
            TypesGrid.AllowUserToResizeRows = false;
            TypesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TypesGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            TypesGrid.BorderStyle = BorderStyle.None;
            TypesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TypesGrid.Columns.AddRange(new DataGridViewColumn[] { TypeName, ObjectCount, ShallowSize, Depth2Size, ReachableSize });
            TypesGrid.Location = new Point(0, 33);
            TypesGrid.Name = "TypesGrid";
            TypesGrid.RowHeadersVisible = false;
            TypesGrid.RowHeadersWidth = 62;
            TypesGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            TypesGrid.Size = new Size(1258, 571);
            TypesGrid.TabIndex = 1;
            TypesGrid.VirtualMode = true;
            TypesGrid.CellValueNeeded += TypesGrid_CellValueNeeded;
            TypesGrid.ColumnHeaderMouseClick += TypesGrid_ColumnHeaderMouseClick;
            TypesGrid.SelectionChanged += TypesGrid_SelectionChanged;
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
            InstancesGrid.ColumnHeaderMouseClick += InstancesGrid_ColumnHeaderMouseClick;
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
            ShallowSize.HeaderText = "Shallow";
            ShallowSize.MinimumWidth = 8;
            ShallowSize.Name = "ShallowSize";
            ShallowSize.Width = 109;
            // 
            // Depth2Size
            // 
            Depth2Size.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Depth2Size.HeaderText = "Depth 2";
            Depth2Size.MinimumWidth = 8;
            Depth2Size.Name = "Depth2Size";
            Depth2Size.Width = 112;
            // 
            // ReachableSize
            // 
            ReachableSize.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ReachableSize.HeaderText = "Reachable";
            ReachableSize.MinimumWidth = 8;
            ReachableSize.Name = "ReachableSize";
            ReachableSize.Width = 127;
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
            dataGridViewTextBoxColumn3.HeaderText = "Shallow";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 109;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewTextBoxColumn4.HeaderText = "Depth 2";
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 112;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewTextBoxColumn5.HeaderText = "Reachable";
            dataGridViewTextBoxColumn5.MinimumWidth = 8;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 127;
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
            splitContainer1.Panel1.PerformLayout();
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
        private DataGridView InstancesGrid;
        private TextBox TypeFilter;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem gCHandlesToolStripMenuItem;
        private ToolStripMenuItem jSObjectsToolStripMenuItem;
        private ToolStripMenuItem jSCSHandlesToolStripMenuItem;
        private ToolStripMenuItem countersToolStripMenuItem;
        private DataGridViewTextBoxColumn TypeName;
        private DataGridViewTextBoxColumn ObjectCount;
        private DataGridViewTextBoxColumn ShallowSize;
        private DataGridViewTextBoxColumn Depth2Size;
        private DataGridViewTextBoxColumn ReachableSize;
        private DataGridViewCheckBoxColumn Root;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
