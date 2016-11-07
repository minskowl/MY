using System;
using System.Web.UI;
using KnowledgeBase.SiteCore;

public partial class _Default : Page
{
    /// <summary>
    /// Gets a value indicating whether [current URL is correct].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [current URL is correct]; otherwise, <c>false</c>.
    /// </value>
    public bool CurrentUrlIsCorrect
    {
        get
        {
            string url = AppSettings.CurrentURL;
            return !string.IsNullOrEmpty(url) &&
                   url.IndexOf(RedirectorAdmin.LoginPage) == -1 &&
                   url.IndexOf(RedirectorAdmin.LogoutPage) == -1 &&
                   url.IndexOf(AppSettings.ApplicationPath + "/" + RedirectorAdmin.DefaultPage) == -1 &&
                   url.IndexOf("HeaderPage.aspx") == -1 &&
                   url.IndexOf("Admin/") != -1 &&
                   url.IndexOf("Admin/Default.aspx") == -1;
        }
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(AppSettings.PanelWidth))
            frameset.NavigatePanelWidth = int.Parse(AppSettings.PanelWidth);
        frameset.PageUrl = (CurrentUrlIsCorrect) ? AppSettings.CurrentURL : "StatisticsInfo.aspx";
    }
}
