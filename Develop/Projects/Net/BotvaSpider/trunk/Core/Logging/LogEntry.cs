using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using BotvaSpider.Core;
using Savchin.Core;

namespace BotvaSpider.Logging
{
    public enum LogEntryType : int
    {
        [Description(" ")]
        Debug = 0,
        [Description("Инфо")]
        Info = 1,
        [Description("Советы")]
        Suggestion = 2,
        [Description("Предупреждение")]
        Warning = 3,
        [Description("Ошибка")]
        Error = 4
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class LogEntry : IXmlSerializable
    {
        public static readonly Icon InfoIcon = new Icon(Resources.Gradient_Ok, 16, 16);
        public static readonly Icon SuggestionIcon = new Icon(Resources.Rounded_Help, 16, 16);
        public static readonly Icon WarningIcon = new Icon(Resources.Warning, 16, 16);
        public static readonly Icon ErrorIcon = new Icon(Resources.Gradient_Cancel, 16, 16);

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute]
        public LogEntryType Type { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [XmlAttribute]
        public LoggerType Source { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [XmlAttribute]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>The object.</value>
        [XmlIgnore]
        public Object Object { get; set; }



        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} {4} {1} {2} {3}",
                Date, Type, Title, Environment.NewLine, Source.GetDescription());
            if (!string.IsNullOrEmpty(Message))
            {
                builder.AppendLine(Message);
            }

            if (Object != null)
            {
                builder.AppendLine(Object.ToString());
            }

            return builder.ToString();
        }

        #region IXmlSerializable
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
            Type = EnumHelper.Parse<LogEntryType>(reader["Type"]);
            Source = EnumHelper.Parse<LoggerType>(reader["Source"]);
            Date = DateTime.Parse(reader["Date"]);
            reader.Read();
            Title = reader.ReadElementString("Title");
            Message = reader.ReadElementString("Message");
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Type", Type.ToString());
            writer.WriteAttributeString("Source", Source.ToString());
            writer.WriteAttributeString("Date", Date.ToString());
            writer.WriteElementString("Title", Title);
            if (!string.IsNullOrEmpty(Message))
                writer.WriteElementString("Message", Message);
            if (Object != null)
                writer.WriteElementString("Object", Object.ToString());

        } 
        #endregion
    }
}