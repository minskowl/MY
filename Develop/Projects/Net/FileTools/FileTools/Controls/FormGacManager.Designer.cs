using Savchin.Forms.ListView;

namespace FileTools.Controls
{
    partial class FormGacManager
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
            this.gacBrowser1 = new Savchin.Forms.Browsers.GacBrowser();
            this.SuspendLayout();
            // 
            // gacBrowser1
            // 
            this.gacBrowser1.AllowDrop = true;
            this.gacBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gacBrowser1.Location = new System.Drawing.Point(0, 0);
            this.gacBrowser1.Name = "gacBrowser1";
            this.gacBrowser1.Size = new System.Drawing.Size(284, 262);
            this.gacBrowser1.TabIndex = 0;
            // 
            // FormGacManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gacBrowser1);
            this.Name = "FormGacManager";
            this.Text = "FormGacManager";
            this.ResumeLayout(false);

        }

        #endregion

        private Savchin.Forms.Browsers.GacBrowser gacBrowser1;

    }
}