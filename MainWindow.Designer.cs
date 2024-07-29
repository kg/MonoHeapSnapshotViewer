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
            TypesGrid = new DataGridView();
            TypeName = new DataGridViewTextBoxColumn();
            ObjectCount = new DataGridViewTextBoxColumn();
            ShallowSize = new DataGridViewTextBoxColumn();
            Depth2Size = new DataGridViewTextBoxColumn();
            SubtreeSize = new DataGridViewTextBoxColumn();
            MenuBar = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            OpenDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)TypesGrid).BeginInit();
            MenuBar.SuspendLayout();
            SuspendLayout();
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
            TypesGrid.Columns.AddRange(new DataGridViewColumn[] { TypeName, ObjectCount, ShallowSize, Depth2Size, SubtreeSize });
            TypesGrid.Location = new Point(0, 36);
            TypesGrid.Name = "TypesGrid";
            TypesGrid.RowHeadersVisible = false;
            TypesGrid.RowHeadersWidth = 62;
            TypesGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            TypesGrid.Size = new Size(1258, 628);
            TypesGrid.TabIndex = 0;
            TypesGrid.VirtualMode = true;
            TypesGrid.CellValueNeeded += TypesGrid_CellValueNeeded;
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 664);
            Controls.Add(TypesGrid);
            Controls.Add(MenuBar);
            MainMenuStrip = MenuBar;
            Name = "MainWindow";
            Text = "Heap Snapshot Viewer";
            ((System.ComponentModel.ISupportInitialize)TypesGrid).EndInit();
            MenuBar.ResumeLayout(false);
            MenuBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView TypesGrid;
        private DataGridViewTextBoxColumn TypeName;
        private DataGridViewTextBoxColumn ObjectCount;
        private DataGridViewTextBoxColumn ShallowSize;
        private DataGridViewTextBoxColumn Depth2Size;
        private DataGridViewTextBoxColumn SubtreeSize;
        private MenuStrip MenuBar;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private OpenFileDialog OpenDialog;
    }
}
