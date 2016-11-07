namespace CodeRocket.Controls
{
    partial class PowerDesigerConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerDesigerConsole));
            this.pdBrowser1 = new Savchin.Controls.Browsers.PDBrowser();
            this.SuspendLayout();
            // 
            // pdBrowser1
            // 
            this.pdBrowser1.CheckBoxes = false;
            this.pdBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdBrowser1.Location = new System.Drawing.Point(0, 3);
            this.pdBrowser1.ModelFilePath = null;
            this.pdBrowser1.Name = "pdBrowser1";
            this.pdBrowser1.ResourcePath = null;
            this.pdBrowser1.ShowSearch = true;
            this.pdBrowser1.Size = new System.Drawing.Size(364, 429);
            this.pdBrowser1.TabIndex = 0;
            // 
            // PowerDesigerConsole
            // 
            this.ClientSize = new System.Drawing.Size(364, 435);
            this.Controls.Add(this.pdBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PowerDesigerConsole";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);

		}
		#endregion

        private Savchin.Controls.Browsers.PDBrowser pdBrowser1;

    }
}