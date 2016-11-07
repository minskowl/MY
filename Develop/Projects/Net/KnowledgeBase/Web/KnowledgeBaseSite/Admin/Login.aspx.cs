using System;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Controls;
using KnowledgeBase.SiteCore;

public partial class Login : SitePage
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
        JavaScript.AppendLine(SiteJavaScriptLibrary.KillFrameScript);
    }
    /// <summary>
    /// Handles the LoggedIn event of the Login1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        User currentUser = GetLoginedUser();
        if (currentUser == null)
            return;
        KbContext.CurrentKb.Login(currentUser);
    }


    /// <summary>
    /// Gets the logined user.
    /// </summary>
    /// <returns></returns>
    private User GetLoginedUser()
    {
        return GetUser(Login1.UserName, Login1.Password);
    }

    private User GetUser(string userName, string password)
    {
        User entity = KbContext.CurrentKb.ManagerUser.GetByLogin(userName);
        if (entity != null && entity.Password == password)
            return entity;

        return null;
    }
}
