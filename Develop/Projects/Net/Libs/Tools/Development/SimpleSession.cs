using System.Collections.Generic;

namespace Savchin.Development
{
    /// <summary>
    /// SimpleSession
    /// </summary>
    internal class SimpleSession : ISession
    {
        readonly Dictionary<string,object> _storage= new Dictionary<string, object>();


        /// <summary>
        /// Abandons this instance.
        /// </summary>
        public void Abandon()
        {
           _storage.Clear();
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            _storage.Remove(key);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _storage.ContainsKey(key) ? _storage[key] : null; 
        }

        /// <summary>
        /// Saves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Save(string key, object value)
        {
            _storage[key] = value;
        }
    }
}