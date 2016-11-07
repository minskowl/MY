using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;
using Savchin.Text;
using Savchin.Web.UI;

public partial class Admin_DownloadImages : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod()]
    public static string DownloadImage(string url)
    {
        WebClient webClient = new WebClient();

        try
        {
            UriBuilder builder = new UriBuilder(url);

            byte[] content = webClient.DownloadData(builder.Uri);
            string fileName = Path.GetFileName(builder.Path);
            IFile file = null;
            if (KbContext.CurrentKb.CurrentKnowledgeID.HasValue)
            {
                file = KbContext.CurrentKb.ManagerFileInclude.Create(
                        KbContext.CurrentKb.CurrentKnowledgeID.Value,
                        fileName,
                        content);
            }
            else
            {
                file = KbContext.CurrentKb.ManagerUserFile.Create(fileName, content);
            }


            return file == null ? string.Empty : AppSettings.GetImageFileAbsoluteUrl(file);

        }
        catch (Exception ex)
        {
            Log.Site.Error("Download image fail", ex);
            throw;
        }

    }

}
