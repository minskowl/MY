using System;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.SiteCore;
using Savchin.Core;
using Savchin.Web.Core;
using Savchin.Web.UI;

public partial class NavigationPage : PageEx
{

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        buttonRefresh.NavigateUrl = Request.Url.AbsolutePath + "?R=" + Randomizer.GetInteger();
        if (IsPostBack)
            return;

        buttonManageKeywords.Visible = buttonManageUsers.Visible = KbContext.CurrentKb.HasUserAdminPermission;
        buttonManageKeywords.Target = buttonManageUsers.Target = PageFrameset.ContentFrameName;

        buttonManageUsers.NavigateUrl = RedirectorBase.GetAbsoluteUrl(RedirectorAdmin.UsersDefaultPageUrl);
        buttonManageUsers.ImageUrl = ImagePathProvider.UsersImage;


        buttonManageKeywords.NavigateUrl = RedirectorBase.GetAbsoluteUrl(RedirectorAdmin.KeywordsDefaultPageUrl);
        buttonManageKeywords.ImageUrl = ImagePathProvider.KeysImage;
    }


    protected void ListCategoriesTreeNodeCreate(object sender, TreeNodeEventArgs e)
    {
        var node = e.Node;
        node.NavigateUrl =RedirectorBase.GetAbsoluteUrl( RedirectorAdmin.GetCategoryInfoPageUrl(int.Parse(node.Value)));
        node.Target = "ContentFrame";

    }
}
