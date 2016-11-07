using System;

namespace Savchin.Development
{
    /// <summary>
    /// ISession
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Abandons this instance.
        /// </summary>
        void Abandon();
        
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// Saves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Save(string key, object value);
    }
}