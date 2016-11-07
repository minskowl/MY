namespace Savchin.Forms.Browsers
{
    partial class AssemblyBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyBrowser));
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvObjects = new Forms.TreeViewEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.findInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBookMarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(3, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(154, 21);
            this.cmbType.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Location = new System.Drawing.Point(163, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.CmdSearchClick);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "empty.ico");
            this.ImageList1.Images.SetKeyName(1, "assembly.ico");
            this.ImageList1.Images.SetKeyName(2, "namespace.ico");
            this.ImageList1.Images.SetKeyName(3, "class.ico");
            this.ImageList1.Images.SetKeyName(4, "method.ico");
            this.ImageList1.Images.SetKeyName(5, "property.ico");
            // 
            // tvObjects
            // 
            this.tvObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.tvObjects.ContextMenuStrip = this.contextMenuStrip1;
            this.tvObjects.ImageIndex = 0;
            this.tvObjects.ImageList = this.ImageList1;
            this.tvObjects.Location = new System.Drawing.Point(3, 30);
            this.tvObjects.Name = "tvObjects";
            this.tvObjects.SelectedImageIndex = 0;
            this.tvObjects.Size = new System.Drawing.Size(238, 283);
            this.tvObjects.TabIndex = 0;
            this.tvObjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvObjectsAfterCheck);
            this.tvObjects.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvObjectsBeforeExpand);
            this.tvObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvObjectsAfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                               this.findInToolStripMenuItem,
                                                                                               this.addBookMarkToolStripMenuItem,
                                                                                               this.uncheckAllToolStripMenuItem,
                                                                                               this.checkToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // findInToolStripMenuItem
            // 
            this.findInToolStripMenuItem.Name = "findInToolStripMenuItem";
            this.findInToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.findInToolStripMenuItem.Text = "Find In";
            this.findInToolStripMenuItem.Click += new System.EventHandler(this.FindInToolStripMenuItemClick);
            // 
            // addBookMarkToolStripMenuItem
            // 
            this.addBookMarkToolStripMenuItem.Name = "addBookMarkToolStripMenuItem";
            this.addBookMarkToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addBookMarkToolStripMenuItem.Text = "Add BookMark";
            this.addBookMarkToolStripMenuItem.Click += new System.EventHandler(this.AddBookMarkToolStripMenuItemClick);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.uncheckAllToolStripMenuItem.Text = "Uncheck All";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.UncheckAllToolStripMenuItemClick);
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                                            this.allToolStripMenuItem});
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.checkToolStripMenuItem.Text = "Check";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.AllToolStripMenuItemClick);
            // 
            // AssemblyBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.tvObjects);
            this.Name = "AssemblyBrowser";
            this.Size = new System.Drawing.Size(241, 316);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeViewEx tvObjects;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button cmdSearch;
        internal System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem findInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBookMarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
    }
}