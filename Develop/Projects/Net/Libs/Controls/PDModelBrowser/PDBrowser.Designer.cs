using Savchin.Forms;

namespace Savchin.Controls.Browsers
{
    partial class PDBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDBrowser));
            this.tvObj = new Forms.TreeViewEx();
            this.cmMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miLoadModel = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveModel = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenSP = new System.Windows.Forms.ToolStripMenuItem();
            this.openDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelSearch = new System.Windows.Forms.Panel();
            this.cmbObjects = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.cmMain.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvObj
            // 
            this.tvObj.ContextMenuStrip = this.cmMain;
            this.tvObj.ImageIndex = 0;
            this.tvObj.ImageList = this.imageList1;
            this.tvObj.Location = new System.Drawing.Point(0, 29);
            this.tvObj.Name = "tvObj";
            this.tvObj.SelectedImageIndex = 0;
            this.tvObj.Size = new System.Drawing.Size(424, 372);
            this.tvObj.TabIndex = 0;
            this.tvObj.DoubleClick += new System.EventHandler(this.tvObj_DoubleClick);
            this.tvObj.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterSelect);
            this.tvObj.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterExpand);
            // 
            // cmMain
            // 
            this.cmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadModel,
            this.miSaveModel,
            this.miOpenSP,
            this.openDiagramToolStripMenuItem});
            this.cmMain.Name = "cmMain";
            this.cmMain.Size = new System.Drawing.Size(194, 92);
            this.cmMain.Opening += new System.ComponentModel.CancelEventHandler(this.cmMain_Opening);
            // 
            // miLoadModel
            // 
            this.miLoadModel.Name = "miLoadModel";
            this.miLoadModel.Size = new System.Drawing.Size(193, 22);
            this.miLoadModel.Text = "LoadModel";
            this.miLoadModel.Click += new System.EventHandler(this.miLoadModel_Click_1);
            // 
            // miSaveModel
            // 
            this.miSaveModel.Name = "miSaveModel";
            this.miSaveModel.Size = new System.Drawing.Size(193, 22);
            this.miSaveModel.Text = "SaveModel";
            this.miSaveModel.Click += new System.EventHandler(this.miSaveModel_Click_1);
            // 
            // miOpenSP
            // 
            this.miOpenSP.Name = "miOpenSP";
            this.miOpenSP.Size = new System.Drawing.Size(193, 22);
            this.miOpenSP.Text = "OpenSP";
            this.miOpenSP.Click += new System.EventHandler(this.miOpenSP_Click_1);
            // 
            // openDiagramToolStripMenuItem
            // 
            this.openDiagramToolStripMenuItem.Name = "openDiagramToolStripMenuItem";
            this.openDiagramToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openDiagramToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openDiagramToolStripMenuItem.Text = "Open Diagram";
            this.openDiagramToolStripMenuItem.Click += new System.EventHandler(this.openDiagramToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.ico");
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.cmbObjects);
            this.panelSearch.Controls.Add(this.buttonSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(424, 29);
            this.panelSearch.TabIndex = 1;
            // 
            // cmbObjects
            // 
            this.cmbObjects.FormattingEnabled = true;
            this.cmbObjects.Location = new System.Drawing.Point(0, 3);
            this.cmbObjects.Name = "cmbObjects";
            this.cmbObjects.Size = new System.Drawing.Size(343, 21);
            this.cmbObjects.Sorted = true;
            this.cmbObjects.TabIndex = 1;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(349, 0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // PDBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.tvObj);
            this.Name = "PDBrowser";
            this.Size = new System.Drawing.Size(424, 401);
            this.Load += new System.EventHandler(this.PDBrowser_Load);
            this.Resize += new System.EventHandler(this.PDBrowser_Resize);
            this.cmMain.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeViewEx tvObj;
        private System.Windows.Forms.ContextMenuStrip cmMain;
        private System.Windows.Forms.ToolStripMenuItem miLoadModel;
        private System.Windows.Forms.ToolStripMenuItem miSaveModel;
        private System.Windows.Forms.ToolStripMenuItem miOpenSP;
        private System.Windows.Forms.ToolStripMenuItem openDiagramToolStripMenuItem;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.ComboBox cmbObjects;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ImageList imageList1;
    }
}
