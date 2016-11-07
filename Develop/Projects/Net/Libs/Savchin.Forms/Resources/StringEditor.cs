using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für StringResourceEditor.
	/// </summary>
	internal class StringEditor:DefaultResourceEditor
	{
		public StringEditor()
		{
		}
		protected override IResourceHolder checkTarget(IResourceHolder Value)
		{
			IResourceHolder RetVal = null;
			if (Value is StringHolder)
			{
				RetVal = base.checkTarget (Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not a StringHolder");
				throw up;
			}
			return RetVal;
		}
		protected override void showEditor()
		{
			frmStringEditor sed = new frmStringEditor();
			sed.RText = Target.Resource as string;
			if (sed.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Target.Resource = sed.RText;
			}
			sed.Dispose();
		}

	}
}
