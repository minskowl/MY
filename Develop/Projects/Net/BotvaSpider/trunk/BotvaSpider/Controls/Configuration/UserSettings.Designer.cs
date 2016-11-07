namespace BotvaSpider.Controls.Configuration
{
    partial class UserSettings
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
            this.checkBoxUsePatrol = new System.Windows.Forms.CheckBox();
            this.boxCoolStatus = new System.Windows.Forms.CheckBox();
            this.boxAutoThreat = new System.Windows.Forms.CheckBox();
            this.boxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.boxEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.boxShowAllerts = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.boxDebugger = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boxMaxDangerousErrors = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.boxMaxInternetErrors = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listServers = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxMaxDangerousErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMaxInternetErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxUsePatrol
            // 
            this.checkBoxUsePatrol.AutoSize = true;
            this.checkBoxUsePatrol.Location = new System.Drawing.Point(3, 107);
            this.checkBoxUsePatrol.Name = "checkBoxUsePatrol";
            this.checkBoxUsePatrol.Size = new System.Drawing.Size(191, 17);
            this.checkBoxUsePatrol.TabIndex = 14;
            this.checkBoxUsePatrol.Text = "Авто. патрулирование по 10 мин";
            this.checkBoxUsePatrol.UseVisualStyleBackColor = true;
            // 
            // boxCoolStatus
            // 
            this.boxCoolStatus.AutoSize = true;
            this.boxCoolStatus.Location = new System.Drawing.Point(3, 84);
            this.boxCoolStatus.Name = "boxCoolStatus";
            this.boxCoolStatus.Size = new System.Drawing.Size(128, 17);
            this.boxCoolStatus.TabIndex = 13;
            this.boxCoolStatus.Text = "Есть статус крутого";
            this.boxCoolStatus.UseVisualStyleBackColor = true;
            // 
            // boxAutoThreat
            // 
            this.boxAutoThreat.AutoSize = true;
            this.boxAutoThreat.Location = new System.Drawing.Point(3, 133);
            this.boxAutoThreat.Name = "boxAutoThreat";
            this.boxAutoThreat.Size = new System.Drawing.Size(187, 17);
            this.boxAutoThreat.TabIndex = 12;
            this.boxAutoThreat.Text = "Автоматическое лечение перса";
            this.boxAutoThreat.UseVisualStyleBackColor = true;
            // 
            // boxPassword
            // 
            this.boxPassword.Location = new System.Drawing.Point(45, 55);
            this.boxPassword.Name = "boxPassword";
            this.boxPassword.Size = new System.Drawing.Size(202, 20);
            this.boxPassword.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Пароль";
            // 
            // boxEmail
            // 
            this.boxEmail.Location = new System.Drawing.Point(45, 29);
            this.boxEmail.Name = "boxEmail";
            this.boxEmail.Size = new System.Drawing.Size(202, 20);
            this.boxEmail.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Логин";
            // 
            // boxShowAllerts
            // 
            this.boxShowAllerts.AutoSize = true;
            this.boxShowAllerts.Location = new System.Drawing.Point(6, 19);
            this.boxShowAllerts.Name = "boxShowAllerts";
            this.boxShowAllerts.Size = new System.Drawing.Size(144, 17);
            this.boxShowAllerts.TabIndex = 13;
            this.boxShowAllerts.Text = "Оповещать о событиях";
            this.toolTip1.SetToolTip(this.boxShowAllerts, "Если в лог пришло событие рангом совета и выше то окно бота начинает моргать или " +
                    "рядом с иконкой в трее появляется подсказка и меняется иконка.");
            this.boxShowAllerts.UseVisualStyleBackColor = true;
            // 
            // boxDebugger
            // 
            this.boxDebugger.AutoSize = true;
            this.boxDebugger.Location = new System.Drawing.Point(6, 92);
            this.boxDebugger.Name = "boxDebugger";
            this.boxDebugger.Size = new System.Drawing.Size(127, 17);
            this.boxDebugger.TabIndex = 15;
            this.boxDebugger.Text = "Включить Дебаггер";
            this.toolTip1.SetToolTip(this.boxDebugger, "Только для разработчиков !!!!!!!!");
            this.boxDebugger.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxMaxDangerousErrors);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.boxMaxInternetErrors);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.boxShowAllerts);
            this.groupBox1.Controls.Add(this.boxDebugger);
            this.groupBox1.Location = new System.Drawing.Point(0, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 123);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление ошибками";
            // 
            // boxMaxDangerousErrors
            // 
            this.boxMaxDangerousErrors.Location = new System.Drawing.Point(166, 67);
            this.boxMaxDangerousErrors.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.boxMaxDangerousErrors.Name = "boxMaxDangerousErrors";
            this.boxMaxDangerousErrors.Size = new System.Drawing.Size(62, 20);
            this.boxMaxDangerousErrors.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Макс. кол-во опасных ошибок";
            // 
            // boxMaxInternetErrors
            // 
            this.boxMaxInternetErrors.Location = new System.Drawing.Point(166, 41);
            this.boxMaxInternetErrors.Name = "boxMaxInternetErrors";
            this.boxMaxInternetErrors.Size = new System.Drawing.Size(62, 20);
            this.boxMaxInternetErrors.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Макс. кол-во ошибок Inernet-a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Сервер";
            // 
            // listServers
            // 
            this.listServers.FormattingEnabled = true;
            this.listServers.Location = new System.Drawing.Point(45, 4);
            this.listServers.Name = "listServers";
            this.listServers.Size = new System.Drawing.Size(202, 21);
            this.listServers.TabIndex = 18;
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listServers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxUsePatrol);
            this.Controls.Add(this.boxCoolStatus);
            this.Controls.Add(this.boxAutoThreat);
            this.Controls.Add(this.boxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxEmail);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(250, 0);
            this.Name = "UserSettings";
            this.Size = new System.Drawing.Size(260, 366);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxMaxDangerousErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMaxInternetErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxUsePatrol;
        private System.Windows.Forms.CheckBox boxCoolStatus;
        private System.Windows.Forms.CheckBox boxAutoThreat;
        private System.Windows.Forms.TextBox boxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox boxEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox boxShowAllerts;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox boxDebugger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown boxMaxDangerousErrors;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown boxMaxInternetErrors;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox listServers;
    }
}
