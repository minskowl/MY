using System;
using Savchin.Collection.Generic;

namespace BotvaSpider.Configuration
{
    public class ImageCollection : DictionaryEx<String, String>
    {

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string Get(string key, string defaultValue)
        {
            if (ContainsKey(key))return this[key];

            Add(key, defaultValue);
            return defaultValue;


        }
    }
}
