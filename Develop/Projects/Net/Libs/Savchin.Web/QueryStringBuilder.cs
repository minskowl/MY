
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Savchin.TimeManagment;

namespace Savchin.Web
{
    /// <summary>
    /// QueryString Builder
    /// </summary>
    public class QueryStringBuilder
    {
        private bool _firstValue = true;
        private readonly StringBuilder _builder = new StringBuilder();

        #region Add

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void AddRange(NameValueCollection parameters)
        {
            foreach (string name in parameters)
            {
                a(name, HttpUtility.UrlEncode(parameters[name]));
            }
        }

        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, string value)
        {
            a(name, HttpUtility.UrlEncode(value));
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, int value)
        {
            a(name, value.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, long value)
        {
            a(name, value.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, short value)
        {
            a(name, value.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, byte value)
        {
            a(name, value.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, DateTime value)
        {
            a(name, value.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void Add(string name, bool value)
        {
            a(name, value ? "1" : "0");
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, Enum value)
        {
            a(name, Enum.Format(value.GetType(),value,"d"));
        }

        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, DateRange value)
        {
            a(name + "From", value.From.ToString());
            a(name + "To", value.To.ToString());
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, long? value)
        {
            if (value.HasValue)
                Add(name, value.Value);
        }
        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, DateRange? value)
        {
            if (value.HasValue)
                Add(name, value.Value);
        }
        #endregion

        /// <summary>
        /// Builds the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static String Build(NameValueCollection parameters)
        {
            return String.Join("&",
                (from string name in parameters
                 select String.Concat(name, "=", HttpUtility.UrlEncode(parameters[name]))
                                     ).ToArray());

        }

        /// <summary>
        /// As the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        private void a(string name, string value)
        {
            if (_firstValue)
            {
                _builder.AppendFormat("?{0}={1}", name, value);
                _firstValue = false;
            }
            else
                _builder.AppendFormat("&{0}={1}", name, value);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return _builder.ToString();
        }
    }

}
