using BotvaSpider.Controls;
using BotvaSpider.Controls.Configuration;
using BotvaSpider.Controls.Configuration.Accountant;

namespace BotvaSpider.Controls.Configuration
{
    partial class FormSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.farmControl1 = new BotvaSpider.Controls.FarmControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.boxAutoDisguise = new System.Windows.Forms.CheckBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.userSettings = new BotvaSpider.Controls.Configuration.UserSettings();
            this.attackSettingsControl1 = new BotvaSpider.Controls.Configuration.AttackSettingsControl();
            this.fightListControl1 = new BotvaSpider.Controls.FightListControl();
            this.mineSettingsControl1 = new BotvaSpider.Controls.Configuration.MineSettingsControl();
            this.wardrobeSettingsControl1 = new BotvaSpider.Controls.Configuration.WardrobeSettingsControl();
            this.accountantControl1 = new BotvaSpider.Controls.Configuration.Accountant.AccountantControl();
            this.userListsControl1 = new BotvaSpider.Controls.Configuration.UserListsControl();
            this.scheduleControl1 = new BotvaSpider.Controls.ScheduleControl();
            this.boxMinEmptySlots = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxMinEmptySlots)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 404);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.userSettings);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(577, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Пользователь";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(577, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Атака";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(571, 372);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.attackSettingsControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(563, 346);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Настройки";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.farmControl1);
            this.tabPage7.Location = new System.Drawing.Point(4, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(563, 346);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "Ферма";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // farmControl1
            // 
            this.farmControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.farmControl1.Location = new System.Drawing.Point(3, 3);
            this.farmControl1.Name = "farmControl1";
            this.farmControl1.Size = new System.Drawing.Size(557, 340);
            this.farmControl1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.fightListControl1);
            this.tabPage6.Location = new System.Drawing.Point(4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(563, 346);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Список для атаки";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.mineSettingsControl1);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(577, 378);
            this.tabPage8.TabIndex = 6;
            this.tabPage8.Text = "Шахта";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.boxMinEmptySlots);
            this.tabPage3.Controls.Add(this.wardrobeSettingsControl1);
            this.tabPage3.Controls.Add(this.boxAutoDisguise);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(577, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Одевалка";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // boxAutoDisguise
            // 
            this.boxAutoDisguise.AutoSize = true;
            this.boxAutoDisguise.Checked = true;
            this.boxAutoDisguise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxAutoDisguise.Location = new System.Drawing.Point(7, 4);
            this.boxAutoDisguise.Name = "boxAutoDisguise";
            this.boxAutoDisguise.Size = new System.Drawing.Size(127, 17);
            this.boxAutoDisguise.TabIndex = 2;
            this.boxAutoDisguise.Text = "Авто Переодевание";
            this.boxAutoDisguise.UseVisualStyleBackColor = true;
            this.boxAutoDisguise.CheckedChanged += new System.EventHandler(this.boxAutoDisguise_CheckedChanged);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.accountantControl1);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(577, 378);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "Бухгалтерия";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.userListsControl1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(577, 378);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Списки";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.scheduleControl1);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(577, 378);
            this.tabPage10.TabIndex = 7;
            this.tabPage10.Text = "Рассписание сна";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(421, 411);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(502, 411);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // userSettings
            // 
            this.userSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userSettings.Location = new System.Drawing.Point(3, 3);
            this.userSettings.Name = "userSettings";
            this.userSettings.Size = new System.Drawing.Size(571, 372);
            this.userSettings.TabIndex = 0;
            // 
            // attackSettingsControl1
            // 
            this.attackSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attackSettingsControl1.Location = new System.Drawing.Point(3, 3);
            this.attackSettingsControl1.Name = "attackSettingsControl1";
            this.attackSettingsControl1.Size = new System.Drawing.Size(557, 340);
            this.attackSettingsControl1.TabIndex = 0;
            // 
            // fightListControl1
            // 
            this.fightListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fightListControl1.Location = new System.Drawing.Point(3, 3);
            this.fightListControl1.Name = "fightListControl1";
            this.fightListControl1.Size = new System.Drawing.Size(557, 340);
            this.fightListControl1.TabIndex = 0;
            // 
            // mineSettingsControl1
            // 
            this.mineSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mineSettingsControl1.Location = new System.Drawing.Point(0, 0);
            this.mineSettingsControl1.MinimumSize = new System.Drawing.Size(240, 130);
            this.mineSettingsControl1.Name = "mineSettingsControl1";
            this.mineSettingsControl1.Size = new System.Drawing.Size(577, 378);
            this.mineSettingsControl1.TabIndex = 0;
            // 
            // wardrobeSettingsControl1
            // 
            this.wardrobeSettingsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wardrobeSettingsControl1.Location = new System.Drawing.Point(6, 77);
            this.wardrobeSettingsControl1.Name = "wardrobeSettingsControl1";
            this.wardrobeSettingsControl1.Size = new System.Drawing.Size(571, 298);
            this.wardrobeSettingsControl1.TabIndex = 3;
            this.wardrobeSettingsControl1.Load += new System.EventHandler(this.wardrobeSettingsControl1_Load);
            // 
            // accountantControl1
            // 
            this.accountantControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountantControl1.Location = new System.Drawing.Point(3, 3);
            this.accountantControl1.Name = "accountantControl1";
            this.accountantControl1.Size = new System.Drawing.Size(571, 372);
            this.accountantControl1.TabIndex = 0;
            // 
            // userListsControl1
            // 
            this.userListsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userListsControl1.Location = new System.Drawing.Point(3, 3);
            this.userListsControl1.Name = "userListsControl1";
            this.userListsControl1.Size = new System.Drawing.Size(571, 372);
            this.userListsControl1.TabIndex = 0;
            // 
            // scheduleControl1
            // 
            this.scheduleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleControl1.Location = new System.Drawing.Point(3, 3);
            this.scheduleControl1.Name = "scheduleControl1";
            this.scheduleControl1.Size = new System.Drawing.Size(571, 372);
            this.scheduleControl1.TabIndex = 21;
            // 
            // boxMinEmptySlots
            // 
            this.boxMinEmptySlots.Location = new System.Drawing.Point(186, 27);
            this.boxMinEmptySlots.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.boxMinEmptySlots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.boxMinEmptySlots.Name = "boxMinEmptySlots";
            this.boxMinEmptySlots.Size = new System.Drawing.Size(46, 20);
            this.boxMinEmptySlots.TabIndex = 4;
            this.boxMinEmptySlots.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Кол-во пустых слотов в одевалке";
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(583, 442);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boxMinEmptySlots)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox boxAutoDisguise;
        private System.Windows.Forms.TabPage tabPage6;
        private FightListControl fightListControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage7;
        private FarmControl farmControl1;
        private WardrobeSettingsControl wardrobeSettingsControl1;
        private System.Windows.Forms.TabPage tabPage9;
        private AccountantControl accountantControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private UserListsControl userListsControl1;
        private UserSettings userSettings;
        private System.Windows.Forms.TabPage tabPage8;
        private MineSettingsControl mineSettingsControl1;
        private AttackSettingsControl attackSettingsControl1;
        private System.Windows.Forms.TabPage tabPage10;
        private ScheduleControl scheduleControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown boxMinEmptySlots;

    }
}