using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.Core;

public partial class UserControls_LoginControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        SiteContext.Current.Login(Login1.UserName, Login1.RememberMeSet);
        Redirector.GoToUserDefaultPage();
    }
}
