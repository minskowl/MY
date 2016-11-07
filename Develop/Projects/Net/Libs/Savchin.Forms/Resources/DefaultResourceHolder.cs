using System;

namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für IconHolder.
	/// </summary>
	public abstract class DefaultResourceHolder:IResourceHolder
	{
		private string resourceName;
		private object resource;
		protected abstract string resourceTypeName
		{
			get;
		}
		protected virtual object getResource()
		{
			return resource;
		}
		protected virtual string checkResourceName(string Name)
		{
			return Name;
		}
		protected virtual object checkResource(object Value)
		{
			return Value;
		}
		public DefaultResourceHolder()
		{
			//
			// TODO: Fügen Sie hier die Konstruktorlogik hinzu
			//
		}
		#region IResourceHolder Member

		public string ResourceType
		{
			get
			{
				return resourceTypeName;
			}
		}

		public string ResourceName
		{
			get
			{
				return resourceName;
			}
			set
			{
				resourceName = checkResourceName(value);
			}
		}

		public object Resource
		{
			get
			{
				return getResource();
			}
			set
			{
				resource = checkResource(value);
			}
		}

		public void WriteResource(System.Resources.ResXResourceWriter writer)
		{
			writer.AddResource(resourceName,getResource());
		}

		#endregion
	}
}
