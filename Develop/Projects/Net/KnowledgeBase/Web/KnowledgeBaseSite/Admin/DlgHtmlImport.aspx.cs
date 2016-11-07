using System;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Services;
using KnowledgeBase.SiteCore;
using Savchin.Text;
using Savchin.Web.UI;

public partial class Admin_DlgHtmlImport : PageEx
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Gets the HTML.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string GetHtml(string url)
    {
        WebClient webClient = new WebClient();
        try
        {
            string result=webClient.DownloadString(url);
            JavaScriptSerializer serializer= new JavaScriptSerializer();
            return StringUtil.Limit(result, serializer.MaxJsonLength);

        }
        catch (Exception ex)
        {
            Log.Site.Error("Import Html fail", ex);
            return StringUtil.ToString(ex);
        }

    }
}
