using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Savchin.Web.UI;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsDashboardEngine, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsDashboardManagerWindow, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.CssDashboard, Savchin.Web.UI.EmbeddedResources.Css, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsDashboard, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]


[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsAjax, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsJson, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsPageEx, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsOutlookPanelBar, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]



namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        internal const string JavaScript = "application/x-javascript";
        internal const string Css = "text/css";
        internal const string Gif = "img/gif";
        internal const string namespaceName = "Savchin.Web.UI.";

        internal const string JsDashboardEngine = namespaceName + "DashBoard.DashboardEngine.js";
        internal const string JsDashboardManagerWindow = namespaceName + "DashBoard.DashboardManagerWindow.js";
        internal const string CssDashboard = namespaceName + "DashBoard.Dashboard.css";
        internal const string JsDashboard = namespaceName + "DashBoard.Dashboard.js";





        internal const string JsOutlookPanelBar = namespaceName + "OutlookBar.OutlookPanelBar.js";

        internal const string JsPropertyGrid = namespaceName + "PropertyGrid.PropertyGrid.js";
        internal const string CssPropertyGrid = namespaceName + "PropertyGrid.PropertyGrid.css";

        internal const string JsAjax = namespaceName + "Ajax.js";
        internal const string JsJson = namespaceName + "Json.js";


        /// <summary>
        /// Regesters the ajax script.
        /// </summary>
        /// <param name="page">The page.</param>
        internal static void RegisterAjax(Page page)
        {
            page.ClientScript.RegisterClientScriptResource(typeof(EmbeddedResources), JsAjax);
        }

        /// <summary>
        /// Regesters the jsons script.
        /// </summary>
        /// <param name="page">The page.</param>
        internal static void RegisterJson(Page page)
        {
            page.ClientScript.RegisterClientScriptResource(typeof(EmbeddedResources), JsJson);
        }
    }
}

