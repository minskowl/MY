using System;
using System.Collections;
namespace Savchin.Forms.Resources
{
	/// <summary>
	/// Zusammenfassung für ClassLoader.
	/// </summary>
	public class ClassLoader:CollectionBase
	{
		public ClassLoaderItem this[int Index]
		{
			get
			{
				return List[Index] as ClassLoaderItem;
			}
		}
		private bool _clearOk;

		public void RefreshClassList()
		{
			_clearOk = true;
			List.Clear();
			_clearOk = false;
			Type mIF = typeof(IResourceHolder);
			System.Reflection.Assembly[] allA = AppDomain.CurrentDomain.GetAssemblies();
			foreach ( System.Reflection.Assembly a in allA)
			{
				foreach ( Type t in a.GetTypes())
				{
					if (mIF.IsAssignableFrom(t))
					{
						try
						{
							var ap = Attribute.GetCustomAttribute(t,typeof(ResourcePropertiesAttribute)) as ResourcePropertiesAttribute;
							if (ap != null)
							{
								List.Add(new ClassLoaderItem(t,ap.Editor,ap.Description, ap.ResourceType));
							}
						}
						catch{}
					}
				}
			}
		}
		protected override void OnClear()
		{
			if (_clearOk)
			{
				base.OnClear ();
			}
			else
			{
                throw new InvalidOperationException("ClassLoader must not be cleared!");
			}
		}
		protected override void OnRemove(int index, object value)
		{
			if (_clearOk)
			{
				base.OnRemove (index, value);
			}
			else
			{
                throw new InvalidOperationException("Items must not be removed manually from ClassLoader!");
			}
		}
		private int getHolderIndex(Type HolderType)
		{
			int RetVal = -1;
			for (int i = 0; i< List.Count; i++)
			{
				if ((List[i] as ClassLoaderItem).HolderType == HolderType)
				{
					RetVal = i;
					break;
				}
			}
			return RetVal;
		}
		private int getHolderfromTarget(Type HoldingType)
		{
			int RetVal = -1;
			for (int i = 0; i< List.Count; i++)
			{
				if ((List[i] as ClassLoaderItem).HoldingType == HoldingType)
				{
					RetVal = i;
					break;
				}
			}
			return RetVal;
		}
		public IResourceHolder CreateHolderInstance(int Index)
		{
			return (List[Index] as ClassLoaderItem).GetHolderInstance();
		}
		public IResourceHolder CreateHolderInstance(string Name, object Value)
		{
			IResourceHolder RetVal = null;
			int id = getHolderfromTarget(Value.GetType());
			if (id != -1)
			{
				RetVal = (List[id] as ClassLoaderItem).GetHolderInstance();
				RetVal.ResourceName = Name;
				RetVal.Resource = Value;
			}
			return RetVal;
		}
		public IResourceEditor CreateEditorInstance(IResourceHolder Target)
		{
			int id = getHolderIndex(Target.GetType());
			IResourceEditor RetVal = (List[id] as ClassLoaderItem).GetEditorInstance();
			RetVal.Target = Target;
			return RetVal;
		}
	}
	public class ClassLoaderItem
	{
		private readonly Type holderType;
		private Type editorType;
		private string typeDescription;
		private Type holdingType;
		public Type HoldingType
		{
			get
			{
				return holdingType;
			}
		}
		public Type HolderType
		{
			get
			{
				return holderType;
			}
		}
		public Type EditorType
		{
			get
			{
				return editorType;
			}
		}
		public string TypeDescription
		{
			get
			{
				return typeDescription;
			}
		}
		private ClassLoaderItem(){}
		internal ClassLoaderItem(Type HolderType, Type EditorType, string TypeDescription, Type HoldingType)
		{
			holderType = HolderType;
			editorType = EditorType;
			typeDescription = TypeDescription;
			holdingType = HoldingType;
		}
		internal IResourceHolder GetHolderInstance()
		{
			IResourceHolder RetVal = null;
			System.Reflection.ConstructorInfo ci = holderType.GetConstructor(Type.EmptyTypes);
			if (ci != null)
			{
				RetVal = ci.Invoke(null) as IResourceHolder;
			}
			return RetVal;
		}
		internal IResourceEditor GetEditorInstance()
		{
			IResourceEditor RetVal = null;
			System.Reflection.ConstructorInfo ci = editorType.GetConstructor(Type.EmptyTypes);
			if (ci != null)
			{
				RetVal = ci.Invoke(null) as IResourceEditor;
			}
			return RetVal;
		}
	}
}
