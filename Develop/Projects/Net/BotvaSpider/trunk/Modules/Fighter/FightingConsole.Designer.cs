using BotvaSpider.Fighter;

namespace BotvaSpider.Consoles
{
    partial class FightingConsole
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
            this.labelState = new System.Windows.Forms.Label();
            this.buttonFight = new System.Windows.Forms.Button();
            this.buttonStopFight = new System.Windows.Forms.Button();
            this.labelFightCount = new System.Windows.Forms.Label();
            this.labelFarmStatistics = new System.Windows.Forms.Label();
            this.labelRandomStatistics = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelBadErrorCount = new System.Windows.Forms.Label();
            this.labelErrorCount = new System.Windows.Forms.Label();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.checkBoxUsePatrol = new System.Windows.Forms.CheckBox();
            this.boxInvestmentEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boxAttackByRandom = new System.Windows.Forms.CheckBox();
            this.boxAttackByList = new System.Windows.Forms.CheckBox();
            this.boxAttackByFarm = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.userHotListControl = new BotvaSpider.Controls.UserListControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.milkingFarmStateControl = new BotvaSpider.Fighter.FarmStateControlControl();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelState
            // 
            this.labelState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelState.Location = new System.Drawing.Point(190, 30);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(272, 23);
            this.labelState.TabIndex = 5;
            this.labelState.Text = "label1";
            // 
            // buttonFight
            // 
            this.buttonFight.Location = new System.Drawing.Point(5, 30);
            this.buttonFight.Name = "buttonFight";
            this.buttonFight.Size = new System.Drawing.Size(82, 23);
            this.buttonFight.TabIndex = 6;
            this.buttonFight.Text = "Понеслась";
            this.buttonFight.UseVisualStyleBackColor = true;
            this.buttonFight.Click += new System.EventHandler(this.buttonFight_Click);
            // 
            // buttonStopFight
            // 
            this.buttonStopFight.Enabled = false;
            this.buttonStopFight.Location = new System.Drawing.Point(94, 30);
            this.buttonStopFight.Name = "buttonStopFight";
            this.buttonStopFight.Size = new System.Drawing.Size(90, 23);
            this.buttonStopFight.TabIndex = 7;
            this.buttonStopFight.Text = "Притормозим";
            this.buttonStopFight.UseVisualStyleBackColor = true;
            this.buttonStopFight.Click += new System.EventHandler(this.buttonStopFight_Click);
            // 
            // labelFightCount
            // 
            this.labelFightCount.AutoSize = true;
            this.labelFightCount.Location = new System.Drawing.Point(6, 30);
            this.labelFightCount.Name = "labelFightCount";
            this.labelFightCount.Size = new System.Drawing.Size(37, 13);
            this.labelFightCount.TabIndex = 8;
            this.labelFightCount.Text = "Всего";
            // 
            // labelFarmStatistics
            // 
            this.labelFarmStatistics.AutoSize = true;
            this.labelFarmStatistics.Location = new System.Drawing.Point(7, 53);
            this.labelFarmStatistics.Name = "labelFarmStatistics";
            this.labelFarmStatistics.Size = new System.Drawing.Size(58, 13);
            this.labelFarmStatistics.TabIndex = 9;
            this.labelFarmStatistics.Text = "По ферме";
            // 
            // labelRandomStatistics
            // 
            this.labelRandomStatistics.AutoSize = true;
            this.labelRandomStatistics.Location = new System.Drawing.Point(5, 75);
            this.labelRandomStatistics.Name = "labelRandomStatistics";
            this.labelRandomStatistics.Size = new System.Drawing.Size(67, 13);
            this.labelRandomStatistics.TabIndex = 9;
            this.labelRandomStatistics.Text = "По рандому";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(505, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.hideBrowserToolStripMenuItem});
            this.actionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.actionsToolStripMenuItem.MergeIndex = 0;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            // 
            // hideBrowserToolStripMenuItem
            // 
            this.hideBrowserToolStripMenuItem.Name = "hideBrowserToolStripMenuItem";
            this.hideBrowserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.hideBrowserToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.hideBrowserToolStripMenuItem.Tag = "Hide";
            this.hideBrowserToolStripMenuItem.Text = "Спрятать Браузер";
            this.hideBrowserToolStripMenuItem.Click += new System.EventHandler(this.hideBrowserToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 59);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(505, 345);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelBadErrorCount);
            this.tabPage1.Controls.Add(this.labelErrorCount);
            this.tabPage1.Controls.Add(this.labelFightCount);
            this.tabPage1.Controls.Add(this.labelFarmStatistics);
            this.tabPage1.Controls.Add(this.labelRandomStatistics);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(497, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Статистика";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelBadErrorCount
            // 
            this.labelBadErrorCount.AutoSize = true;
            this.labelBadErrorCount.Location = new System.Drawing.Point(158, 3);
            this.labelBadErrorCount.Name = "labelBadErrorCount";
            this.labelBadErrorCount.Size = new System.Drawing.Size(73, 13);
            this.labelBadErrorCount.TabIndex = 10;
            this.labelBadErrorCount.Text = "Ошибок бота";
            // 
            // labelErrorCount
            // 
            this.labelErrorCount.AutoSize = true;
            this.labelErrorCount.Location = new System.Drawing.Point(8, 3);
            this.labelErrorCount.Name = "labelErrorCount";
            this.labelErrorCount.Size = new System.Drawing.Size(92, 13);
            this.labelErrorCount.TabIndex = 10;
            this.labelErrorCount.Text = "Ошибок сервера";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.checkBoxUsePatrol);
            this.tabPageSettings.Controls.Add(this.boxInvestmentEnabled);
            this.tabPageSettings.Controls.Add(this.groupBox1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(497, 319);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Настройки";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // checkBoxUsePatrol
            // 
            this.checkBoxUsePatrol.AutoSize = true;
            this.checkBoxUsePatrol.Location = new System.Drawing.Point(6, 135);
            this.checkBoxUsePatrol.Name = "checkBoxUsePatrol";
            this.checkBoxUsePatrol.Size = new System.Drawing.Size(191, 17);
            this.checkBoxUsePatrol.TabIndex = 15;
            this.checkBoxUsePatrol.Text = "Авто. патрулирование по 10 мин";
            this.checkBoxUsePatrol.UseVisualStyleBackColor = true;
            this.checkBoxUsePatrol.CheckedChanged += new System.EventHandler(this.checkBoxUsePatrol_CheckedChanged);
            // 
            // boxInvestmentEnabled
            // 
            this.boxInvestmentEnabled.AutoSize = true;
            this.boxInvestmentEnabled.Location = new System.Drawing.Point(6, 112);
            this.boxInvestmentEnabled.Name = "boxInvestmentEnabled";
            this.boxInvestmentEnabled.Size = new System.Drawing.Size(188, 17);
            this.boxInvestmentEnabled.TabIndex = 4;
            this.boxInvestmentEnabled.Text = "Авто инвестирование включено";
            this.boxInvestmentEnabled.UseVisualStyleBackColor = true;
            this.boxInvestmentEnabled.CheckedChanged += new System.EventHandler(this.boxInvestmentEnabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxAttackByRandom);
            this.groupBox1.Controls.Add(this.boxAttackByList);
            this.groupBox1.Controls.Add(this.boxAttackByFarm);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Атака";
            // 
            // boxAttackByRandom
            // 
            this.boxAttackByRandom.AutoSize = true;
            this.boxAttackByRandom.Location = new System.Drawing.Point(6, 19);
            this.boxAttackByRandom.Name = "boxAttackByRandom";
            this.boxAttackByRandom.Size = new System.Drawing.Size(140, 17);
            this.boxAttackByRandom.TabIndex = 1;
            this.boxAttackByRandom.Text = "По случайному поиску";
            this.boxAttackByRandom.UseVisualStyleBackColor = true;
            this.boxAttackByRandom.CheckedChanged += new System.EventHandler(this.boxAttackByRandom_CheckedChanged);
            // 
            // boxAttackByList
            // 
            this.boxAttackByList.AutoSize = true;
            this.boxAttackByList.Location = new System.Drawing.Point(6, 65);
            this.boxAttackByList.Name = "boxAttackByList";
            this.boxAttackByList.Size = new System.Drawing.Size(78, 17);
            this.boxAttackByList.TabIndex = 3;
            this.boxAttackByList.Text = "По списку";
            this.boxAttackByList.UseVisualStyleBackColor = true;
            this.boxAttackByList.CheckedChanged += new System.EventHandler(this.boxAttackByList_CheckedChanged);
            // 
            // boxAttackByFarm
            // 
            this.boxAttackByFarm.AutoSize = true;
            this.boxAttackByFarm.Location = new System.Drawing.Point(6, 42);
            this.boxAttackByFarm.Name = "boxAttackByFarm";
            this.boxAttackByFarm.Size = new System.Drawing.Size(77, 17);
            this.boxAttackByFarm.TabIndex = 2;
            this.boxAttackByFarm.Text = "По ферме";
            this.boxAttackByFarm.UseVisualStyleBackColor = true;
            this.boxAttackByFarm.CheckedChanged += new System.EventHandler(this.boxAttackByFarm_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.userHotListControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(497, 319);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Горячий список";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // userHotListControl
            // 
            this.userHotListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userHotListControl.FightMachine = null;
            this.userHotListControl.Location = new System.Drawing.Point(3, 3);
            this.userHotListControl.Name = "userHotListControl";
            this.userHotListControl.Size = new System.Drawing.Size(491, 313);
            this.userHotListControl.TabIndex = 0;
            this.userHotListControl.UseWhiteListFilter = false;
            this.userHotListControl.ListChanged += new System.EventHandler(this.userHotListControl_ListChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.milkingFarmStateControl);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(497, 319);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Ферма";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // milkingFarmStateControl
            // 
            this.milkingFarmStateControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.milkingFarmStateControl.Location = new System.Drawing.Point(3, 3);
            this.milkingFarmStateControl.Name = "milkingFarmStateControl";
            this.milkingFarmStateControl.Size = new System.Drawing.Size(491, 313);
            this.milkingFarmStateControl.TabIndex = 0;
            // 
            // FightingConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 403);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonStopFight);
            this.Controls.Add(this.buttonFight);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FightingConsole";
            this.TabText = "Бодалка";
            this.Text = "Бодалка";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Button buttonFight;
        private System.Windows.Forms.Button buttonStopFight;
        private System.Windows.Forms.Label labelFightCount;
        private System.Windows.Forms.Label labelFarmStatistics;
        private System.Windows.Forms.Label labelRandomStatistics;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem hideBrowserToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.CheckBox boxAttackByList;
        private System.Windows.Forms.CheckBox boxAttackByFarm;
        private System.Windows.Forms.CheckBox boxAttackByRandom;
        private System.Windows.Forms.TabPage tabPage3;
        private BotvaSpider.Controls.UserListControl userHotListControl;
        private System.Windows.Forms.TabPage tabPage4;
        private FarmStateControlControl milkingFarmStateControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox boxInvestmentEnabled;
        private System.Windows.Forms.CheckBox checkBoxUsePatrol;
        private System.Windows.Forms.Label labelBadErrorCount;
        private System.Windows.Forms.Label labelErrorCount;
    }
}