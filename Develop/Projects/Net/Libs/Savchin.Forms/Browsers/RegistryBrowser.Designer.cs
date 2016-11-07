namespace Savchin.Forms.Browsers
{
    partial class RegistryBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistryBrowser));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.stringValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dWORDValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvObj = new Forms.TreeViewEx();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.ico");
            this.imageList1.Images.SetKeyName(1, "folder.gif");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                               this.newToolStripMenuItem,
                                                                                               this.renameToolStripMenuItem,
                                                                                               this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                                          this.kewToolStripMenuItem,
                                                                                                          this.toolStripMenuItem1,
                                                                                                          this.stringValueToolStripMenuItem,
                                                                                                          this.dWORDValueToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // kewToolStripMenuItem
            // 
            this.kewToolStripMenuItem.Name = "kewToolStripMenuItem";
            this.kewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.kewToolStripMenuItem.Text = "Key";
            this.kewToolStripMenuItem.Click += new System.EventHandler(this.kewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // stringValueToolStripMenuItem
            // 
            this.stringValueToolStripMenuItem.Name = "stringValueToolStripMenuItem";
            this.stringValueToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.stringValueToolStripMenuItem.Text = "String Value";
            this.stringValueToolStripMenuItem.Click += new System.EventHandler(this.stringValueToolStripMenuItem_Click);
            // 
            // dWORDValueToolStripMenuItem
            // 
            this.dWORDValueToolStripMenuItem.Name = "dWORDValueToolStripMenuItem";
            this.dWORDValueToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dWORDValueToolStripMenuItem.Text = "DWORD Value";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // tvObj
            // 
            this.tvObj.ContextMenuStrip = this.contextMenuStrip1;
            this.tvObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObj.ImageIndex = 0;
            this.tvObj.ImageList = this.imageList1;
            this.tvObj.LabelEdit = true;
            this.tvObj.Location = new System.Drawing.Point(0, 0);
            this.tvObj.Name = "tvObj";
            this.tvObj.SelectedImageIndex = 0;
            this.tvObj.Size = new System.Drawing.Size(318, 269);
            this.tvObj.TabIndex = 1;
            this.tvObj.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvObj_AfterLabelEdit);
            this.tvObj.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvObj_BeforeExpand);
            this.tvObj.DoubleClick += new System.EventHandler(this.tvObj_DoubleClick);
            this.tvObj.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterSelect);
            // 
            // RegistryBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvObj);
            this.Name = "RegistryBrowser";
            this.Size = new System.Drawing.Size(318, 269);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeViewEx tvObj;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stringValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dWORDValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    }
}