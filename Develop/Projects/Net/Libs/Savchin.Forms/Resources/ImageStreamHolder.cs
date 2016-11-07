using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für ImageStreamHolder.
	/// </summary>
	[ResourceProperties(typeof(ImageStreamEditor),"Image Stream",typeof(System.Windows.Forms.ImageListStreamer))]
	internal class ImageStreamHolder:DefaultResourceHolder
	{
		private System.Windows.Forms.ImageList list;
		private bool initialized = false;
		internal bool Initialized
		{
			get
			{
				return initialized;
			}
			set
			{
				initialized = value;
			}
		}
		internal System.Windows.Forms.ImageList List
		{
			get
			{
				return list;
			}
		}
		protected override object getResource()
		{
			object RetVal = base.getResource();
			if (initialized || RetVal == null)
			{
				RetVal = list.ImageStream;
			}
			return RetVal;
		}

		public ImageStreamHolder()
		{
			list = new System.Windows.Forms.ImageList();
		}
		protected override object checkResource(object Value)
		{
			object RetVal = null;
			if (Value is System.Windows.Forms.ImageListStreamer)
			{
				RetVal = base.checkResource (Value);
			}
			else
			{
				InvalidCastException up = new InvalidCastException("Value is not ImageListStream!");
				throw up;
			}
			return RetVal;
		}
		protected override string resourceTypeName
		{
			get
			{
				return "Image Stream";
			}
		}
	}
}
