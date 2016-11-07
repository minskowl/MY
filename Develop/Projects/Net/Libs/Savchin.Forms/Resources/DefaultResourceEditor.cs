using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für IconEditor.
	/// </summary>
	public abstract class DefaultResourceEditor:IResourceEditor
	{
		private IResourceHolder target;
		public DefaultResourceEditor()
		{
		}
		protected abstract void showEditor();
		protected virtual IResourceHolder checkTarget(IResourceHolder Value)
		{
			return Value;
		}
		#region IResourceEditor Member

		public IResourceHolder Target
		{
			get
			{
				return target;
			}
			set
			{
				target = checkTarget(value);
			}
		}

		public void ShowEditor()
		{
			showEditor();
		}

		#endregion
	}
}
