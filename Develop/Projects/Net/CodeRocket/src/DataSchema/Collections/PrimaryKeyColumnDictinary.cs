using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Savchin.Data.Schema.Collections
{
    /// <summary>
    /// PrimaryKeyColumnDictionary
    /// </summary>
    public class PrimaryKeyColumnDictionary : Dictionary<string, ColumnSchema>, IXmlSerializable
    {


        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string columnName=reader.ReadElementString("Column");

        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (string key in Keys)
            {
                writer.WriteElementString("Column", key);
            }
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ColumnSchema ci in Values)
            {
                sb.Append(ci.ToString());
            }
            return sb.ToString();
        }

    }
}
