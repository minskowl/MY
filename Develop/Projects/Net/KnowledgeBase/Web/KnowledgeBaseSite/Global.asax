<%@ Import Namespace="KnowledgeBase.BussinesLayer.Core" %>
<%@ Import Namespace="KnowledgeBase.Mssql.Dal" %>
<%@ Import Namespace="KnowledgeBase.SiteCore" %>
<%@ Import Namespace="KnowledgeBase.SiteCore.Providers" %>
<%@ Import Namespace="log4net.Config" %>
<%@ Import Namespace="KnowledgeBase.Core" %>
<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        try
        {
            XmlConfigurator.Configure();

            Log.Site.Info("Site_Start");

            var connectionString = ConfigurationManager.ConnectionStrings["context"].ConnectionString;
            var context = new KbContext(new DalWebProvider(new MssqlFactoryProvider(connectionString)));

            context.SettingsId = AppSettings.SettingsID;

            KbContext.CurrentKb = context;

        }
        catch (Exception ex)
        {
            Log.Site.Fatal("Application_Start", ex);
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        Log.Site.Info("Application_End");

    }

    //    void Application_Error(object sender, EventArgs e)
    //    {
    //        // Code that runs when an unhandled error occurs
    //Log.Site.Fatal(Application);
    //    }

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

    protected void Application_BeginRequest(object sender, EventArgs e)
    {

    }
    protected void Application_EndRequest(object sender, EventArgs e)
    {
        ((DalWebProvider)KbContext.CurrentKb.Provider).DisposeCurrentConnection();
    }


</script>
