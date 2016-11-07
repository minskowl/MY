namespace BotvaSpider.Controls.Configuration
{
    partial class AttackSettingsControl
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
            this.checkBoxAllowLostGlory = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.boxAttackTimeShift = new System.Windows.Forms.NumericUpDown();
            this.boxIgnoreWarsClan = new System.Windows.Forms.CheckBox();
            this.boxMinSkillDifference = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.boxMinBenefit = new System.Windows.Forms.NumericUpDown();
            this.rivalSourcesControl1 = new BotvaSpider.Controls.Configuration.RivalSourcesControl();
            ((System.ComponentModel.ISupportInitialize)(this.boxAttackTimeShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMinSkillDifference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMinBenefit)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxAllowLostGlory
            // 
            this.checkBoxAllowLostGlory.AutoSize = true;
            this.checkBoxAllowLostGlory.Location = new System.Drawing.Point(15, 73);
            this.checkBoxAllowLostGlory.Name = "checkBoxAllowLostGlory";
            this.checkBoxAllowLostGlory.Size = new System.Drawing.Size(144, 17);
            this.checkBoxAllowLostGlory.TabIndex = 16;
            this.checkBoxAllowLostGlory.Text = "Разрешить слив славы";
            this.checkBoxAllowLostGlory.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Корректировка атаки";
            this.toolTip1.SetToolTip(this.label7, "Временная корректировка атаки (сек)");
            // 
            // boxAttackTimeShift
            // 
            this.boxAttackTimeShift.Location = new System.Drawing.Point(160, 51);
            this.boxAttackTimeShift.Name = "boxAttackTimeShift";
            this.boxAttackTimeShift.Size = new System.Drawing.Size(73, 20);
            this.boxAttackTimeShift.TabIndex = 14;
            // 
            // boxIgnoreWarsClan
            // 
            this.boxIgnoreWarsClan.AutoSize = true;
            this.boxIgnoreWarsClan.Location = new System.Drawing.Point(15, 96);
            this.boxIgnoreWarsClan.Name = "boxIgnoreWarsClan";
            this.boxIgnoreWarsClan.Size = new System.Drawing.Size(159, 17);
            this.boxIgnoreWarsClan.TabIndex = 22;
            this.boxIgnoreWarsClan.Text = "Игнорировать кланы в КВ";
            this.boxIgnoreWarsClan.UseVisualStyleBackColor = true;
            // 
            // boxMinSkillDifference
            // 
            this.boxMinSkillDifference.Location = new System.Drawing.Point(160, 8);
            this.boxMinSkillDifference.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.boxMinSkillDifference.Name = "boxMinSkillDifference";
            this.boxMinSkillDifference.Size = new System.Drawing.Size(73, 20);
            this.boxMinSkillDifference.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Мин. разница в статах";
            this.toolTip1.SetToolTip(this.label2, "Минимальная суммарная разница в скилах ");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Мин. доход";
            this.toolTip1.SetToolTip(this.label3, "Минн доход с коровы. Если средний доход с коровы станет меньшим эттой суммы бот п" +
                    "ерестанет ее бить.");
            // 
            // boxMinBenefit
            // 
            this.boxMinBenefit.Location = new System.Drawing.Point(160, 28);
            this.boxMinBenefit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.boxMinBenefit.Name = "boxMinBenefit";
            this.boxMinBenefit.Size = new System.Drawing.Size(73, 20);
            this.boxMinBenefit.TabIndex = 26;
            // 
            // rivalSourcesControl1
            // 
            this.rivalSourcesControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rivalSourcesControl1.Location = new System.Drawing.Point(3, 119);
            this.rivalSourcesControl1.Name = "rivalSourcesControl1";
            this.rivalSourcesControl1.Size = new System.Drawing.Size(473, 219);
            this.rivalSourcesControl1.TabIndex = 27;
            // 
            // AttackSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rivalSourcesControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.boxMinBenefit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxMinSkillDifference);
            this.Controls.Add(this.boxIgnoreWarsClan);
            this.Controls.Add(this.checkBoxAllowLostGlory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.boxAttackTimeShift);
            this.Name = "AttackSettingsControl";
            this.Size = new System.Drawing.Size(479, 341);
            ((System.ComponentModel.ISupportInitialize)(this.boxAttackTimeShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMinSkillDifference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxMinBenefit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAllowLostGlory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown boxAttackTimeShift;
        private System.Windows.Forms.CheckBox boxIgnoreWarsClan;
        private System.Windows.Forms.NumericUpDown boxMinSkillDifference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown boxMinBenefit;
        private RivalSourcesControl rivalSourcesControl1;
    }
}
