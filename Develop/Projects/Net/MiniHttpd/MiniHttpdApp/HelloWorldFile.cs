using System;
using System.IO;
using MiniHttpd;

namespace MiniHttpdApp
{
	public class HelloWorldFile : IFile
	{
		public HelloWorldFile(string name, IDirectory parent)
		{
			this.name = name;
			this.parent = parent;
		}

		string name;
		IDirectory parent;

		public void OnFileRequested(HttpRequest request, IDirectory directory)
		{
			// Assign a MemoryStream to hold the response content.
			request.Response.ResponseContent = new MemoryStream();

			// Create a StreamWriter to which we can write some text, and write to it.
			using(StreamWriter writer = new StreamWriter(request.Response.ResponseContent))
			{
				writer.WriteLine("Hello, world!");
			}
		}

		public string ContentType
		{
			get { return ContentTypes.GetExtensionType(".txt"); }
		}
		public string Name
		{
			get { return name; }
		}

		public IDirectory Parent
		{
			get { return parent; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add HelloWorldFile.Dispose implementation
		}

		#endregion
	}
}
