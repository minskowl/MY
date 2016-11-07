using Savchin.Forms;
using Savchin.Forms.Docking;
using Savchin.WinApi;
using Savchin.WinApi.UserActivity;

namespace Savchin.EventSpy
{
    partial class ExplorerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
            this.controlSelector = new ControlFinder();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseTrackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assembyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new DockPanel();
            this.globalEventProvider1 = new GlobalEventProvider();
            this.statusLabelMouseCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlSelector
            // 
            this.controlSelector.IsSelectionEnabled = false;
            this.controlSelector.SelectionButton = System.Windows.Forms.MouseButtons.Right;
            this.controlSelector.SelectedControl += new FormComponentEventHandler(this.controlSelector_ControlSelected);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spyToolStripMenuItem,
            this.windowsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(475, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // spyToolStripMenuItem
            // 
            this.spyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectControlToolStripMenuItem,
            this.startFormToolStripMenuItem,
            this.captureTestToolStripMenuItem,
            this.mouseTrackingToolStripMenuItem});
            this.spyToolStripMenuItem.Name = "spyToolStripMenuItem";
            this.spyToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.spyToolStripMenuItem.Text = "Spy";
            // 
            // selectControlToolStripMenuItem
            // 
            this.selectControlToolStripMenuItem.Name = "selectControlToolStripMenuItem";
            this.selectControlToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.selectControlToolStripMenuItem.Text = "Select Control";
            this.selectControlToolStripMenuItem.Click += new System.EventHandler(this.selectControlToolStripMenuItem_Click);
            // 
            // startFormToolStripMenuItem
            // 
            this.startFormToolStripMenuItem.Name = "startFormToolStripMenuItem";
            this.startFormToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.startFormToolStripMenuItem.Text = "Start Form";
            // 
            // captureTestToolStripMenuItem
            // 
            this.captureTestToolStripMenuItem.Name = "captureTestToolStripMenuItem";
            this.captureTestToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.captureTestToolStripMenuItem.Text = "Capture Test";
            this.captureTestToolStripMenuItem.Click += new System.EventHandler(this.captureTestToolStripMenuItem_Click);
            // 
            // mouseTrackingToolStripMenuItem
            // 
            this.mouseTrackingToolStripMenuItem.Name = "mouseTrackingToolStripMenuItem";
            this.mouseTrackingToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.mouseTrackingToolStripMenuItem.Text = "Mouse Tracking";
            this.mouseTrackingToolStripMenuItem.Click += new System.EventHandler(this.mouseTrackingToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assembyToolStripMenuItem,
            this.formsToolStripMenuItem,
            this.outputToolStripMenuItem,
            this.propertyToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.windowsToolStripMenuItem.Text = "&Windows";
            // 
            // assembyToolStripMenuItem
            // 
            this.assembyToolStripMenuItem.Name = "assembyToolStripMenuItem";
            this.assembyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.assembyToolStripMenuItem.Text = "&Assemby";
            this.assembyToolStripMenuItem.Click += new System.EventHandler(this.assembyToolStripMenuItem_Click);
            // 
            // formsToolStripMenuItem
            // 
            this.formsToolStripMenuItem.Name = "formsToolStripMenuItem";
            this.formsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.formsToolStripMenuItem.Text = "&Forms";
            this.formsToolStripMenuItem.Click += new System.EventHandler(this.formsToolStripMenuItem_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.outputToolStripMenuItem.Text = "&Output";
            this.outputToolStripMenuItem.Click += new System.EventHandler(this.outputToolStripMenuItem_Click);
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.propertyToolStripMenuItem.Text = "&Property";
            this.propertyToolStripMenuItem.Click += new System.EventHandler(this.propertyToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(475, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMouseCoord});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(475, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.Location = new System.Drawing.Point(0, 52);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(475, 511);
            this.dockPanel.TabIndex = 3;
            // 
            // statusLabelMouseCoord
            // 
            this.statusLabelMouseCoord.Name = "statusLabelMouseCoord";
            this.statusLabelMouseCoord.Size = new System.Drawing.Size(109, 17);
            this.statusLabelMouseCoord.Text = "toolStripStatusLabel1";
            // 
            // ExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 588);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dockPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ExplorerForm";
            this.Text = "ExplorerForm";
            this.Load += new System.EventHandler(this.ExplorerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExplorerForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlFinder controlSelector;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectControlToolStripMenuItem;
        private DockPanel dockPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem assembyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseTrackingToolStripMenuItem;
        private GlobalEventProvider globalEventProvider1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMouseCoord;
    }
}