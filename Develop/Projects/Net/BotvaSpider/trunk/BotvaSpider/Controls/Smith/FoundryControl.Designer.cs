using BotvaSpider.Controls.Mining;

namespace BotvaSpider.Controls
{
    partial class FoundryControl
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.controlResult = new BigCrystalMapResult();
            this.controlMap = new BotvaSpider.Controls.BigCrystalMap();
            this.pageControl1 = new BotvaSpider.Controls.PageControl();
            this.checkBoxSlideMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(106, 188);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // controlResult
            // 
            this.controlResult.IsSmall = false;
            this.controlResult.Location = new System.Drawing.Point(210, 6);
            this.controlResult.Name = "controlResult";
            this.controlResult.Size = new System.Drawing.Size(242, 183);
            this.controlResult.TabIndex = 1;
            // 
            // controlMap
            // 
            this.controlMap.IsSmall = false;
            this.controlMap.Location = new System.Drawing.Point(8, 4);
            this.controlMap.Name = "controlMap";
            this.controlMap.Size = new System.Drawing.Size(185, 189);
            this.controlMap.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Location = new System.Drawing.Point(276, 182);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Pages = 0;
            this.pageControl1.Size = new System.Drawing.Size(184, 23);
            this.pageControl1.TabIndex = 16;
            this.pageControl1.Visible = false;
            this.pageControl1.CurrentPageChanged += new System.EventHandler(this.pageControl1_CurrentPageChanged);
            // 
            // checkBoxSlideMode
            // 
            this.checkBoxSlideMode.AutoSize = true;
            this.checkBoxSlideMode.Checked = true;
            this.checkBoxSlideMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSlideMode.Location = new System.Drawing.Point(210, 184);
            this.checkBoxSlideMode.Name = "checkBoxSlideMode";
            this.checkBoxSlideMode.Size = new System.Drawing.Size(60, 17);
            this.checkBoxSlideMode.TabIndex = 15;
            this.checkBoxSlideMode.Text = "Сумма";
            this.checkBoxSlideMode.UseVisualStyleBackColor = true;
            this.checkBoxSlideMode.CheckedChanged += new System.EventHandler(this.checkBoxSlideMode_CheckedChanged);
            // 
            // FoundryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.checkBoxSlideMode);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.controlResult);
            this.Controls.Add(this.controlMap);
            this.Name = "FoundryControl";
            this.Size = new System.Drawing.Size(463, 251);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BigCrystalMap controlMap;
        private BigCrystalMapResult controlResult;
        private System.Windows.Forms.Button buttonSave;
        private PageControl pageControl1;
        private System.Windows.Forms.CheckBox checkBoxSlideMode;

    }
}
