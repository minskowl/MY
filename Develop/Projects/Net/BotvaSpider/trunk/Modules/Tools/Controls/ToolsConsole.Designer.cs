namespace BotvaSpider.Consoles
{
    partial class ToolsConsole
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearStaffListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillStaffListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffListAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelStatus = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffListToolStripMenuItem});
            this.actionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.actionsToolStripMenuItem.MergeIndex = 0;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // staffListToolStripMenuItem
            // 
            this.staffListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearStaffListToolStripMenuItem,
            this.fillStaffListToolStripMenuItem,
            this.staffListAddToolStripMenuItem});
            this.staffListToolStripMenuItem.Name = "staffListToolStripMenuItem";
            this.staffListToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.staffListToolStripMenuItem.Text = "Штабные списки";
            // 
            // clearStaffListToolStripMenuItem
            // 
            this.clearStaffListToolStripMenuItem.Name = "clearStaffListToolStripMenuItem";
            this.clearStaffListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.clearStaffListToolStripMenuItem.Text = "Очистить";
            // 
            // fillStaffListToolStripMenuItem
            // 
            this.fillStaffListToolStripMenuItem.Name = "fillStaffListToolStripMenuItem";
            this.fillStaffListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.fillStaffListToolStripMenuItem.Text = "Заполнить";
            // 
            // staffListAddToolStripMenuItem
            // 
            this.staffListAddToolStripMenuItem.Name = "staffListAddToolStripMenuItem";
            this.staffListAddToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.staffListAddToolStripMenuItem.Text = "Добавить";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStatus.Location = new System.Drawing.Point(0, 24);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(59, 13);
            this.labelStatus.TabIndex = 20;
            this.labelStatus.Text = "labelStatus";
            // 
            // ToolsConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ToolsConsole";
            this.TabText = "ToolsConsole";
            this.Text = "ToolsConsole";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearStaffListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillStaffListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffListAddToolStripMenuItem;
        private System.Windows.Forms.Label labelStatus;
    }
}