using System;
using System.Resources;
namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung f�r IResourceHolder.
	/// </summary>
	public interface IResourceHolder
	{
		string ResourceType{get;}
		string ResourceName{get;set;}
		object Resource{get;set;}
		void WriteResource(ResXResourceWriter writer);
	}
	public interface IResourceEditor
	{
		IResourceHolder Target{get;set;}
		void ShowEditor();
	}
}
