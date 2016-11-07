using WatiN.Core.Interfaces;

namespace BotvaSpider.Core
{
    internal class LinkMathcer : ICompare
    {

        private string url;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }


        /// <summary>
        /// Compares the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool Compare(string value)
        {
            return value.StartsWith(Url);
        }
    }
}