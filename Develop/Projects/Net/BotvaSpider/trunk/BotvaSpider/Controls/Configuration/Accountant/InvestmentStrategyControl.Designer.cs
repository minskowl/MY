using BotvaSpider.Controls.Configuration.Accountant;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    partial class InvestmentStrategyControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.boxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tradeSearcherControl1 = new BotvaSpider.Controls.Configuration.Accountant.TradeSearcherControl();
            this.skillCombo1 = new BotvaSpider.Controls.SkillCombo();
            this.investmentStrategiesChekedList1 = new BotvaSpider.Controls.Configuration.Accountant.InvestmentStrategiesChekedList();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Стат для покупки";
            // 
            // boxEnabled
            // 
            this.boxEnabled.AutoSize = true;
            this.boxEnabled.Location = new System.Drawing.Point(289, 5);
            this.boxEnabled.Name = "boxEnabled";
            this.boxEnabled.Size = new System.Drawing.Size(163, 17);
            this.boxEnabled.TabIndex = 4;
            this.boxEnabled.Text = "Инвестирование включено";
            this.boxEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tradeSearcherControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 239);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Покупать шмот";
            // 
            // tradeSearcherControl1
            // 
            this.tradeSearcherControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tradeSearcherControl1.Location = new System.Drawing.Point(3, 16);
            this.tradeSearcherControl1.Name = "tradeSearcherControl1";
            this.tradeSearcherControl1.Size = new System.Drawing.Size(465, 220);
            this.tradeSearcherControl1.TabIndex = 2;
            // 
            // skillCombo1
            // 
            this.skillCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skillCombo1.FormattingEnabled = true;
            this.skillCombo1.Location = new System.Drawing.Point(285, 60);
            this.skillCombo1.Name = "skillCombo1";
            this.skillCombo1.Size = new System.Drawing.Size(121, 21);
            this.skillCombo1.TabIndex = 1;
            // 
            // investmentStrategiesChekedList1
            // 
            this.investmentStrategiesChekedList1.FormattingEnabled = true;
            this.investmentStrategiesChekedList1.Location = new System.Drawing.Point(3, 3);
            this.investmentStrategiesChekedList1.Name = "investmentStrategiesChekedList1";
            this.investmentStrategiesChekedList1.Size = new System.Drawing.Size(276, 79);
            this.investmentStrategiesChekedList1.TabIndex = 0;
            this.investmentStrategiesChekedList1.Value = BotvaSpider.Core.InvestmentStrategy.Undefined;
            // 
            // InvestmentStrategyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.boxEnabled);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skillCombo1);
            this.Controls.Add(this.investmentStrategiesChekedList1);
            this.MinimumSize = new System.Drawing.Size(450, 0);
            this.Name = "InvestmentStrategyControl";
            this.Size = new System.Drawing.Size(477, 327);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BotvaSpider.Controls.Configuration.Accountant.InvestmentStrategiesChekedList investmentStrategiesChekedList1;
        private SkillCombo skillCombo1;
        private TradeSearcherControl tradeSearcherControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox boxEnabled;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}