using System;
using System.Collections.Specialized;

namespace Savchin.Collection
{
    /// <summary>
    /// NameValueCollectionHelper
    /// </summary>
    public static class NameValueCollectionHelper
    {
        /// <summary>
        /// Gets the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static T GetEnum<T>(this  NameValueCollection collection)
        {
            return collection.GetEnum<T>(typeof(T).Name);
        }

        /// <summary>
        /// Gets the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T GetEnum<T>(this  NameValueCollection collection, string key)
        {
            return (T)Enum.Parse(typeof(T), collection[key]);
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool GetBool(this  NameValueCollection collection, string key)
        {
            return collection.GetBool(key, false);
        }
        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public static bool GetBool(this  NameValueCollection collection, string key, bool defaultValue)
        {
            var tmp = collection[key];
            return string.IsNullOrWhiteSpace(tmp) ? defaultValue : bool.Parse(tmp);
        }
    }
}
