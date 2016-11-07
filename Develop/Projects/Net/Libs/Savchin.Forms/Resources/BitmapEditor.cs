using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für BitmapEditor.
	/// </summary>
	internal class BitmapEditor:DefaultResourceEditor
	{
		public BitmapEditor()
		{
			//
			// TODO: Fügen Sie hier die Konstruktorlogik hinzu
			//
		}
		protected override IResourceHolder checkTarget(IResourceHolder Value)
		{
			IResourceHolder RetVal = null;
			if (Value is BitmapHolder)
			{
				RetVal = base.checkTarget(Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not a BitmapHolder");
				throw up;
			}
			return RetVal;
		}
		protected override void showEditor()
		{
			frmBitmapEditor tmp = new frmBitmapEditor();
			tmp.Image = Target.Resource as System.Drawing.Bitmap;
			if (tmp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Target.Resource = tmp.Image;
			}
		}

	}
}
