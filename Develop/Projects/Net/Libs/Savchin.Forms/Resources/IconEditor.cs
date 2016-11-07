using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für IconEditor.
	/// </summary>
	internal class IconEditor:DefaultResourceEditor
	{
		public IconEditor()
		{
		}
		protected override IResourceHolder checkTarget(IResourceHolder Value)
		{
			IResourceHolder RetVal = null;
			if (Value is IconHolder)
			{
				RetVal = Value;
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not IconEditor");
				throw up;
			}
			return RetVal;
		}
		protected override void showEditor()
		{
			frmIconEditor editor = new frmIconEditor();
			editor.Target = base.Target.Resource as System.Drawing.Icon;
			if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Target.Resource = editor.Target;
			}
			editor.Dispose();
		}
	}
}
