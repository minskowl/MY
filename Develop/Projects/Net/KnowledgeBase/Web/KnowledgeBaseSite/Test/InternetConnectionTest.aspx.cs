using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_InternetConnectionTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Handles the Click event of the Button1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            var client = new WebClient();
            client.Credentials = new NetworkCredential("site", "site", "ACER");
            client.Proxy = null;
            //client.UseDefaultCredentials = true;
            Literal1.Text = client.DownloadString("http://google.com");
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.ToString();
        }
    }
}
