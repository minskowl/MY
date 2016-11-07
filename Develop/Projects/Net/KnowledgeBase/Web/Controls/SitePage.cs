using KnowledgeBase.SiteCore;
using Savchin.Web;
using Savchin.Web.UI;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.Controls
{
    public class SitePage : PageEx
    {
        //private KbContext _context;
        //public KbContext KbContext
        //{
        //    get
        //    {
        //        if (_context == null) _context = KbContext.Current;
        //        return _context;
        //    }
        //}

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            JavaScriptOnLoad.AppendLine(SiteJavaScriptLibrary.WindowRefreshScript);

        }
        /// <summary>
        /// Closes browser window on client side.
        /// </summary>
        public void CloseWindowOnClient()
        {
            ClientScript.RegisterStartupScript(typeof(SitePage), "close", JavaScriptLibrary.WindowCloseScript, true);
        }
        /// <summary>
        /// Shows the content.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void ShowContent(string url)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "Redirect", "\n window.parent.frames[2].location.replace('" + AppSettings.ApplicationAbsolutUrl + "/" + url + "'); \n", true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitePage"/> class.
        /// </summary>
        public SitePage()
        {
            PageIncludes.AddJavaScript(GetType(), AppSettings.ApplicationJavaScriptsUrl + "JScript.js", "JScript.js");
        }

        /// <summary>
        /// Called when register java scripts.
        /// </summary>
        protected override void OnRegisterJavaScripts()
        {
            base.OnRegisterJavaScripts();

            JavaScript.AppendLine(SiteJavaScriptLibrary.GetInitializeSettingsScript(this));

            if (SavePreviousPageUrl)
                JavaScriptOnLoad.Append(SiteJavaScriptLibrary.SaveCurrentUrlScipt);

        }

    }
}
