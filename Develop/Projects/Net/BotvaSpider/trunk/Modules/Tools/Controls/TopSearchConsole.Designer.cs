namespace BotvaSpider.Consoles
{
    partial class TopSearchConsole
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonImportAllFights = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importClanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getRivalsFromClanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTopClansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchClanWithMoneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTopLoosersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importGuildUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxOut = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Поиск";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrl.Location = new System.Drawing.Point(48, 37);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(707, 20);
            this.textBoxUrl.TabIndex = 11;
            // 
            // buttonImportAllFights
            // 
            this.buttonImportAllFights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportAllFights.Location = new System.Drawing.Point(633, 512);
            this.buttonImportAllFights.Name = "buttonImportAllFights";
            this.buttonImportAllFights.Size = new System.Drawing.Size(122, 23);
            this.buttonImportAllFights.TabIndex = 14;
            this.buttonImportAllFights.Text = "Import All Fights";
            this.buttonImportAllFights.UseVisualStyleBackColor = true;
            this.buttonImportAllFights.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(767, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importClanToolStripMenuItem,
            this.getRivalsFromClanToolStripMenuItem,
            this.importTopClansToolStripMenuItem,
            this.searchClanWithMoneyToolStripMenuItem,
            this.importTopLoosersToolStripMenuItem,
            this.importGuildUsersToolStripMenuItem});
            this.actionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.actionsToolStripMenuItem.MergeIndex = 0;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // importClanToolStripMenuItem
            // 
            this.importClanToolStripMenuItem.Name = "importClanToolStripMenuItem";
            this.importClanToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.importClanToolStripMenuItem.Text = "Импортировать Клан";
            this.importClanToolStripMenuItem.Click += new System.EventHandler(this.importClanToolStripMenuItem_Click);
            // 
            // getRivalsFromClanToolStripMenuItem
            // 
            this.getRivalsFromClanToolStripMenuItem.Name = "getRivalsFromClanToolStripMenuItem";
            this.getRivalsFromClanToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.getRivalsFromClanToolStripMenuItem.Text = "Выбрать противников из кланов";
            this.getRivalsFromClanToolStripMenuItem.Click += new System.EventHandler(this.getRivalsFromClanToolStripMenuItem_Click);
            // 
            // importTopClansToolStripMenuItem
            // 
            this.importTopClansToolStripMenuItem.Name = "importTopClansToolStripMenuItem";
            this.importTopClansToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.importTopClansToolStripMenuItem.Text = "Import Top Clans";
            this.importTopClansToolStripMenuItem.Click += new System.EventHandler(this.importTopClansToolStripMenuItem_Click);
            // 
            // searchClanWithMoneyToolStripMenuItem
            // 
            this.searchClanWithMoneyToolStripMenuItem.Name = "searchClanWithMoneyToolStripMenuItem";
            this.searchClanWithMoneyToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.searchClanWithMoneyToolStripMenuItem.Text = "Поиск Кланов с казной";
            this.searchClanWithMoneyToolStripMenuItem.Click += new System.EventHandler(this.searchClanWithMoneyToolStripMenuItem_Click);
            // 
            // importTopLoosersToolStripMenuItem
            // 
            this.importTopLoosersToolStripMenuItem.Name = "importTopLoosersToolStripMenuItem";
            this.importTopLoosersToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.importTopLoosersToolStripMenuItem.Text = "Импорт Лучших проигрывающих";
            this.importTopLoosersToolStripMenuItem.Click += new System.EventHandler(this.importTopLoosersToolStripMenuItem_Click);
            // 
            // importGuildUsersToolStripMenuItem
            // 
            this.importGuildUsersToolStripMenuItem.Name = "importGuildUsersToolStripMenuItem";
            this.importGuildUsersToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.importGuildUsersToolStripMenuItem.Text = "Импорт Юзеров из гильдии";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 514);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(59, 13);
            this.labelStatus.TabIndex = 16;
            this.labelStatus.Text = "labelStatus";
            // 
            // textBoxOut
            // 
            this.textBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOut.Location = new System.Drawing.Point(6, 62);
            this.textBoxOut.Multiline = true;
            this.textBoxOut.Name = "textBoxOut";
            this.textBoxOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOut.Size = new System.Drawing.Size(751, 427);
            this.textBoxOut.TabIndex = 17;
            // 
            // TopSearchConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 569);
            this.Controls.Add(this.textBoxOut);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonImportAllFights);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TopSearchConsole";
            this.TabText = "TopSearchConsole";
            this.Text = "Поиск в ТОПе";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonImportAllFights;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importClanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getRivalsFromClanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTopClansToolStripMenuItem;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ToolStripMenuItem searchClanWithMoneyToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxOut;
        private System.Windows.Forms.ToolStripMenuItem importTopLoosersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importGuildUsersToolStripMenuItem;
    }
}