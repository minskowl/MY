<%@ Import Namespace="System.Security.Cryptography.X509Certificates" %>
<%@ Import Namespace="System.Net.Security" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Savchin.Web.Development" %>
<%@ Import Namespace="Savchin.Development" %>
<%@ Import Namespace="log4net.Config" %>
<%@ Import Namespace="Site.Core" %>
<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        XmlConfigurator.Configure();
        SiteContext.Current.Provider = new WebProvider();

    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
    {
        SiteContext.Current.DisposeCurrentConnection();
        SiteContext.Current.SavePesistentContext();
    }
</script>

