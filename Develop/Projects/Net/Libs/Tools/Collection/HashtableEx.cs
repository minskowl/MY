using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using Savchin.Core;

namespace Savchin.Collection
{
    public class HashtableEx : Hashtable, IXmlSerializable
    {
        #region IXmlSerializable Membres

        private Hashtable _cache;

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            try
            {    // Start to use the reader.
                reader.Read();
                // Read the first element ie root of this object
                reader.ReadStartElement("dictionary");

                // Read all elements
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    // parsing the item
                    reader.ReadStartElement("item");

                    // PArsing the key and value 
                    reader.ReadStartElement("key");
                    var key = DeSerialize(reader);
                    reader.ReadEndElement();

                    reader.ReadStartElement("value");
                    var value = DeSerialize(reader);
                    reader.ReadEndElement();
                    // en reading the item.
                    reader.ReadEndElement();
                    reader.MoveToContent();

                    // add the item
                    Add(key, value);
                }

                // Extremely important to read the node to its end.
                // next call of the reader methods will crash if not called.
                reader.ReadEndElement();
            }
            finally
            {
                _cache = null;
            }





        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            try
            {
                // Write the root elemnt 
                writer.WriteStartElement("dictionary");

                // Fore each object in this
                foreach (object key in Keys)
                {
                    object value = this[key];
                    // Write item, key and value
                    writer.WriteStartElement("item");
                    writer.WriteStartElement("key");
                    Serialize(writer, key);
                    writer.WriteEndElement();

                    writer.WriteStartElement("value");
                    Serialize(writer, value);
                    writer.WriteEndElement();

                    // write </item>
                    writer.WriteEndElement();
                }
                // write </dictionnary>
                writer.WriteEndElement();
            }
            finally
            {
                _cache = null;
            }


        }
        private object DeSerialize(XmlReader reader)
        {
            var type = reader.ReadElementString("type");
            var serializer = GetSerializer(type);
            return serializer.Deserialize(reader);
        }

        private void Serialize(XmlWriter writer, object o)
        {
            var type = o.GetType();
            writer.WriteElementString("type", type.GetTypeReference());
            var valueSerializer = GetSerializer(type);
            valueSerializer.Serialize(writer, o);
        }

        
        private XmlSerializer GetSerializer(object key)
        {
            var type = key is Type ? (Type)key : Type.GetType(key.ToString());
            XmlSerializer res;
            if (_cache == null)
            {
                _cache = new Hashtable();
                res = new XmlSerializer(type);
                _cache.Add(type, res);
                return res;
            }
            res = _cache[key] as XmlSerializer;

            if (res == null)
            {
                res = new XmlSerializer(type);
                _cache.Add(key, res);
            }
            return res;
        }

        #endregion
    }

}
