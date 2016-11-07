namespace BotvaSpider.Controls.Mining
{
    partial class SaveMapControl
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
            this.crystalMap1 = new BotvaSpider.Controls.BigCrystalMap();
            this.crystalMapResult1 = new BigCrystalMapResult();
            this.buttonStore = new System.Windows.Forms.Button();
            this.boxIsSmall = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // crystalMap1
            // 
            this.crystalMap1.IsSmall = true;
            this.crystalMap1.Location = new System.Drawing.Point(13, 34);
            this.crystalMap1.Name = "crystalMap1";
            this.crystalMap1.Size = new System.Drawing.Size(188, 184);
            this.crystalMap1.TabIndex = 7;
            // 
            // crystalMapResult1
            // 
            this.crystalMapResult1.IsSmall = true;
            this.crystalMapResult1.Location = new System.Drawing.Point(207, 37);
            this.crystalMapResult1.Name = "crystalMapResult1";
            this.crystalMapResult1.Size = new System.Drawing.Size(235, 184);
            this.crystalMapResult1.TabIndex = 9;
            // 
            // buttonStore
            // 
            this.buttonStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStore.Location = new System.Drawing.Point(295, 0);
            this.buttonStore.Name = "buttonStore";
            this.buttonStore.Size = new System.Drawing.Size(147, 23);
            this.buttonStore.TabIndex = 6;
            this.buttonStore.Text = "Сохранить результаты";
            this.buttonStore.UseVisualStyleBackColor = true;
            this.buttonStore.Click += new System.EventHandler(this.buttonStore_Click);
            // 
            // boxIsSmall
            // 
            this.boxIsSmall.AutoSize = true;
            this.boxIsSmall.Checked = true;
            this.boxIsSmall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxIsSmall.Location = new System.Drawing.Point(13, 7);
            this.boxIsSmall.Name = "boxIsSmall";
            this.boxIsSmall.Size = new System.Drawing.Size(98, 17);
            this.boxIsSmall.TabIndex = 8;
            this.boxIsSmall.Text = "Малая поляна";
            this.boxIsSmall.UseVisualStyleBackColor = true;
            this.boxIsSmall.CheckedChanged += new System.EventHandler(this.boxIsSmall_CheckedChanged);
            // 
            // SaveMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.crystalMap1);
            this.Controls.Add(this.crystalMapResult1);
            this.Controls.Add(this.buttonStore);
            this.Controls.Add(this.boxIsSmall);
            this.Name = "SaveMapControl";
            this.Size = new System.Drawing.Size(447, 221);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BigCrystalMap crystalMap1;
        private BigCrystalMapResult crystalMapResult1;
        private System.Windows.Forms.Button buttonStore;
        private System.Windows.Forms.CheckBox boxIsSmall;
    }
}