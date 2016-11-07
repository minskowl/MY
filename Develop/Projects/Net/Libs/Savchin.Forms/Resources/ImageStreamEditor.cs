using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für ImageStreamEditor.
	/// </summary>
	internal class ImageStreamEditor:DefaultResourceEditor
	{
		public ImageStreamEditor()
		{
		}
		protected override IResourceHolder checkTarget(IResourceHolder Value)
		{
			IResourceHolder RetVal = null;
			if (Value is ImageStreamHolder)
			{
				RetVal = base.checkTarget (Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not an ImageStreamHolder");
				throw up;
			}
			return RetVal;
		}
		protected override void showEditor()
		{
			ImageStreamHolder sth = Target as ImageStreamHolder;
			System.Windows.Forms.ImageListStreamer str = Target.Resource as System.Windows.Forms.ImageListStreamer;
			frmImageStreamEditor edt = new frmImageStreamEditor();
			if (!sth.Initialized)
			{
				edt.Stream = sth.Resource as System.Windows.Forms.ImageListStreamer;
			}
			edt.List = sth.List;
			edt.Initialized = sth.Initialized;
			if (edt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				sth.Initialized = true;
			}
			edt.closeList();
			edt.Dispose();
		}
	}
}
