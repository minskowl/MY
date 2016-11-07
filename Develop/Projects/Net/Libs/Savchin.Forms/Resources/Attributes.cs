using System;

namespace Savchin.Forms.Resources
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ResourcePropertiesAttribute:Attribute
	{
		private Type editor;
		private string description;
		private Type resourceType;
		public Type Editor
		{
			get
			{
				return editor;
			}
		}
		public string Description
		{
			get
			{
				return description;
			}
		}
		public Type ResourceType
		{
			get
			{
				return resourceType;
			}
		}
		public ResourcePropertiesAttribute(Type Editor, string Description, Type ResourceType)
		{
			Type t = typeof(IResourceEditor);
			if (t.IsAssignableFrom(Editor))
			{
				editor = Editor;
			}
			else
			{
				InvalidCastException up = new InvalidCastException(string.Format("{0} is not an IResourceEditor!",Editor));
				throw up;
			}
			description = Description;
			resourceType = ResourceType;
		}
	}
}