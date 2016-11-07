using Savchin.Forms.Docking;

namespace CodeRocket
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToSolutionDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new DockPanel();
            this.contextMenuShema = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reverseEngeniringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schemaSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.contextMenuShema.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(750, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.recentProjectsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "&Project";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Enabled = false;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.newProjectToolStripMenuItem.Text = "&New";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Enabled = false;
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openProjectToolStripMenuItem.Text = "&Open...";
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveProjectToolStripMenuItem.Text = "&Save";
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Enabled = false;
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save &As..";
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Enabled = false;
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.closeProjectToolStripMenuItem.Text = "&Close";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
            // 
            // recentProjectsToolStripMenuItem
            // 
            this.recentProjectsToolStripMenuItem.Name = "recentProjectsToolStripMenuItem";
            this.recentProjectsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.recentProjectsToolStripMenuItem.Text = "&Recent Projects";
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToOutputToolStripMenuItem,
            this.generateToSolutionDirToolStripMenuItem,
            this.generateToSolutionToolStripMenuItem});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.generateToolStripMenuItem.Text = "&Generate";
            // 
            // generateToOutputToolStripMenuItem
            // 
            this.generateToOutputToolStripMenuItem.Enabled = false;
            this.generateToOutputToolStripMenuItem.Name = "generateToOutputToolStripMenuItem";
            this.generateToOutputToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateToOutputToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generateToOutputToolStripMenuItem.Text = "To Output";
            // 
            // generateToSolutionDirToolStripMenuItem
            // 
            this.generateToSolutionDirToolStripMenuItem.Enabled = false;
            this.generateToSolutionDirToolStripMenuItem.Name = "generateToSolutionDirToolStripMenuItem";
            this.generateToSolutionDirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.G)));
            this.generateToSolutionDirToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generateToSolutionDirToolStripMenuItem.Text = "To Solution Dir";
            // 
            // generateToSolutionToolStripMenuItem
            // 
            this.generateToSolutionToolStripMenuItem.Enabled = false;
            this.generateToSolutionToolStripMenuItem.Name = "generateToSolutionToolStripMenuItem";
            this.generateToSolutionToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generateToSolutionToolStripMenuItem.Text = "To Solution";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assemblyToolStripMenuItem,
            this.errorsToolStripMenuItem,
            this.dataSchemaToolStripMenuItem,
            this.powerDesignerToolStripMenuItem,
            this.propertyToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // assemblyToolStripMenuItem
            // 
            this.assemblyToolStripMenuItem.Enabled = false;
            this.assemblyToolStripMenuItem.Name = "assemblyToolStripMenuItem";
            this.assemblyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.assemblyToolStripMenuItem.Text = "Assembly";
            // 
            // errorsToolStripMenuItem
            // 
            this.errorsToolStripMenuItem.Enabled = false;
            this.errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            this.errorsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.errorsToolStripMenuItem.Text = "Errors";
            // 
            // dataSchemaToolStripMenuItem
            // 
            this.dataSchemaToolStripMenuItem.Enabled = false;
            this.dataSchemaToolStripMenuItem.Name = "dataSchemaToolStripMenuItem";
            this.dataSchemaToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.dataSchemaToolStripMenuItem.Text = "Data Schema";
            // 
            // powerDesignerToolStripMenuItem
            // 
            this.powerDesignerToolStripMenuItem.Enabled = false;
            this.powerDesignerToolStripMenuItem.Name = "powerDesignerToolStripMenuItem";
            this.powerDesignerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.powerDesignerToolStripMenuItem.Text = "Power Designer";
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.Enabled = false;
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.propertyToolStripMenuItem.Text = "Property";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Enabled = false;
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Enabled = false;
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.closeAllToolStripMenuItem.Text = "Close All Files";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(750, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.Location = new System.Drawing.Point(0, 27);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(750, 409);
            this.dockPanel.TabIndex = 2;
            // 
            // contextMenuShema
            // 
            this.contextMenuShema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reverseEngeniringToolStripMenuItem,
            this.schemaSaveToolStripMenuItem});
            this.contextMenuShema.Name = "contextMenuShema";
            this.contextMenuShema.Size = new System.Drawing.Size(174, 70);
            // 
            // reverseEngeniringToolStripMenuItem
            // 
            this.reverseEngeniringToolStripMenuItem.Name = "reverseEngeniringToolStripMenuItem";
            this.reverseEngeniringToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.reverseEngeniringToolStripMenuItem.Text = "Reverse engineering";
            // 
            // schemaSaveToolStripMenuItem
            // 
            this.schemaSaveToolStripMenuItem.Name = "schemaSaveToolStripMenuItem";
            this.schemaSaveToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.schemaSaveToolStripMenuItem.Text = "Save";
            // 
            // FormMain1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 461);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.Text = "CodeRocket";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain1_FormClosing);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.contextMenuShema.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToSolutionDirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToSolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerDesignerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuShema;
        private System.Windows.Forms.ToolStripMenuItem reverseEngeniringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schemaSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}