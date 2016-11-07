using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Savchin.Web
{
    /// <summary>
    /// BrowserType
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// IE
        /// </summary>
        IE,
        /// <summary>
        /// FireFox
        /// </summary>
        FireFox
    }

    /// <summary>
    /// RequestHelper
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// Gets the type of the browser.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static BrowserType GetBrowserType(this HttpRequest request)
        {
            var browser = request.Browser;
            if (browser.Id == "mozillafirefox" || browser.Browser == "Firefox")
                return BrowserType.FireFox;
            return BrowserType.IE;


        }
    }
}
