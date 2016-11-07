using Savchin.Forms.Browsers;

namespace CodeRocket.Controls
{
    partial class AssemblyConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyConsole));
            this.assemblyBrowser1 = new AssemblyBrowser();
            this.SuspendLayout();
            // 
            // assemblyBrowser1
            // 
            this.assemblyBrowser1.CheckBoxes = false;
            this.assemblyBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assemblyBrowser1.Location = new System.Drawing.Point(0, 3);
            this.assemblyBrowser1.Name = "assemblyBrowser1";
            this.assemblyBrowser1.Size = new System.Drawing.Size(364, 429);
            this.assemblyBrowser1.TabIndex = 0;
            // 
            // AssemblyConsole
            // 
            this.ClientSize = new System.Drawing.Size(364, 435);
            this.Controls.Add(this.assemblyBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssemblyConsole";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);

		}
		#endregion

        private AssemblyBrowser assemblyBrowser1;


    }
}