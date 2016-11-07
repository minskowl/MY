using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.Controls;
using Savchin.Text;

public partial class Test_Test1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result;
        Uri uri = new Uri("http://www.tut.by/");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        //request.UseDefaultCredentials = true;
        //request.Credentials = new NetworkCredential("Dmitry.Savchin", "kbyercXX2");
        //WebProxy proxy= new WebProxy("192.168.0.10:3128",true);
        //request.Proxy = proxy;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (Stream stream = response.GetResponseStream())
        {
            result = StringUtil.GetString(stream);
        }

        response.Close();
        labelOut.Text= result;
    }
}
