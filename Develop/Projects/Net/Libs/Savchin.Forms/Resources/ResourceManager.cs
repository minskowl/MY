using System;
using System.Collections;
using System.Resources;
namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für ResourceManager.
	/// </summary>
	public class ResourceManager:CollectionBase
	{
		public IResourceHolder this[int Index]
		{
			get
			{
				return List[Index] as IResourceHolder;
			}
		}
		private ClassLoader loader;
		private string fileName;
		private bool incomplete = false;
		public bool Incomplete
		{
			get
			{
				return incomplete;
			}
		}
		public ClassLoader Loader
		{
			get
			{
				return loader;
			}
		}
		private ResourceManager()
		{
			loader = new ClassLoader();
			loader.RefreshClassList();
		}
		public ResourceManager(string ResxFile):this()
		{
			System.IO.FileInfo f = new System.IO.FileInfo(ResxFile);
			if (f.Exists)
			{
				var st = new ResXResourceSet(ResxFile);
				foreach (DictionaryEntry e in st)
				{
					IResourceHolder hld = loader.CreateHolderInstance(e.Key.ToString(),e.Value);
					if (hld != null)
					{
						List.Add(hld);
					}
					else
					{
						incomplete = true;
					}
				}
				st.Close();
			}
			fileName = ResxFile;
		}
		public void Save()
		{
			if (!incomplete)
			{
				var wr = new ResXResourceWriter(fileName);
				foreach (IResourceHolder hld in List)
				{
					hld.WriteResource(wr);
				}
				wr.Generate();
				wr.Close();
			}
			else
			{
				InvalidOperationException up = new InvalidOperationException("Incomplete Resource File can not be saved!");
				throw up;
			}
		}
		public void Add(IResourceHolder Value)
		{
			List.Add(Value);
		}
		public void Remove(IResourceHolder Value)
		{
			List.Remove(Value);
		}
		public void Edit(int Index)
		{
			IResourceHolder hld = this[Index];
			Edit(hld);
		}
		public void Edit(IResourceHolder Value)
		{
			IResourceEditor editor = loader.CreateEditorInstance(Value);
			editor.ShowEditor();
		}
	}
}
