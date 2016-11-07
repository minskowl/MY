
using System;

namespace Savchin.Web.UI.HtmlEditor.FileBrowser
{
	public class TypeConfigList
	{
		private FileWorkerBase _FileWorker;
		private System.Collections.Hashtable _Types;

		public TypeConfigList( FileWorkerBase fileWorker )
		{
			_FileWorker = fileWorker;

			_Types = new System.Collections.Hashtable( 4 );
		}

		public TypeConfig this[ string typeName ]
		{
			get
			{
				if ( !_Types.Contains( typeName ) )
					_Types[ typeName ] = new TypeConfig( _FileWorker );

				return (TypeConfig)_Types[ typeName ];
			}
		}
	}
}
