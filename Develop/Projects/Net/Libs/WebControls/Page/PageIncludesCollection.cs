
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI
{
    /// <summary>
    /// PageIncludeCollection
    /// </summary>
    public class PageIncludeCollection : IEnumerable<PageInclude>
    {
        readonly Dictionary<string, PageInclude> storage = new Dictionary<string, PageInclude>();

        /// <summary>
        /// Adds the specified include.
        /// </summary>
        /// <param name="include">The include.</param>
        public void Add(PageInclude include)
        {
            if (!storage.ContainsKey(include.Key))
            {
                storage.Add(include.Key, include);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            storage.Clear();
        }

        /// <summary>
        /// Adds the CSS.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="url">The URL.</param>
        public void AddCss(Type type,string key, string url)
        {
            Add(new PageInclude(PageIncludeType.Css, type, key, url));
        }

        /// <summary>
        /// Adds the java script.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="key">The key.</param>
        public void AddJavaScript(Type type, string key, string url)
        {
            Add(new PageInclude(PageIncludeType.JavaScript, type, key, url));
        }

        /// <summary>
        /// Adds the java script.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        public void AddJavaScript(Type type, string key)
        {
            Add(new PageInclude(PageIncludeType.JavaScript, type, key));
        }

        /// <summary>
        /// Inits the page.
        /// </summary>
        /// <param name="page">The page.</param>
        public void InitPage(Page page)
        {
            foreach (PageInclude include in storage.Values)
            {
                include.Register(page);
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator<PageInclude> IEnumerable<PageInclude>.GetEnumerator()
        {
            return storage.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<PageInclude>)this).GetEnumerator();
        }
    }
}
