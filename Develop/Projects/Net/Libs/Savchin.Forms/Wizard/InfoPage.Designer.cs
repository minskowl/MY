using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Wizard
{

	/// <summary>
	/// An inherited <see cref="InfoContainer"/> that contains a <see cref="Label"/> 
	/// with the description of the page.
	/// </summary>
	public partial class InfoPage : InfoContainer
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
        
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(172, 56);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(304, 328);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "This wizard enables you to...";
            // 
            // InfoPage
            // 
            this.Controls.Add(this.lblDescription);
            this.Name = "InfoPage";
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.ResumeLayout(false);

		}
		Label	lblDescription ;
		#endregion

	
	}
}

