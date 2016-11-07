using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Savchin.Web
{
    /// <summary>
    /// Localizator
    /// </summary>
    public static class Localizator
    {
        private static Dictionary<Key, string> _cache;
        private static Dictionary<Type, string> _keys;

        /// <summary>
        /// Initializes the <see cref="Localizator"/> class.
        /// </summary>
        static Localizator()
        {
            _cache = new Dictionary<Key, string>();
            _keys = new Dictionary<Type, string>();
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
        }

        static void CurrentDomainDomainUnload(object sender, EventArgs e)
        {

            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            try
            {
                if (_cache != null)
                {
                    _cache.Clear();
                    _cache = null;
                }
            }
            catch
            {
            }
            try
            {
                if (_keys != null)
                {

                    _keys.Clear();
                    _keys = null;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Registers the localization.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="className">Name of the class.</param>
        public static void RegisterLocalization(Type enumType, string className)
        {
            _keys.Add(enumType, className);
        }

        /// <summary>
        /// Gets the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Get(Enum value)
        {
            return Get(Thread.CurrentThread.CurrentUICulture.Name, value);
        }

        /// <summary>
        /// Gets the specified locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Get(string locale, Enum value)
        {
            return Get(new Key(locale, value));
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string Get(Key key)
        {
            lock (_cache)
            {
                if (_cache.ContainsKey(key))
                    return _cache[key];

                var classKey = _keys[key.Value.GetType()];
                var localization = (string)HttpContext.GetGlobalResourceObject(classKey, key.Value.ToString());

                _cache.Add(key, localization);
                return localization;
            }
        }


        private class Key : IEquatable<Key>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Key"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="locale">The locale.</param>
            public Key(string locale, Enum value)
            {
                Value = value;
                Locale = locale;
            }


            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public Enum Value { get; private set; }
            /// <summary>
            /// Gets or sets the locale.
            /// </summary>
            /// <value>The locale.</value>
            public string Locale { get; private set; }

            /// <summary>
            /// Equalses the specified other.
            /// </summary>
            /// <param name="other">The other.</param>
            /// <returns></returns>
            public bool Equals(Key other)
            {
                return Value == other.Value && Locale == other.Locale;
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public override int GetHashCode()
            {
                return Value.GetHashCode() ^ Locale.GetHashCode();
            }
            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            /// <returns>
            /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
            /// </returns>
            public override bool Equals(object obj)
            {
                return obj is Key && Equals((Key)obj);
            }
        }
    }
}
