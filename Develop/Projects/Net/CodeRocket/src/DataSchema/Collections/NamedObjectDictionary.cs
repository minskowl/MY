using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Savchin.Data.Schema.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class NamedObjectDictionary<TValue> : Dictionary<string, TValue>//, IXmlSerializable
        where TValue : class, INamedObject
    {
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TValue ci in Values)
            {
                sb.Append(ci.ToString());
            }
            return sb.ToString();
        }
    }
}
