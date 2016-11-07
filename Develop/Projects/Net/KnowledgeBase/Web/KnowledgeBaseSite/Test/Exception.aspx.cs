using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_Exception : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        throw new Exception("Test exception");
    }
}
