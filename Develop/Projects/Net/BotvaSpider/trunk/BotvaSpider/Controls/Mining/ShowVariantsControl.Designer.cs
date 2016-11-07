using BotvaSpider.Controls.Mining;

namespace BotvaSpider.Controls.Mining
{
    partial class ShowVariantsControl
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
            this.buttonShow = new System.Windows.Forms.Button();
            this.boxIsSmall1 = new System.Windows.Forms.CheckBox();
            this.bigCrystalMapResult1 = new BigCrystalMapResult();
            this.crystalMapSelector1 = new CrystalMapSelector();
            this.SuspendLayout();
            // 
            // buttonShow
            // 
            this.buttonShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShow.CausesValidation = false;
            this.buttonShow.Location = new System.Drawing.Point(370, 5);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 17;
            this.buttonShow.Text = "Показать варианты";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // boxIsSmall1
            // 
            this.boxIsSmall1.AutoSize = true;
            this.boxIsSmall1.Checked = true;
            this.boxIsSmall1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxIsSmall1.Location = new System.Drawing.Point(12, 9);
            this.boxIsSmall1.Name = "boxIsSmall1";
            this.boxIsSmall1.Size = new System.Drawing.Size(98, 17);
            this.boxIsSmall1.TabIndex = 15;
            this.boxIsSmall1.Text = "Малая поляна";
            this.boxIsSmall1.UseVisualStyleBackColor = true;
            this.boxIsSmall1.CheckedChanged += new System.EventHandler(this.boxIsSmall1_CheckedChanged);
            // 
            // bigCrystalMapResult1
            // 
            this.bigCrystalMapResult1.IsSmall = true;
            this.bigCrystalMapResult1.Location = new System.Drawing.Point(213, 32);
            this.bigCrystalMapResult1.Name = "bigCrystalMapResult1";
            this.bigCrystalMapResult1.Size = new System.Drawing.Size(242, 183);
            this.bigCrystalMapResult1.TabIndex = 16;
            // 
            // crystalMapSelector1
            // 
            this.crystalMapSelector1.IsSmall = true;
            this.crystalMapSelector1.Location = new System.Drawing.Point(3, 32);
            this.crystalMapSelector1.Name = "crystalMapSelector1";
            this.crystalMapSelector1.Size = new System.Drawing.Size(204, 201);
            this.crystalMapSelector1.TabIndex = 0;
            // 
            // ShowVariantsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.bigCrystalMapResult1);
            this.Controls.Add(this.boxIsSmall1);
            this.Controls.Add(this.crystalMapSelector1);
            this.Name = "ShowVariantsControl";
            this.Size = new System.Drawing.Size(459, 252);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalMapSelector crystalMapSelector1;
        private System.Windows.Forms.Button buttonShow;
        private BigCrystalMapResult bigCrystalMapResult1;
        private System.Windows.Forms.CheckBox boxIsSmall1;
    }
}