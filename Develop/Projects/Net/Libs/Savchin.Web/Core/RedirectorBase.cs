using System;
using System.Collections.Generic;
using System.Web;
using Savchin.Text;

namespace Savchin.Web.Core
{
    public class RedirectorBase
    {
        #region Constants
        /// <summary>
        /// Root
        /// </summary>
        public const string Root = "~/";
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultPage = "Default.aspx";
        /// <summary>
        /// 
        /// </summary>
        public const string LogoutPage = "Logout.aspx";
        /// <summary>
        /// 
        /// </summary>
        public const string LoginPage = "Login.aspx";
        /// <summary>
        /// 
        /// </summary>
        public const string keyId = "ID";
        /// <summary>
        /// 
        /// </summary>
        public const string keyParentId = "ParentID";
        /// <summary>
        /// 
        /// </summary>
        public const string keyMode = "mode"; 
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectorBase"/> class.
        /// </summary>
        protected RedirectorBase() { }

        /// <summary>
        /// Gets the login page URL.
        /// </summary>
        /// <value>The login page URL.</value>
        public static string LoginPageFullUrl
        {
            get { return GetFullUrl(LoginPage); }
        }

        /// <summary>
        /// Gets the logout page URL.
        /// </summary>
        /// <value>The logout page URL.</value>
        public static string LogoutPageFullUrl
        {
            get { return GetFullUrl(LogoutPage); }
        }

        /// <summary>
        /// Determines whether [is current URL] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// 	<c>true</c> if [is current URL] [the specified URL]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCurrentUrl(string url)
        {
            return string.Compare(
                HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath,
                url,
                true) == 0;
        }

        /// <summary>
        /// Gets the absolute URL.
        /// </summary>
        /// <param name="relativUrl">The relativ URL.</param>
        /// <returns></returns>
        public static string GetAbsoluteUrl(string relativUrl)
        {
            Uri currentUrl = HttpContext.Current.Request.Url;
            return currentUrl.Scheme + "://" + currentUrl.Host + GetFullUrl(relativUrl);
        }
        /// <summary>
        /// Gets the id URL pattern.
        /// </summary>
        /// <param name="relativUrl">The relativ URL.</param>
        /// <returns></returns>
        protected static string GetIdUrlPattern(string relativUrl)
        {
            return GetAbsoluteUrl(relativUrl) + "?" + keyId + "={0}";
        }
        /// <summary>
        /// Gets the full URL.
        /// </summary>
        /// <param name="relativUrl">The relativ URL.</param>
        /// <returns>return full application URL</returns>
        public static string GetFullUrl(string relativUrl)
        {
            var appPath = HttpContext.Current.Request.ApplicationPath;
            if (relativUrl.StartsWith("~"))
                return appPath + relativUrl.Substring(1);


            if (relativUrl.StartsWith(appPath + "/"))
                return relativUrl;

            return HttpContext.Current.Request.ApplicationPath + (relativUrl.StartsWith("/") ? relativUrl : "/" + relativUrl);
        }

        /// <summary>
        /// Goes to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public static void GoTo(string url)
        {
            HttpContext.Current.Response.Redirect(url,true);
        }
        /// <summary>
        /// Ids the query string.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string IdQueryString(object id)
        {
            return BuildQueryString(keyId, id);
        }
        /// <summary>
        /// Parents the id query string.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
       public static string ParentIdQueryString(long id)
        {
            return BuildQueryString(keyParentId, id);
        }
        /// <summary>
        /// Ids the query string.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string IdQueryString(IEnumerable<long> id)
        {
            return BuildQueryString(keyId, StringUtil.Join(id, "-"));
        }
        /// <summary>
        /// Ids the mode query string.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        protected static string IdModeQueryString(long id, string mode)
        {
            return BuildQueryString(keyId, id, keyMode, mode);
        }
        /// <summary>
        /// Builds the query string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string BuildQueryString(string name, object value)
        {
            return "?" + GetKeyValuePair(name, value);
        }
        /// <summary>
        /// Builds the query string.
        /// </summary>
        /// <returns></returns>
        public static string BuildQueryString(params object[] Query)
        {
            string result = "";
            for (int i = 0; i < Query.Length; i += 2)
            {
                if (i == 0)
                    result += "?" + GetKeyValuePair((string)Query[i], Query[i + 1]);
                else
                    result += "&" + GetKeyValuePair((string)Query[i], Query[i + 1]);

            }
            return result;

        }

        private static string GetKeyValuePair(string name, object value)
        {
            if (value == null)
                return HttpUtility.UrlEncode(name) + "=";

            return HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode(value.ToString());
        }
    }
}
