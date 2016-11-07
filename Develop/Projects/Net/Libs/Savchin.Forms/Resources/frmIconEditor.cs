using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für frmIconEdit.
	/// </summary>
	internal class frmIconEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox picPreView;
		private System.Windows.Forms.Button cmdApply;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdChangeIcon;
		private Icon target;
		public Icon Target
		{
			get
			{
				return target;
			}
			set
			{
				target = value;
				if (target != null)
				{
					picPreView.Image = target.ToBitmap();
				}
				else
				{
					picPreView.Image = null;
				}
			}
		}
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmIconEditor()
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
			this.cmdApply = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdChangeIcon = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// picPreView
			// 
			this.picPreView.Location = new System.Drawing.Point(8, 8);
			this.picPreView.Name = "picPreView";
			this.picPreView.Size = new System.Drawing.Size(88, 72);
			this.picPreView.TabIndex = 0;
			this.picPreView.TabStop = false;
			// 
			// cmdApply
			// 
			this.cmdApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdApply.Location = new System.Drawing.Point(8, 88);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(80, 24);
			this.cmdApply.TabIndex = 2;
			this.cmdApply.Text = "Apply";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(224, 88);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			// 
			// cmdChangeIcon
			// 
			this.cmdChangeIcon.Location = new System.Drawing.Point(120, 8);
			this.cmdChangeIcon.Name = "cmdChangeIcon";
			this.cmdChangeIcon.Size = new System.Drawing.Size(136, 32);
			this.cmdChangeIcon.TabIndex = 4;
			this.cmdChangeIcon.Text = "Change Icon";
			this.cmdChangeIcon.Click += new System.EventHandler(this.cmdChangeIcon_Click);
			// 
			// frmIconEdit
			// 
			this.AcceptButton = this.cmdApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(314, 120);
			this.Controls.Add(this.cmdChangeIcon);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdApply);
			this.Controls.Add(this.picPreView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmIconEdit";
			this.Text = "Edit Icon";
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdChangeIcon_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Pick an Icon-File";
			dlg.Filter = "All Icon Files|*.ico";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Target = new Icon(dlg.FileName);
			}
		}
	}
}
