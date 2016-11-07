namespace CodeRocket.Controls
{
    partial class SchemaConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaConsole));
            this.schemaBrowser1 = new Savchin.Data.Schema.Controls.SchemaBrowser();
            this.SuspendLayout();
            // 
            // schemaBrowser1
            // 
            this.schemaBrowser1.CheckBoxes = false;
            this.schemaBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaBrowser1.Location = new System.Drawing.Point(0, 3);
            this.schemaBrowser1.Name = "schemaBrowser1";
            this.schemaBrowser1.Size = new System.Drawing.Size(364, 429);
            this.schemaBrowser1.TabIndex = 0;
            // 
            // SchemaConsole
            // 
            this.ClientSize = new System.Drawing.Size(364, 435);
            this.Controls.Add(this.schemaBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SchemaConsole";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Text = "Data Schema";
            this.ResumeLayout(false);

		}
		#endregion

        private Savchin.Data.Schema.Controls.SchemaBrowser schemaBrowser1;



    }
}