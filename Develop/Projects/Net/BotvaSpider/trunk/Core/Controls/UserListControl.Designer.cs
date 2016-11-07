namespace BotvaSpider.Controls
{
    partial class UserListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.boxInput = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromClipBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertCrystalOwnersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertFromShtabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertTOPCowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxInput
            // 
            this.boxInput.AcceptsReturn = true;
            this.boxInput.ContextMenuStrip = this.contextMenuStrip1;
            this.boxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxInput.Location = new System.Drawing.Point(0, 0);
            this.boxInput.Multiline = true;
            this.boxInput.Name = "boxInput";
            this.boxInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.boxInput.Size = new System.Drawing.Size(150, 150);
            this.boxInput.TabIndex = 0;
            this.boxInput.TextChanged += new System.EventHandler(this.boxInput_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 158);
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.UndoToolStripMenuItem.Text = "Отменить";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.cutToolStripMenuItem.Text = "Вырезать";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyToolStripMenuItem.Text = "Копировать";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromClipBoardToolStripMenuItem,
            this.insertCrystalOwnersToolStripMenuItem,
            this.insertFromShtabToolStripMenuItem,
            this.insertTOPCowsToolStripMenuItem,
            this.guildToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.insertToolStripMenuItem.Text = "Вставить";
            // 
            // fromClipBoardToolStripMenuItem
            // 
            this.fromClipBoardToolStripMenuItem.Name = "fromClipBoardToolStripMenuItem";
            this.fromClipBoardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.fromClipBoardToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.fromClipBoardToolStripMenuItem.Text = "Из буфера обмена";
            this.fromClipBoardToolStripMenuItem.Click += new System.EventHandler(this.fromClipBoardToolStripMenuItem_Click);
            // 
            // insertCrystalOwnersToolStripMenuItem
            // 
            this.insertCrystalOwnersToolStripMenuItem.Name = "insertCrystalOwnersToolStripMenuItem";
            this.insertCrystalOwnersToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.insertCrystalOwnersToolStripMenuItem.Text = "Владельцев кристалов";
            this.insertCrystalOwnersToolStripMenuItem.Click += new System.EventHandler(this.insertCrystalOwnersToolStripMenuItem_Click);
            // 
            // insertFromShtabToolStripMenuItem
            // 
            this.insertFromShtabToolStripMenuItem.Name = "insertFromShtabToolStripMenuItem";
            this.insertFromShtabToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.insertFromShtabToolStripMenuItem.Text = "Коров из штаба";
            this.insertFromShtabToolStripMenuItem.Click += new System.EventHandler(this.insertFromShtabToolStripMenuItem_Click);
            // 
            // insertTOPCowsToolStripMenuItem
            // 
            this.insertTOPCowsToolStripMenuItem.Name = "insertTOPCowsToolStripMenuItem";
            this.insertTOPCowsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.insertTOPCowsToolStripMenuItem.Text = "Топовых из фермы";
            this.insertTOPCowsToolStripMenuItem.Click += new System.EventHandler(this.insertTOPCowsToolStripMenuItem_Click);
            // 
            // guildToolStripMenuItem
            // 
            this.guildToolStripMenuItem.Name = "guildToolStripMenuItem";
            this.guildToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.guildToolStripMenuItem.Text = "Из гильдии";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // UserListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.boxInput);
            this.Name = "UserListControl";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boxInput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertCrystalOwnersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertFromShtabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertTOPCowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromClipBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guildToolStripMenuItem;
    }
}
