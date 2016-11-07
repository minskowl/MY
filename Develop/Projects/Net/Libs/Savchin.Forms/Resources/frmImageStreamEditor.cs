using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für frmImageStreamEditor.
	/// </summary>
	internal class frmImageStreamEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ImageList imlEditor;
		private System.Windows.Forms.Label lblImageWidth;
		private System.Windows.Forms.TextBox txtImageWidth;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtImageHeight;
		private System.Windows.Forms.Button cmdInitList;
		private System.Windows.Forms.ListBox lstImages;
		private System.Windows.Forms.Button cmdAddImage;
		private System.Windows.Forms.Button cmdChangeImage;
		private System.Windows.Forms.Button cmdRemoveImage;
		private System.Windows.Forms.Button cmdApply;
		private System.Windows.Forms.Button cmdCancel;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PictureBox picPreview;
		private ImageListStreamer stream;
		private bool initialized;
		public bool Initialized
		{
			get
			{
				return initialized;
			}
			set
			{
				initialized = value;
				if (initialized)
				{
					enableButtons();
					ReloadList();
				}
			}
		}
		public ImageListStreamer Stream 
		{
			get
			{
				return imlEditor.ImageStream;
			}
			set
			{
				stream = value;
			}
		}
		public System.Windows.Forms.ImageList List
		{
			get
			{
				return imlEditor;
			}
			set
			{
				imlEditor = value;
				txtImageHeight.Text = imlEditor.ImageSize.Height.ToString();
				txtImageWidth.Text = imlEditor.ImageSize.Width.ToString();
			}
		}
		public frmImageStreamEditor()
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
			this.components = new System.ComponentModel.Container();
			this.lblImageWidth = new System.Windows.Forms.Label();
			this.txtImageWidth = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtImageHeight = new System.Windows.Forms.TextBox();
			this.cmdInitList = new System.Windows.Forms.Button();
			this.lstImages = new System.Windows.Forms.ListBox();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.cmdAddImage = new System.Windows.Forms.Button();
			this.cmdChangeImage = new System.Windows.Forms.Button();
			this.cmdRemoveImage = new System.Windows.Forms.Button();
			this.cmdApply = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblImageWidth
			// 
			this.lblImageWidth.Location = new System.Drawing.Point(8, 8);
			this.lblImageWidth.Name = "lblImageWidth";
			this.lblImageWidth.Size = new System.Drawing.Size(104, 24);
			this.lblImageWidth.TabIndex = 0;
			this.lblImageWidth.Text = "Default Imagewidth:";
			// 
			// txtImageWidth
			// 
			this.txtImageWidth.Location = new System.Drawing.Point(120, 8);
			this.txtImageWidth.Name = "txtImageWidth";
			this.txtImageWidth.Size = new System.Drawing.Size(56, 20);
			this.txtImageWidth.TabIndex = 1;
			this.txtImageWidth.Text = "16";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Default Imageheight:";
			// 
			// txtImageHeight
			// 
			this.txtImageHeight.Location = new System.Drawing.Point(120, 40);
			this.txtImageHeight.Name = "txtImageHeight";
			this.txtImageHeight.Size = new System.Drawing.Size(56, 20);
			this.txtImageHeight.TabIndex = 3;
			this.txtImageHeight.Text = "16";
			// 
			// cmdInitList
			// 
			this.cmdInitList.Location = new System.Drawing.Point(184, 40);
			this.cmdInitList.Name = "cmdInitList";
			this.cmdInitList.Size = new System.Drawing.Size(120, 23);
			this.cmdInitList.TabIndex = 4;
			this.cmdInitList.Text = "Set and load Stream";
			this.cmdInitList.Click += new System.EventHandler(this.cmdInitList_Click);
			// 
			// lstImages
			// 
			this.lstImages.Location = new System.Drawing.Point(8, 72);
			this.lstImages.Name = "lstImages";
			this.lstImages.Size = new System.Drawing.Size(120, 173);
			this.lstImages.TabIndex = 5;
			this.lstImages.SelectedIndexChanged += new System.EventHandler(this.lstImages_SelectedIndexChanged);
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(136, 72);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(88, 72);
			this.picPreview.TabIndex = 6;
			this.picPreview.TabStop = false;
			// 
			// cmdAddImage
			// 
			this.cmdAddImage.Enabled = false;
			this.cmdAddImage.Location = new System.Drawing.Point(136, 152);
			this.cmdAddImage.Name = "cmdAddImage";
			this.cmdAddImage.Size = new System.Drawing.Size(96, 23);
			this.cmdAddImage.TabIndex = 7;
			this.cmdAddImage.Text = "Add Image";
			this.cmdAddImage.Click += new System.EventHandler(this.cmdAddImage_Click);
			// 
			// cmdChangeImage
			// 
			this.cmdChangeImage.Enabled = false;
			this.cmdChangeImage.Location = new System.Drawing.Point(136, 176);
			this.cmdChangeImage.Name = "cmdChangeImage";
			this.cmdChangeImage.Size = new System.Drawing.Size(96, 23);
			this.cmdChangeImage.TabIndex = 8;
			this.cmdChangeImage.Text = "Change Image";
			this.cmdChangeImage.Click += new System.EventHandler(this.cmdChangeImage_Click);
			// 
			// cmdRemoveImage
			// 
			this.cmdRemoveImage.Enabled = false;
			this.cmdRemoveImage.Location = new System.Drawing.Point(136, 200);
			this.cmdRemoveImage.Name = "cmdRemoveImage";
			this.cmdRemoveImage.Size = new System.Drawing.Size(96, 23);
			this.cmdRemoveImage.TabIndex = 9;
			this.cmdRemoveImage.Text = "Remove Image";
			this.cmdRemoveImage.Click += new System.EventHandler(this.cmdRemoveImage_Click);
			// 
			// cmdApply
			// 
			this.cmdApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdApply.Enabled = false;
			this.cmdApply.Location = new System.Drawing.Point(8, 256);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(88, 24);
			this.cmdApply.TabIndex = 10;
			this.cmdApply.Text = "Apply";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(224, 256);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(80, 24);
			this.cmdCancel.TabIndex = 11;
			this.cmdCancel.Text = "Cancel";
			// 
			// frmImageStreamEditor
			// 
			this.AcceptButton = this.cmdApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(312, 286);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdApply);
			this.Controls.Add(this.cmdRemoveImage);
			this.Controls.Add(this.cmdChangeImage);
			this.Controls.Add(this.cmdAddImage);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lstImages);
			this.Controls.Add(this.cmdInitList);
			this.Controls.Add(this.txtImageHeight);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtImageWidth);
			this.Controls.Add(this.lblImageWidth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmImageStreamEditor";
			this.Text = "Edit Image Stream";
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdInitList_Click(object sender, System.EventArgs e)
		{
			imlEditor.ImageSize = new Size(int.Parse(txtImageWidth.Text),int.Parse(txtImageHeight.Text));
			imlEditor.ImageStream = stream;
			enableButtons();
			ReloadList();
		}
		private void enableButtons()
		{
			cmdInitList.Enabled = false;
			cmdApply.Enabled = true;
			cmdAddImage.Enabled = true;
			cmdChangeImage.Enabled = true;
			cmdRemoveImage.Enabled = true;
			cmdCancel.Enabled = false;
			this.ControlBox = false;
		}
		private void ReloadList()
		{
			lstImages.Items.Clear();
			for (int i = 0; i< imlEditor.Images.Count; i++)
			{
				lstImages.Items.Add(string.Format("Image {0}",i+1));
			}
			picPreview.Image = null;
		}
		private void lstImages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstImages.SelectedIndex != -1)
			{
				picPreview.Image = imlEditor.Images[lstImages.SelectedIndex];
			}
			else
			{
				picPreview.Image = null;
			}
		}

		private void cmdAddImage_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Pick an Image File";
			dlg.Filter = "All Image Files|*.Ico;*.Bmp;*.Jpg;*.png";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.FileName.ToLower().EndsWith(".ico"))
				{
					imlEditor.Images.Add(new Icon(dlg.FileName));
				}
				else
				{
					imlEditor.Images.Add(new Bitmap(dlg.FileName));
				}
			}
			ReloadList();
		}
		private void cmdChangeImage_Click(object sender, System.EventArgs e)
		{
			if (lstImages.SelectedIndex != -1)
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Title = "Pick an Image File";
				dlg.Filter = "All Image Files|*.Ico;*.Bmp;*.Jpg;*.png";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					if (dlg.FileName.ToLower().EndsWith(".ico"))
					{
						imlEditor.Images[lstImages.SelectedIndex] = new Icon(dlg.FileName).ToBitmap();
					}
					else
					{
						imlEditor.Images[lstImages.SelectedIndex] = new Bitmap(dlg.FileName);
					}
				}
			}
			ReloadList();
		}

		private void cmdRemoveImage_Click(object sender, System.EventArgs e)
		{
			if (lstImages.SelectedIndex != -1)
			{
				imlEditor.Images.RemoveAt(lstImages.SelectedIndex);
			}
			ReloadList();
		}
		public void closeList()
		{
			imlEditor = null;
		}
	}
}
