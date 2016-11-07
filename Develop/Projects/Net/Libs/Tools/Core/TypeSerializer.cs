using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Savchin.Core
{

    /// <summary>
    /// TypeSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class TypeSerializer<T>
    {
        #region XML Serialization
        private static XmlSerializer _serializerXml;
        /// <summary>
        /// Gets the serializer XML.
        /// </summary>
        /// <value>The serializer XML.</value>
        private static XmlSerializer SerializerXml
        {
            get { return _serializerXml ?? (_serializerXml = new XmlSerializer(typeof(T))); }
        }

        /// <summary>
        /// Froms the XML file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static T FromXmlFile(string fileName)
        {
            T result;
            using (var fs = new StreamReader(fileName))
            {
                result = (T)SerializerXml.Deserialize(fs);
            }
            return result;
        }

        /// <summary>
        /// Froms the XML string.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T FromXmlString(string xml)
        {
            T result;
            using (var fs = new StringReader(xml))
            {
                result = (T)SerializerXml.Deserialize(fs);
            }
            return result;
        }

        public static T FromXml(XmlReader reader)
        {
            return (T)SerializerXml.Deserialize(reader);
        }
        /// <summary>
        /// Toes the XML file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="obj">The obj.</param>
        public static void ToXmlFile(String fileName, object obj)
        {
            using (var writer = new StreamWriter(fileName))
            {
                SerializerXml.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Toes the XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="obj">The obj.</param>
        public static void ToXml(XmlWriter writer, object obj)
        {
            SerializerXml.Serialize(writer, obj);
        }

        /// <summary>
        /// Toes the XML.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="obj">The obj.</param>
        public static void ToXml(Stream stream, object obj)
        {
            SerializerXml.Serialize(stream, obj);
        }
        /// <summary>
        /// Puts to stream.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="stream">The stream.</param>
        public static void PutToXmlStream(Stream stream, object obj)
        {
            SerializerXml.Serialize(stream, obj);
        }
        /// <summary>
        /// Toes the XML string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string ToXmlString(object obj)
        {
            using (var writer = new StringWriter())
            {
                SerializerXml.Serialize(writer, obj);
                return writer.GetStringBuilder().ToString();
            }
        }
        #endregion

        #region Binary Serialization
        private static BinaryFormatter _binaryFormatter;
        private static BinaryFormatter BinaryFormatter
        {
            get { return _binaryFormatter ?? (_binaryFormatter = new BinaryFormatter()); }
        }
        /// <summary>
        /// Toes the binary file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="o">The o.</param>
        public static void ToBinaryFile(string fileName, object o)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                BinaryFormatter.Serialize(stream, o);
                stream.Flush();
                stream.Close();
            }
        }
        /// <summary>
        /// Toes the binary.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static byte[] ToBinary(object o)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter.Serialize(stream, o);
                stream.Flush();
                stream.Position = 0;
                return stream.GetBuffer();

            }
        }
        /// <summary>
        /// Froms the binary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static T FromBinary(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return (T)BinaryFormatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// Froms the binary file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static T FromBinaryFile(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return (T)BinaryFormatter.Deserialize(stream);
            }
        }
        #endregion

    }
}
