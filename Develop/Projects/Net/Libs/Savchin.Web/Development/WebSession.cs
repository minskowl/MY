using System.Web;
using Savchin.Development;

namespace Savchin.Web.Development
{
    /// <summary>
    /// WebSession
    /// </summary>
    internal class WebSession : ISession
    {
        /// <summary>
        /// Abandons this instance.
        /// </summary>
        public void Abandon()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }

        /// <summary>
        /// Saves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Save(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}