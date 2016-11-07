namespace SchemaEditor.Controls
{
    partial class PropertyConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyConsole));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.comboBoxControls = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(0, 34);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this;
            this.propertyGrid.Size = new System.Drawing.Size(364, 398);
            this.propertyGrid.TabIndex = 0;
            // 
            // comboBoxControls
            // 
            this.comboBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxControls.FormattingEnabled = true;
            this.comboBoxControls.Location = new System.Drawing.Point(4, 7);
            this.comboBoxControls.Name = "comboBoxControls";
            this.comboBoxControls.Size = new System.Drawing.Size(357, 21);
            this.comboBoxControls.TabIndex = 1;
            this.comboBoxControls.SelectedIndexChanged += new System.EventHandler(this.comboBoxControls_SelectedIndexChanged);
            // 
            // PropertyConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(364, 435);
            this.Controls.Add(this.comboBoxControls);
            this.Controls.Add(this.propertyGrid);
       
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PropertyConsole";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);

            this.Text = "Properties";
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.ComboBox comboBoxControls;
        private System.Windows.Forms.PropertyGrid propertyGrid;
    }
}