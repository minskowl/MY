using Savchin.Forms.Browsers;

namespace Savchin.EventSpy.Consoles
{
    partial class AssemblyWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyWindow));
            this.assemblyBrowser1 = new AssemblyBrowser();
            this.SuspendLayout();
            // 
            // assemblyBrowser1
            // 
            this.assemblyBrowser1.CheckBoxes = false;
            this.assemblyBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assemblyBrowser1.Location = new System.Drawing.Point(0, 3);
            this.assemblyBrowser1.Name = "assemblyBrowser1";
            this.assemblyBrowser1.Size = new System.Drawing.Size(208, 283);
            this.assemblyBrowser1.TabIndex = 0;
            // 
            // AssemblyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(208, 289);
            this.Controls.Add(this.assemblyBrowser1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssemblyWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ShowHint = Savchin.Forms.Docking.DockState.DockRight;
            this.TabText = "Assembly";
            this.Text = "Assembly";
            this.ResumeLayout(false);

		}
		#endregion

        private AssemblyBrowser assemblyBrowser1;

    }
}