using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für frmBitmapEditor.
	/// </summary>
	internal class frmBitmapEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox picPreView;
		private System.Windows.Forms.Button cmdChangeImage;
		private System.Windows.Forms.Button cmdApply;
		private System.Windows.Forms.Button cmdCancel;
		private System.Drawing.Bitmap myImg;
		public System.Drawing.Bitmap Image
		{
			get
			{
				return myImg;
			}
			set
			{
				myImg = value;
				displayImage();
			}
		}
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmBitmapEditor()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.picPreView = new System.Windows.Forms.PictureBox();
			this.cmdChangeImage = new System.Windows.Forms.Button();
			this.cmdApply = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// picPreView
			// 
			this.picPreView.Location = new System.Drawing.Point(16, 8);
			this.picPreView.Name = "picPreView";
			this.picPreView.Size = new System.Drawing.Size(200, 200);
			this.picPreView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picPreView.TabIndex = 0;
			this.picPreView.TabStop = false;
			// 
			// cmdChangeImage
			// 
			this.cmdChangeImage.Location = new System.Drawing.Point(232, 56);
			this.cmdChangeImage.Name = "cmdChangeImage";
			this.cmdChangeImage.Size = new System.Drawing.Size(120, 32);
			this.cmdChangeImage.TabIndex = 1;
			this.cmdChangeImage.Text = "Change Image";
			this.cmdChangeImage.Click += new System.EventHandler(this.cmdChangeImage_Click);
			// 
			// cmdApply
			// 
			this.cmdApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdApply.Location = new System.Drawing.Point(8, 216);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(104, 24);
			this.cmdApply.TabIndex = 2;
			this.cmdApply.Text = "Apply";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(264, 216);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 24);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			// 
			// frmBitmapEditor
			// 
			this.AcceptButton = this.cmdApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(384, 246);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdApply);
			this.Controls.Add(this.cmdChangeImage);
			this.Controls.Add(this.picPreView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBitmapEditor";
			this.Text = "Edit Bitmap";
			this.ResumeLayout(false);

		}
		#endregion

		private void displayImage()
		{
			if (myImg != null)
			{
				double rt = (double)myImg.Width/(double)myImg.Height;
				double ht = rt*200;
				double wt = 200/rt;
				if (myImg.Height > myImg.Width)
				{
					picPreView.Height = 200;
					picPreView.Width = (int)ht;
				}
				else
				{
					picPreView.Width = 200;
					picPreView.Height = (int)wt;
				}
			}
			picPreView.Image = myImg;
		}
		private void cmdChangeImage_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Pick a Bitmap";
			dlg.Filter = "All Bitmap Formats|*.bmp;*.jpg;*.pcx;*.gif";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				myImg = new Bitmap(dlg.FileName);
				displayImage();
			}
		}
	}
}
