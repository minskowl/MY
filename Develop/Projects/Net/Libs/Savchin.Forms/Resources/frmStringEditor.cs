using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für frmStringEditor.
	/// </summary>
	internal class frmStringEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtrText;
		private System.Windows.Forms.Button cmdApply;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public string RText
		{
			get
			{
				return txtrText.Text;
			}
			set
			{
				txtrText.Text = value;
			}
		}
		public frmStringEditor()
		{
			InitializeComponent();
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
			this.txtrText = new System.Windows.Forms.TextBox();
			this.cmdApply = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtrText
			// 
			this.txtrText.AcceptsReturn = true;
			this.txtrText.AcceptsTab = true;
			this.txtrText.Location = new System.Drawing.Point(8, 8);
			this.txtrText.Multiline = true;
			this.txtrText.Name = "txtrText";
			this.txtrText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtrText.Size = new System.Drawing.Size(280, 224);
			this.txtrText.TabIndex = 0;
			this.txtrText.Text = "";
			this.txtrText.WordWrap = false;
			// 
			// cmdApply
			// 
			this.cmdApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdApply.Location = new System.Drawing.Point(8, 240);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(96, 24);
			this.cmdApply.TabIndex = 1;
			this.cmdApply.Text = "Apply";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(192, 240);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(96, 24);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			// 
			// frmStringEditor
			// 
			this.AcceptButton = this.cmdApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(292, 270);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdApply);
			this.Controls.Add(this.txtrText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmStringEditor";
			this.Text = "Edit String";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
