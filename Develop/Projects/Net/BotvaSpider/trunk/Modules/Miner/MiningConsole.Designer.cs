namespace BotvaSpider.Consoles
{
    partial class MiningConsole
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
            this.buttonStopFight = new System.Windows.Forms.Button();
            this.buttonFight = new System.Windows.Forms.Button();
            this.labelState = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mineCristalDistrutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMineTotalStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStopFight
            // 
            this.buttonStopFight.Enabled = false;
            this.buttonStopFight.Location = new System.Drawing.Point(97, 34);
            this.buttonStopFight.Name = "buttonStopFight";
            this.buttonStopFight.Size = new System.Drawing.Size(90, 23);
            this.buttonStopFight.TabIndex = 10;
            this.buttonStopFight.Text = "Притормозим";
            this.buttonStopFight.UseVisualStyleBackColor = true;
            this.buttonStopFight.Click += new System.EventHandler(this.buttonStopFight_Click);
            // 
            // buttonFight
            // 
            this.buttonFight.Location = new System.Drawing.Point(8, 34);
            this.buttonFight.Name = "buttonFight";
            this.buttonFight.Size = new System.Drawing.Size(82, 23);
            this.buttonFight.TabIndex = 9;
            this.buttonFight.Text = "Понеслась";
            this.buttonFight.UseVisualStyleBackColor = true;
            this.buttonFight.Click += new System.EventHandler(this.buttonFight_Click);
            // 
            // labelState
            // 
            this.labelState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelState.Location = new System.Drawing.Point(193, 34);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(236, 23);
            this.labelState.TabIndex = 8;
            this.labelState.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statisticsToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineCristalDistrutionToolStripMenuItem,
            this.showMineTotalStatisticsToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.statisticsToolStripMenuItem.Text = "Статистика";
            // 
            // mineCristalDistrutionToolStripMenuItem
            // 
            this.mineCristalDistrutionToolStripMenuItem.Name = "mineCristalDistrutionToolStripMenuItem";
            this.mineCristalDistrutionToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.mineCristalDistrutionToolStripMenuItem.Text = "Распределение кристалов в шахте";
            // 
            // showMineTotalStatisticsToolStripMenuItem
            // 
            this.showMineTotalStatisticsToolStripMenuItem.Name = "showMineTotalStatisticsToolStripMenuItem";
            this.showMineTotalStatisticsToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.showMineTotalStatisticsToolStripMenuItem.Text = "Суммарная статистика";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideBrowserToolStripMenuItem});
            this.actionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.actionsToolStripMenuItem.MergeIndex = 0;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // hideBrowserToolStripMenuItem
            // 
            this.hideBrowserToolStripMenuItem.Name = "hideBrowserToolStripMenuItem";
            this.hideBrowserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.hideBrowserToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.hideBrowserToolStripMenuItem.Tag = "Hide";
            this.hideBrowserToolStripMenuItem.Text = "Спрятать Браузер";
            this.hideBrowserToolStripMenuItem.Click += new System.EventHandler(this.hideBrowserToolStripMenuItem_Click);
            // 
            // MiningConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 508);
            this.Controls.Add(this.buttonStopFight);
            this.Controls.Add(this.buttonFight);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MiningConsole";
            this.TabText = "MiningConsole";
            this.Text = "MiningConsole";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStopFight;
        private System.Windows.Forms.Button buttonFight;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mineCristalDistrutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMineTotalStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideBrowserToolStripMenuItem;
    }
}