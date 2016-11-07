using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für IconHolder.
	/// </summary>
	[ResourceProperties(typeof(IconEditor),"Icon Resource",typeof(System.Drawing.Icon))]
	internal class IconHolder:DefaultResourceHolder
	{
		public IconHolder()
		{
		}
		protected override string resourceTypeName
		{
			get
			{
				return "Icon";
			}
		}
		protected override object checkResource(object Value)
		{
			object RetVal = null;
			if (Value is System.Drawing.Icon)
			{
				RetVal = Value;
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Object is not an Icon");
				throw up;
			}
			return RetVal;
		}
	}
}
