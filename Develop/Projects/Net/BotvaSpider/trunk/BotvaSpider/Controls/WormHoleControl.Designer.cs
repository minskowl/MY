using BotvaSpider.Controls.Mining;

namespace BotvaSpider.Controls
{
    partial class WormHoleControl
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
            this.checkBoxSlideMode = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.holeMapResult = new BigCrystalMapResult();
            this.holelMap = new BotvaSpider.Controls.BigCrystalMap();
            this.pageControl1 = new BotvaSpider.Controls.PageControl();
            this.SuspendLayout();
            // 
            // checkBoxSlideMode
            // 
            this.checkBoxSlideMode.AutoSize = true;
            this.checkBoxSlideMode.Checked = true;
            this.checkBoxSlideMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSlideMode.Location = new System.Drawing.Point(180, 188);
            this.checkBoxSlideMode.Name = "checkBoxSlideMode";
            this.checkBoxSlideMode.Size = new System.Drawing.Size(60, 17);
            this.checkBoxSlideMode.TabIndex = 12;
            this.checkBoxSlideMode.Text = "Сумма";
            this.checkBoxSlideMode.UseVisualStyleBackColor = true;
            this.checkBoxSlideMode.CheckedChanged += new System.EventHandler(this.checkBoxSlideMode_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(4, 184);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // holeMapResult
            // 
            this.holeMapResult.IsSmall = false;
            this.holeMapResult.Location = new System.Drawing.Point(201, 6);
            this.holeMapResult.Name = "holeMapResult";
            this.holeMapResult.Size = new System.Drawing.Size(242, 183);
            this.holeMapResult.TabIndex = 8;
            // 
            // holelMap
            // 
            this.holelMap.IsSmall = false;
            this.holelMap.Location = new System.Drawing.Point(0, 6);
            this.holelMap.Name = "holelMap";
            this.holelMap.Size = new System.Drawing.Size(185, 189);
            this.holelMap.TabIndex = 7;
            // 
            // pageControl1
            // 
            this.pageControl1.Location = new System.Drawing.Point(246, 186);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Pages = 0;
            this.pageControl1.Size = new System.Drawing.Size(184, 23);
            this.pageControl1.TabIndex = 14;
            this.pageControl1.Visible = false;
            this.pageControl1.CurrentPageChanged += new System.EventHandler(this.pageControl1_CurrentPageChanged);
            // 
            // WormHoleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.checkBoxSlideMode);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.holeMapResult);
            this.Controls.Add(this.holelMap);
            this.Name = "WormHoleControl";
            this.Size = new System.Drawing.Size(436, 239);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSlideMode;
        private System.Windows.Forms.Button buttonSave;
        private BigCrystalMapResult holeMapResult;
        private BigCrystalMap holelMap;
        private PageControl pageControl1;
    }
}
