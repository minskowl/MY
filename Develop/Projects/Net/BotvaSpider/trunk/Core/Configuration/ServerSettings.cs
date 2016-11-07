using System.ComponentModel;
using System.Net;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ServerSettings
    {
        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; set; }
        /// <summary>
        /// Gets or sets the accept.
        /// </summary>
        /// <value>The accept.</value>
        public string Accept { get; set; }
        /// <summary>
        /// Gets or sets the referer.
        /// </summary>
        /// <value>The referer.</value>
        public string Referer { get; set; }

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
        public string Cookie { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSettings"/> class.
        /// </summary>
        public ServerSettings()
        {
        }
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ServerSettings Create()
        {
            return new ServerSettings
                       {
                           UserAgent =
                               "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.0.6) Gecko/2009011913 Firefox/3.0.6 (.NET CLR 3.5.30729)",
                           Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                           Referer = "http://www.botva-online.ru/",
                           Cookie = "PHPSESSID=20b3a7a0c793a92292e2406b428aa4f3"
                       };
        }

        /// <summary>
        /// Creates the request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public HttpWebRequest CreateRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.Accept = Accept;

            request.Referer = Referer;
            //request.Connection = "keep-alive";
            //request.Headers.Add("Accept-Language", "ru,en-us;q=0.7,en;q=0.3");
            //request.Headers.Add("Accept-Encoding", "gzip,deflate");
            //request.Headers.Add("Accept-Charset", "windows-1251,utf-8;q=0.7,*;q=0.7");
            //request.Headers.Add("Keep-Alive", "300");

            request.Headers.Add("Cookie", Cookie);
            return request;
        }
    }
}