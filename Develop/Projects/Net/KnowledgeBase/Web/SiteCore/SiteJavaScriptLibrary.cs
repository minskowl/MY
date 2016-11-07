#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Web;
using System.Web.UI;
using Savchin.Web.UI;


namespace KnowledgeBase.SiteCore
{
    /// <summary>
    /// JavaScriptLibrary
    /// </summary>
    public static class SiteJavaScriptLibrary
    {
        public const string seetingsObjectName = "SiteSettings";
        /// <summary>
        /// Gets the save current URL scipt.
        /// </summary>
        /// <value>The save current URL scipt.</value>
        public static string SaveCurrentUrlScipt
        {
            get { return string.Format("\n {0}.SetCurrentURL(window.document.URL); \n", seetingsObjectName); }
        }
        /// <summary>
        /// Gets the clear current URL scipt.
        /// </summary>
        /// <value>The clear current URL scipt.</value>
        public static string ClearCurrentUrlScipt
        {
            get { return string.Format("\n {0}.SetCurrentURL(''); \n", seetingsObjectName); }
        }

        /// <summary>
        /// Gets the kill frame script.
        /// </summary>
        /// <value>The kill frame script.</value>
        public static readonly string KillFrameScript = "\n if(window.document!=window.parent.document) window.parent.location.replace(window.document.URL); \n";
        /// <summary>
        /// Gets the restor frame script.
        /// </summary>
        /// <value>The restor frame script.</value>
        public static string RestorFrameScript
        {
            get { return string.Format("\n if(window.parent==window) window.location.replace('{0}'); \n", AppSettings.ApplicationUrl); }
        }

        /// <summary>
        /// Gets the close window.
        /// </summary>
        /// <value>The close window.</value>
        public static string WindowCloseScript
        {
            get { return "\n window.close(); \n"; }
        }

        /// <summary>
        /// Gets the refresh window.
        /// </summary>
        /// <value>The refresh window.</value>
        public static string WindowRefreshScript
        {
            get { return "\n window.parent.location.reload(); \n"; }
        }

        /// <summary>
        /// Gets the PNG fix script.
        /// </summary>
        /// <value>The PNG fix script.</value>
        public static string PngFixScript
        {
            get { return "\n AlphaRender(); \n "; }
        }
        /// <summary>
        /// Initializes the settings.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static string GetInitializeSettingsScript(Page page)
        {
            return
                String.Format(
                    @"
var jsDebug={2};
var baseImageUrl='{0}'; 
var {3} = new Settings('{1}'); 
",
                    ControlHelper.GetThemebleUrl(string.Empty, page.Theme),
                    page.Request.ApplicationPath,
                    AppSettings.JsDebugMode.ToString().ToLower(),
                    seetingsObjectName);
        }
    }
}
