namespace BotvaSpider
{
    partial class FormTest
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
            this.logViewer = new BotvaSpider.Controls.LogViewer();
            this.SuspendLayout();
            // 
            // logViewer
            // 
            this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewer.Location = new System.Drawing.Point(0, 0);
            this.logViewer.Name = "logViewer";
            this.logViewer.Size = new System.Drawing.Size(292, 273);
            this.logViewer.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.logViewer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private BotvaSpider.Controls.LogViewer logViewer;



    }
}