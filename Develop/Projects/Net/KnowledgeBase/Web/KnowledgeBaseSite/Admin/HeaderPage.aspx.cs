using System;
using System.Collections.Generic;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.SiteCore;

public partial class HeaderPage : SitePage
{
    /// <summary>
    /// Gets a value indicating whether [save previous page URL].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [save previous page URL]; otherwise, <c>false</c>.
    /// </value>
    protected override bool SavePreviousPageUrl
    {
        get { return false; }
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        buttonEditUser.Text = KbContext.CurrentKb.GetCurrentUser().Login;
        buttonEditUser.NavigateUrl = RedirectorAdmin.GetUserEditPageUrl(KbContext.CurrentUserId);
        buttonEditUser.Target = PageFrameset.ContentFrameName;

    }

    /// <summary>
    /// Handles the Click event of the buttonGotoCategory control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonGotoCategory_Click(object sender, EventArgs e)
    {
        string matcher = textBoxCategoryMatcher.Text.Trim();
        if (string.IsNullOrEmpty(matcher))
            return;
        int categoryId;
        if (!int.TryParse(matcher, out categoryId))
        {
            categoryId = FindCategory(matcher);
        }

        if (Identifier.IsValid(categoryId))
        {
            ShowContent(RedirectorAdmin.GetCategoryInfoPageUrl(categoryId));
        }
        else
        {
            ShowContent(RedirectorAdmin.GetSearchPageUrl(matcher));
        }
    }
    private int FindCategory(string matcher)
    {
        List<Category> categories = KbContext.CurrentKb.ManagerCategory.FindByName(matcher);

        if (categories == null || categories.Count == 0)
            return 0;

        if (categories.Count == 1)
            return categories[0].CategoryID;

        foreach (Category category in categories)
        {
            if (category.Name == matcher)
                return category.CategoryID;
        }

        return 0;
    }

    /// <summary>
    /// Handles the Click event of the buttonSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        ShowContent(RedirectorAdmin.SearchPage);
    }

    /// <summary>
    /// Handles the Click event of the buttonLogout control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void buttonLogout_Click(object sender, EventArgs e)
    {
        KbContext.CurrentKb.Logout();
    }

    protected void buttonEditUser_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
