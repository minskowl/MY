namespace BotvaSpider.Controls
{
    partial class RangeFilterControl
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
            this.groupBoxRivalLevel = new System.Windows.Forms.GroupBox();
            this.boxEnabled = new System.Windows.Forms.CheckBox();
            this.boxRivalLevelTo = new System.Windows.Forms.NumericUpDown();
            this.boxRivalLevelFrom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxRivalLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxRivalLevelTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxRivalLevelFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxRivalLevel
            // 
            this.groupBoxRivalLevel.Controls.Add(this.boxEnabled);
            this.groupBoxRivalLevel.Controls.Add(this.boxRivalLevelTo);
            this.groupBoxRivalLevel.Controls.Add(this.boxRivalLevelFrom);
            this.groupBoxRivalLevel.Controls.Add(this.label5);
            this.groupBoxRivalLevel.Controls.Add(this.label4);
            this.groupBoxRivalLevel.Location = new System.Drawing.Point(3, 3);
            this.groupBoxRivalLevel.Name = "groupBoxRivalLevel";
            this.groupBoxRivalLevel.Size = new System.Drawing.Size(171, 49);
            this.groupBoxRivalLevel.TabIndex = 8;
            this.groupBoxRivalLevel.TabStop = false;
            // 
            // boxEnabled
            // 
            this.boxEnabled.AutoSize = true;
            this.boxEnabled.Location = new System.Drawing.Point(6, 0);
            this.boxEnabled.Name = "boxEnabled";
            this.boxEnabled.Size = new System.Drawing.Size(80, 17);
            this.boxEnabled.TabIndex = 9;
            this.boxEnabled.Text = "checkBox1";
            this.boxEnabled.UseVisualStyleBackColor = true;
            // 
            // boxRivalLevelTo
            // 
            this.boxRivalLevelTo.Location = new System.Drawing.Point(114, 19);
            this.boxRivalLevelTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.boxRivalLevelTo.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.boxRivalLevelTo.Name = "boxRivalLevelTo";
            this.boxRivalLevelTo.Size = new System.Drawing.Size(47, 20);
            this.boxRivalLevelTo.TabIndex = 8;
            // 
            // boxRivalLevelFrom
            // 
            this.boxRivalLevelFrom.Location = new System.Drawing.Point(33, 19);
            this.boxRivalLevelFrom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.boxRivalLevelFrom.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.boxRivalLevelFrom.Name = "boxRivalLevelFrom";
            this.boxRivalLevelFrom.Size = new System.Drawing.Size(49, 20);
            this.boxRivalLevelFrom.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "до +";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "от +";
            // 
            // RangeFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxRivalLevel);
            this.Name = "RangeFilterControl";
            this.Size = new System.Drawing.Size(181, 59);
            this.groupBoxRivalLevel.ResumeLayout(false);
            this.groupBoxRivalLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxRivalLevelTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxRivalLevelFrom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRivalLevel;
        protected System.Windows.Forms.NumericUpDown boxRivalLevelTo;
        protected System.Windows.Forms.NumericUpDown boxRivalLevelFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox boxEnabled;
  
    }
}
