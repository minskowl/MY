using System;
using System.Collections.Generic;
using System.Web.UI;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.SiteCore;
using Savchin.Web.UI;

public partial class Users_Default : SitePage
{
    private readonly UserManager _manager = KbContext.CurrentKb.ManagerUser;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!KbContext.CurrentKb.HasUserAdminPermission)
        {
            RedirectorAdmin.GoToUserEditPage(KbContext.CurrentUserId);
            return;
        }
    }

    public IList<User> GetData()
    {
        return _manager.GetAll();
    }

    /// <summary>
    /// Handles the Click event of the buttonEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonEditClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToUserEditPage(gridUsers.GetIntDataKey((Control) sender));
    }

    /// <summary>
    /// Handles the Click event of the buttonRights control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonRightsClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToUserRightsPage(gridUsers.GetIntDataKey((Control)sender));
    }
    /// <summary>
    /// Handles the Click event of the buttonAddUser control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonAddUserClick(object sender, EventArgs e)
    {
        RedirectorAdmin.GoToUserAddPage();
    }
    /// <summary>
    /// Handles the Init event of the buttonRights control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ButtonRightsInit(object sender, EventArgs e)
    {
        ((ButtonEx) sender).ImageUrl = ImagePathProvider.KeyImage;
    }


}
