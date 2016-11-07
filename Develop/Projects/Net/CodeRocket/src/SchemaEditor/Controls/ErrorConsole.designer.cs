namespace SchemaEditor.Controls
{
    partial class ErrorConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorConsole));
            this.errorViewer1 = new Controls.ExceptionViewer();
            this.SuspendLayout();
            // 
            // errorViewer1
            // 
            this.errorViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorViewer1.Location = new System.Drawing.Point(0, 3);
            this.errorViewer1.Name = "errorViewer1";
            this.errorViewer1.Size = new System.Drawing.Size(364, 429);
            this.errorViewer1.TabIndex = 0;
            // 
            // ErrorConsole
            // 
            this.ClientSize = new System.Drawing.Size(364, 435);
            this.Controls.Add(this.errorViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorConsole";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);

		}
		#endregion

        private ExceptionViewer errorViewer1;



    }
}