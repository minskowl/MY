#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;

namespace KnowledgeBase.SiteCore
{
    /// <summary>
    /// Application Settings
    /// </summary>
    public static class AppSettings
    {
        public const int DashboardDefaultMaxHeight = 200;

        private static readonly NameValueCollection settings;

        /// <summary>
        /// Initializes the <see cref="AppSettings"/> class.
        /// </summary>
        static AppSettings()
        {
            settings = ConfigurationManager.AppSettings;
            settingsID = byte.Parse(settings.Get("SettingsID"));
            appVersion = settings.Get("AppVersion");
            showExceptions = bool.Parse(settings.Get("ShowExceptions"));
            jsDebugMode = bool.Parse(settings.Get("JsDebugMode"));

        }


        #region URLs


        /// <summary>
        /// Gets the tutorials base URL.
        /// </summary>
        /// <value>The tutorials base URL.</value>
        public static string TutorialsBaseUrl
        {
            get { return settings.Get("TutorialsBaseUrl"); }
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <value>The current URL.</value>
        public static string CurrentURL
        {
            get { return GetCookieString("CurrentURL"); }
        }

        public static string ApplicationAbsolutUrl
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Host + ApplicationUrl;
            }
        }
        /// <summary>
        /// Returns application path
        /// </summary>
        public static string ApplicationUrl
        {
            get
            {
                return HttpContext.Current.Request.ApplicationPath == "/" ?
                    "" :
                    HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// Gets the file base URL.
        /// </summary>
        /// <value>The file base URL.</value>
        public static string FileBaseUrl
        {
            get { return ApplicationUrl + "/File.ashx?ID="; }
        }
        /// <summary>
        /// Gets the image provider base URL.
        /// </summary>
        /// <value>The image provider base URL.</value>
        public static string ImageProviderBaseUrl
        {
            get { return ApplicationUrl + "/Image.ashx"; }
        }
        public static string ImageProviderBaseAbsolutUrl
        {
            get { return ApplicationAbsolutUrl + "/Image.ashx"; }
        }
        /// <summary>
        /// Gets the application images URL.
        /// </summary>
        /// <value>The application images URL.</value>
        public static string ApplicationImagesUrl
        {
            get { return ApplicationUrl + "/images/"; }
        }
        /// <summary>
        /// Gets the application java scripts URL.
        /// </summary>
        /// <value>The application java scripts URL.</value>
        public static string ApplicationJavaScriptsUrl
        {
            get { return ApplicationUrl + "/scripts/"; }
        }
        #endregion

        private static readonly byte settingsID;
        /// <summary>
        /// Gets the settings ID.
        /// </summary>
        /// <value>The settings ID.</value>
        public static byte SettingsID
        {
            get { return settingsID; }
        }

        private static readonly bool jsDebugMode;
        /// <summary>
        /// Gets a value indicating whether [js debug mode].
        /// </summary>
        /// <value><c>true</c> if [js debug mode]; otherwise, <c>false</c>.</value>
        public static bool JsDebugMode
        {
            get { return jsDebugMode; }
        }
        private static readonly bool showExceptions;

        /// <summary>
        /// Gets the show exceptions.
        /// </summary>
        /// <value>The show exceptions.</value>
        public static bool ShowExceptions
        {
            get { return showExceptions; }
        }

        private static readonly string appVersion;
        /// <summary>
        /// Gets the app version.
        /// </summary>
        /// <value>The app version.</value>
        public static string AppVersion
        {
            get { return appVersion; }
        }


        #region Pathes

        /// <summary>
        /// Gets the current path.
        /// </summary>
        /// <value>The current path.</value>
        public static string CurrentPath
        {
            get { return HttpContext.Current.Server.MapPath("."); }
        }
        private static string applicationPath;

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <value>The application path.</value>
        public static string ApplicationPath
        {
            get
            {
                if (applicationPath == null) applicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                return applicationPath;
            }
        }

        private static string chartPath;
        /// <summary>
        /// Gets the chart path.
        /// </summary>
        /// <value>The chart path.</value>
        public static string ChartPath
        {
            get
            {
                if (chartPath == null)
                    chartPath = ApplicationPath + "\\WebCharts\\";
                return chartPath;
            }
        }




        #endregion




        /// <summary>
        /// Gets the height of the form scroll area.
        /// </summary>
        /// <value>The height of the form scroll area.</value>
        public static Unit? FormScrollAreaHeight
        {
            get { return GetCookieUnit("FormScrollAreaHeight"); }
        }
        /// <summary>
        /// Gets the height of the page scroll area.
        /// </summary>
        /// <value>The height of the page scroll area.</value>
        public static Unit? PageScrollAreaHeight
        {
            get { return GetCookieUnit("PageScrollAreaHeight"); }
        }
        /// <summary>
        /// Gets the width of the page scroll area.
        /// </summary>
        /// <value>The width of the page scroll area.</value>
        public static Unit? PageScrollAreaWidth
        {
            get { return GetCookieUnit("PageScrollAreaWidth"); }
        }

        /// <summary>
        /// Gets the width of the panel.
        /// </summary>
        /// <value>The width of the panel.</value>
        public static string PanelWidth
        {
            get { return GetCookieString("PanelWidth"); }
        }

        /// <summary>
        /// Gets the WAM user site map provider.
        /// </summary>
        /// <value>The WAM user site map provider.</value>
        public static SiteMapProvider WAMUserSiteMapProvider
        {
            get
            {
                return SiteMap.Providers["WAMUserSiteMapProvider"];
            }
        }



        /// <summary>
        /// Gets the image file absolute URL.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static string GetImageFileAbsoluteUrl(IFile file)
        {
            return ImageProviderBaseAbsolutUrl + "?" + file.QueryString;
        }

        #region Helpers
        /// <summary>
        /// Gets the cookie unit.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static Unit? GetCookieUnit(string key)
        {
            string height = GetCookieString(key);
            if (string.IsNullOrEmpty(height))
                return null;
            try
            {
                return Unit.Parse(height);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the cookie string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetCookieString(string key)
        {
            if (HttpContext.Current == null)
                return null;


            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
                return null;

            return HttpUtility.UrlDecode(cookie.Value);

        }
        #endregion



    }
}
