using System;
using System.Data;
using System.Web.UI;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.SiteCore;

public partial class Keywords_Default : SitePage
{
    readonly KeywordManager _manager = KbContext.CurrentKb.ManagerKeyword;
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!KbContext.CurrentKb.PermissionSet.CanEditKeywords) GoToPreviousPage();

    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <returns></returns>
    public DataSet GetData()
    {
        return _manager.GetInfoAll();
    }

    /// <summary>
    /// Handles the Click event of the buttonAddKeyword control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonAddKeywordClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKeywordAddPage();
    }

    /// <summary>
    /// Handles the Click event of the buttonEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonEditClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToKeywordEditPage(griKeywords.GetIntDataKey((Control) sender));
    }

    /// <summary>
    /// Handles the Click event of the buttonApprove control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonApproveClick(object sender, EventArgs e)
    {
        _manager.SetStatus(griKeywords.GetIntDataKey((Control)sender),KeywordStatus.Approved);
        griKeywords.DataBind();
    }

}
