using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Savchin.Collection.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [XmlRoot("dictionary")]
    public class DictionaryEx<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
        //,IList<KeyValuePair<TKey, TValue>>
    {
        #region Properties
        private const string keyItem = "item";
        private const string keyKey = "key";

        private XmlSerializer keySerializer;
        private XmlSerializer KeySerializer
        {
            get { return keySerializer ?? (keySerializer = new XmlSerializer(typeof (TKey))); }
        }

        private XmlSerializer valueSerializer;
        private string keyValue = "value";

        private XmlSerializer ValueSerializer
        {
            get { return valueSerializer ?? (valueSerializer = new XmlSerializer(typeof (TValue))); }
        } 
        #endregion
       

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
   

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType !=XmlNodeType.EndElement)
            {
                reader.ReadStartElement(keyItem);

  
                reader.ReadStartElement(keyKey);
                var key = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement(keyValue);
                var value = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (var key in Keys)
            {
                writer.WriteStartElement(keyItem);

                writer.WriteStartElement(keyKey);
                KeySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement(keyValue);
                var value = this[key];
                ValueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
        #endregion

        //#region Implementation of IList<KeyValuePair<TKey,TValue>>

        ///// <summary>
        /////                     Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
        ///// </summary>
        ///// <returns>
        /////                     The index of <paramref name="item" /> if found in the list; otherwise, -1.
        ///// </returns>
        ///// <param name="item">
        /////                     The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.
        /////                 </param>
        //int IList<KeyValuePair<TKey, TValue>>.IndexOf(KeyValuePair<TKey, TValue> item)
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        /////                     Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
        ///// </summary>
        ///// <param name="index">
        /////                     The zero-based index at which <paramref name="item" /> should be inserted.
        /////                 </param>
        ///// <param name="item">
        /////                     The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.
        /////                 </param>
        ///// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /////                 </exception>
        ///// <exception cref="T:System.NotSupportedException">
        /////                     The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /////                 </exception>
        //void IList<KeyValuePair<TKey, TValue>>.Insert(int index, KeyValuePair<TKey, TValue> item)
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        /////                     Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
        ///// </summary>
        ///// <param name="index">
        /////                     The zero-based index of the item to remove.
        /////                 </param>
        ///// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /////                 </exception>
        ///// <exception cref="T:System.NotSupportedException">
        /////                     The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /////                 </exception>
        //void IList<KeyValuePair<TKey, TValue>>.RemoveAt(int index)
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        /////                     Gets or sets the element at the specified index.
        ///// </summary>
        ///// <returns>
        /////                     The element at the specified index.
        ///// </returns>
        ///// <param name="index">
        /////                     The zero-based index of the element to get or set.
        /////                 </param>
        ///// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /////                 </exception>
        ///// <exception cref="T:System.NotSupportedException">
        /////                     The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /////                 </exception>
        //KeyValuePair<TKey, TValue> IList<KeyValuePair<TKey, TValue>>.this[int index]
        //{
        //    get { throw new System.NotImplementedException(); }
        //    set { throw new System.NotImplementedException(); }
        //}

        //#endregion
    }
}