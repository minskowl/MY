using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung f�r BitmapHolder.
	/// </summary>
	[ResourceProperties(typeof(BitmapEditor),"Bitmap Resource",typeof(System.Drawing.Bitmap))]
	internal class BitmapHolder:DefaultResourceHolder
	{
		public BitmapHolder()
		{
			//
			// TODO: F�gen Sie hier die Konstruktorlogik hinzu
			//
		}
		protected override string resourceTypeName
		{
			get
			{
				return "Bitmap Resource";
			}
		}

		protected override object checkResource(object Value)
		{
			object RetVal = null;
			if (Value is System.Drawing.Bitmap)
			{
				RetVal = base.checkResource(Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not a Bitmap");
				throw up;
			}
			return RetVal;
		}
	}
}
