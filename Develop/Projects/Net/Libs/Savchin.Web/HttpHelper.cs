using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Savchin.Text;

namespace Savchin.Web
{
    public static class HttpHelper
    {
        /// <summary>
        /// Expires the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        public static void ExpireCookie(string cookieName)
        {

            HttpContext.Current.Response.Cookies.Remove(cookieName);

            HttpCookie cookie = new HttpCookie(cookieName, "");
            cookie.Expires = DateTime.Now.AddDays(-5);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            string result;
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                result = StringUtil.GetString(stream);
            }

            response.Close();
            return result;
        }
    }
}
