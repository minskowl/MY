namespace BotvaSpider.Controls.Configuration.Accountant
{
    partial class AccountantControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.boxMinMoney = new System.Windows.Forms.NumericUpDown();
            this.boxInvestmentEnabled = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.coulombSelector1 = new BotvaSpider.Controls.CoulombSelector();
            this.boxSoundEnabled = new System.Windows.Forms.CheckBox();
            this.boxSearchEnabled = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.boxShopingInteval = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.normalInvestmentStrategyControl = new BotvaSpider.Controls.Configuration.Accountant.InvestmentStrategyControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.atackInvestmentStrategyControl = new BotvaSpider.Controls.Configuration.Accountant.InvestmentStrategyControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.notificationSearcher = new BotvaSpider.Controls.Configuration.Accountant.TradeSearcherControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.boxMinMoney)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxShopingInteval)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Мин. остаток";
            // 
            // boxMinMoney
            // 
            this.boxMinMoney.Location = new System.Drawing.Point(79, 57);
            this.boxMinMoney.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.boxMinMoney.Name = "boxMinMoney";
            this.boxMinMoney.Size = new System.Drawing.Size(89, 20);
            this.boxMinMoney.TabIndex = 2;
            // 
            // boxInvestmentEnabled
            // 
            this.boxInvestmentEnabled.AutoSize = true;
            this.boxInvestmentEnabled.Location = new System.Drawing.Point(6, 6);
            this.boxInvestmentEnabled.Name = "boxInvestmentEnabled";
            this.boxInvestmentEnabled.Size = new System.Drawing.Size(188, 17);
            this.boxInvestmentEnabled.TabIndex = 3;
            this.boxInvestmentEnabled.Text = "Авто инвестирование включено";
            this.boxInvestmentEnabled.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 309);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.coulombSelector1);
            this.tabPage1.Controls.Add(this.boxSoundEnabled);
            this.tabPage1.Controls.Add(this.boxSearchEnabled);
            this.tabPage1.Controls.Add(this.boxInvestmentEnabled);
            this.tabPage1.Controls.Add(this.boxMinMoney);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.boxShopingInteval);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(377, 268);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Главная";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // coulombSelector1
            // 
            this.coulombSelector1.Location = new System.Drawing.Point(200, 3);
            this.coulombSelector1.Name = "coulombSelector1";
            this.coulombSelector1.Size = new System.Drawing.Size(118, 116);
            this.coulombSelector1.SmallSize = true;
            this.coulombSelector1.TabIndex = 4;
            // 
            // boxSoundEnabled
            // 
            this.boxSoundEnabled.AutoSize = true;
            this.boxSoundEnabled.Location = new System.Drawing.Point(9, 120);
            this.boxSoundEnabled.Name = "boxSoundEnabled";
            this.boxSoundEnabled.Size = new System.Drawing.Size(140, 17);
            this.boxSoundEnabled.TabIndex = 3;
            this.boxSoundEnabled.Text = "Звуковое оповещение";
            this.boxSoundEnabled.UseVisualStyleBackColor = true;
            // 
            // boxSearchEnabled
            // 
            this.boxSearchEnabled.AutoSize = true;
            this.boxSearchEnabled.Location = new System.Drawing.Point(9, 97);
            this.boxSearchEnabled.Name = "boxSearchEnabled";
            this.boxSearchEnabled.Size = new System.Drawing.Size(111, 17);
            this.boxSearchEnabled.TabIndex = 3;
            this.boxSearchEnabled.Text = "Розыск включен";
            this.boxSearchEnabled.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Интервал";
            this.toolTip1.SetToolTip(this.label3, "Интервал в мин между посещениями сбытницы");
            // 
            // boxShopingInteval
            // 
            this.boxShopingInteval.Location = new System.Drawing.Point(79, 30);
            this.boxShopingInteval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.boxShopingInteval.Name = "boxShopingInteval";
            this.boxShopingInteval.Size = new System.Drawing.Size(89, 20);
            this.boxShopingInteval.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.normalInvestmentStrategyControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(377, 268);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Обычное инвест.";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // normalInvestmentStrategyControl
            // 
            this.normalInvestmentStrategyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalInvestmentStrategyControl.Location = new System.Drawing.Point(3, 3);
            this.normalInvestmentStrategyControl.Margin = new System.Windows.Forms.Padding(0);
            this.normalInvestmentStrategyControl.Name = "normalInvestmentStrategyControl";
            this.normalInvestmentStrategyControl.Size = new System.Drawing.Size(371, 262);
            this.normalInvestmentStrategyControl.TabIndex = 20;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.atackInvestmentStrategyControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(486, 283);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "При нападении";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // atackInvestmentStrategyControl
            // 
            this.atackInvestmentStrategyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.atackInvestmentStrategyControl.Location = new System.Drawing.Point(3, 3);
            this.atackInvestmentStrategyControl.Name = "atackInvestmentStrategyControl";
            this.atackInvestmentStrategyControl.Size = new System.Drawing.Size(480, 277);
            this.atackInvestmentStrategyControl.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.notificationSearcher);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(377, 268);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Розыск";
            this.toolTip1.SetToolTip(this.tabPage4, "Поиск вещей в сбытницы с оповещением о находке");
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // notificationSearcher
            // 
            this.notificationSearcher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationSearcher.Location = new System.Drawing.Point(3, 3);
            this.notificationSearcher.Name = "notificationSearcher";
            this.notificationSearcher.Size = new System.Drawing.Size(371, 262);
            this.notificationSearcher.TabIndex = 0;
            // 
            // AccountantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "AccountantControl";
            this.Size = new System.Drawing.Size(494, 309);
            ((System.ComponentModel.ISupportInitialize)(this.boxMinMoney)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxShopingInteval)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown boxMinMoney;
        private System.Windows.Forms.CheckBox boxInvestmentEnabled;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private InvestmentStrategyControl normalInvestmentStrategyControl;
        private System.Windows.Forms.TabPage tabPage3;
        private InvestmentStrategyControl atackInvestmentStrategyControl;
        private CoulombSelector coulombSelector1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown boxShopingInteval;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage4;
        private TradeSearcherControl notificationSearcher;
        private System.Windows.Forms.CheckBox boxSearchEnabled;
        private System.Windows.Forms.CheckBox boxSoundEnabled;

    }
}