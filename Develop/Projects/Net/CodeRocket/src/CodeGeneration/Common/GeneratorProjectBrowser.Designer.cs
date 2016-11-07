using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Savchin.Forms;

namespace Savchin.CodeGeneration
{
    partial class GeneratorProjectBrowser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneratorProjectBrowser));
            this.tvObjects = new TreeViewEx();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvObjects
            // 
            this.tvObjects.CheckBoxes = true;
            this.tvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObjects.ImageIndex = 0;
            this.tvObjects.ImageList = this.imageList1;
            this.tvObjects.Location = new System.Drawing.Point(0, 0);
            this.tvObjects.Name = "tvObjects";
            this.tvObjects.SelectedImageIndex = 0;
            this.tvObjects.Size = new System.Drawing.Size(277, 299);
            this.tvObjects.TabIndex = 0;
            this.tvObjects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvObjectsAfterCheck);
            this.tvObjects.DoubleClick += new System.EventHandler(this.TvObjectsDoubleClick);
            this.tvObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvObjectsAfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "prj.ico");
            this.imageList1.Images.SetKeyName(1, "template.ico");
            this.imageList1.Images.SetKeyName(2, "folder.ico");
            this.imageList1.Images.SetKeyName(3, "folder_open.ico");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 26);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.GenerateToolStripMenuItemClick);
            // 
            // GeneratorProjectBrowser
            // 
            this.Controls.Add(this.tvObjects);
            this.Name = "GeneratorProjectBrowser";
            this.Size = new System.Drawing.Size(277, 299);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public TreeViewEx tvObjects;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem generateToolStripMenuItem;
        private ImageList imageList1;

    }
}
