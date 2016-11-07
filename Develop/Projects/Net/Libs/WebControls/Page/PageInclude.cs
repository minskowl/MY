using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Savchin.Web;


namespace Savchin.Web.UI
{
    /// <summary>
    /// IncludeType
    /// </summary>
    public enum PageIncludeType
    {
        /// <summary>
        /// Css
        /// </summary>
        Css = 0,
        /// <summary>
        /// JavaScript
        /// </summary>
        JavaScript = 1
    }

    /// <summary>
    /// PageInclude
    /// </summary>
    public struct PageInclude : IEquatable<PageInclude>
    {

        #region Properties
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

        private PageIncludeType includeType;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public PageIncludeType IncludeType
        {
            get { return includeType; }
            set { includeType = value; }
        }

        private Type type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type KeyType
        {
            get { return type; }
            set { type = value; }
        }

        private string key;
        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="PageInclude"/> struct.
        /// </summary>
        /// <param name="includeType">Type of the include.</param>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        public PageInclude(PageIncludeType includeType, Type type, string key)
        {
            this.includeType = includeType;
            this.type = type;
            this.key = key;
            this.url = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PageInclude"/> struct.
        /// </summary>
        /// <param name="includeType">Type of the include.</param>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="url">The URL.</param>
        public PageInclude(PageIncludeType includeType, Type type, string key, string url)
        {
            this.includeType = includeType;
            this.type = type;
            this.key = key;
            this.url = url;
        }


        #region Equals

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override int GetHashCode()
        {
            return url.GetHashCode() ^ type.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PageInclude))
                return false;

            return Equals((PageInclude)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(PageInclude other)
        {
            if (Url != other.Url)
                return false;

            return type == other.type;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PageInclude point1, PageInclude point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PageInclude point1, PageInclude point2)
        {
            return !point1.Equals(point2);
        }
        #endregion



        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public string GetUrl(Page page)
        {
            return (string.IsNullOrEmpty(url))
                       ? page.ClientScript.GetWebResourceUrl(type, key)
                       : url;
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public string GetHtml(System.Web.UI.Page page)
        {
            string realUrl = GetUrl(page);
            return (IncludeType == PageIncludeType.Css) ?
                                                            JavaScriptBuilder.GetCssInclude(realUrl) : JavaScriptBuilder.GetJsScriptInclude(realUrl);
        }

        /// <summary>
        /// Registers the client script.
        /// </summary>
        /// <param name="page">The page.</param>
        public void Register(Page page)
        {
            if (includeType == PageIncludeType.Css)
            {
                var myHtmlLink = new HtmlLink();
                myHtmlLink.Href = GetUrl(page);
                myHtmlLink.Attributes.Add("rel", "stylesheet");
                myHtmlLink.Attributes.Add("type", "text/css");
                page.Header.Controls.Add(myHtmlLink);


            }
            else
            {
                if (string.IsNullOrEmpty(url))
                    ScriptManager.RegisterClientScriptResource(page, type, key);
                else
                    ScriptManager.RegisterClientScriptInclude(page, type, key, url);
            }
        }
    }
}
