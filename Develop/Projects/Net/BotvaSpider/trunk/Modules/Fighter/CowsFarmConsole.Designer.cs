namespace BotvaSpider.Consoles
{
    partial class CowsFarmConsole
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
            this.farmControl1 = new BotvaSpider.Controls.FarmControl();
            this.SuspendLayout();
            // 
            // farmControl1
            // 
            this.farmControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.farmControl1.Location = new System.Drawing.Point(0, 0);
            this.farmControl1.Name = "farmControl1";
            this.farmControl1.Size = new System.Drawing.Size(345, 307);
            this.farmControl1.TabIndex = 0;
            // 
            // FarmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 307);
            this.Controls.Add(this.farmControl1);
            this.Name = "FarmConsole";
            this.TabText = "Ферма";
            this.Text = "Ферма";
            this.ResumeLayout(false);

        }

        #endregion

        private BotvaSpider.Controls.FarmControl farmControl1;
    }
}