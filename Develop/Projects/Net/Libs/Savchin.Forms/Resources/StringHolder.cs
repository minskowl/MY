using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für StringResourceHolder.
	/// </summary>
	[ResourceProperties(typeof(StringEditor),"String Resource",typeof(string))]
	internal class StringHolder:DefaultResourceHolder
	{
		public StringHolder()
		{
		}
		protected override object checkResource(object Value)
		{
			object RetVal = null;
			if (Value is string)
			{
				RetVal = base.checkResource (Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not a String");
				throw up;
			}
			return RetVal;
		}
		protected override string resourceTypeName
		{
			get
			{
				return "String Resource";
			}
		}
	}
}
