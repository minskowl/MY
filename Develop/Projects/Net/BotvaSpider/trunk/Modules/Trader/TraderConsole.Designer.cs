namespace BotvaSpider.Consoles
{
    partial class TraderConsole
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchInMarketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tradeSearcherControl1 = new BotvaSpider.Controls.Configuration.Accountant.TradeSearcherControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tradeSearcherControl1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(400, 365);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchInMarketToolStripMenuItem,
            this.investMoneyToolStripMenuItem});
            this.actionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.actionsToolStripMenuItem.MergeIndex = 0;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // searchInMarketToolStripMenuItem
            // 
            this.searchInMarketToolStripMenuItem.Name = "searchInMarketToolStripMenuItem";
            this.searchInMarketToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.searchInMarketToolStripMenuItem.Text = "Искать на рынке";
            this.searchInMarketToolStripMenuItem.Click += new System.EventHandler(this.searchInMarketToolStripMenuItem_Click);
            // 
            // investMoneyToolStripMenuItem
            // 
            this.investMoneyToolStripMenuItem.Name = "investMoneyToolStripMenuItem";
            this.investMoneyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.investMoneyToolStripMenuItem.Text = "Инвестировать деньги";
            this.investMoneyToolStripMenuItem.Click += new System.EventHandler(this.investMoneyToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 179);
            this.textBox1.TabIndex = 0;
            // 
            // tradeSearcherControl1
            // 
            this.tradeSearcherControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tradeSearcherControl1.Location = new System.Drawing.Point(0, 24);
            this.tradeSearcherControl1.Name = "tradeSearcherControl1";
            this.tradeSearcherControl1.Size = new System.Drawing.Size(400, 158);
            this.tradeSearcherControl1.TabIndex = 2;
            // 
            // TraderConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 365);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TraderConsole";
            this.TabText = "Сбытница";
            this.Text = "Сбытница";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchInMarketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem investMoneyToolStripMenuItem;
        private BotvaSpider.Controls.Configuration.Accountant.TradeSearcherControl tradeSearcherControl1;
    }
}