

using System ;
using System.Globalization ;
using System.Xml ;
using System.Web ;

namespace Savchin.Web.UI.HtmlEditor.FileBrowser
{
	public class Uploader : FileWorkerBase
	{
		protected override void OnLoad(EventArgs e)
		{
			this.Config.LoadConfig();

			if ( !Config.Enabled )
			{
				this.SendFileUploadResponse( 1, true, "", "", "This connector is disabled. Please check the \"editor/filemanager/connectors/aspx/config.aspx\" file." );
				return;
			}

			string sResourceType = Request.QueryString[ "Type" ];

			if ( sResourceType == null )
			{
				this.SendFileUploadResponse( 1, true, "", "", "Invalid request." );
				return;
			}

			// Check if it is an allowed type.
			if ( !Config.CheckIsTypeAllowed( sResourceType ) )
			{
				this.SendFileUploadResponse( 1, true, "", "", "Invalid resource type specified." );
				return;
			}

			this.FileUpload( sResourceType, "/", true );
		}
	}
}
