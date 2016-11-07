using System;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Savchin.Collection.Generic;
using Savchin.Core;

namespace Savchin.SystemEnvironment
{
    /// <summary>
    /// EnviromentInfo class stores informatiion about computer  for serialize into Error Report
    /// </summary>
    public class EnviromentInfo : IXmlSerializable
    {

        #region Properties
        /// <summary>
        /// Gets or sets the frameworks versions.
        /// </summary>
        /// <value>The frameworks versions.</value>
        public FrameworksVersionCollection FrameworksVersions { get; set; }

        /// <summary>
        /// Gets or sets the OS version.
        /// </summary>
        /// <value>The OS version.</value>
        public OperatingSystemInfo OSVersion { get; set; }

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        public string CurrentCulture { get; set; }

        /// <summary>
        /// Gets or sets the current UI culture.
        /// </summary>
        /// <value>The current UI culture.</value>
        public string CurrentUICulture { get; private set; }


        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public DomainInfo DomainInfo { get; private set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; private set; }


        /// <summary>
        /// Gets the pathes.
        /// </summary>
        public NameValuePair[] Pathes { get; private set; }

        #endregion

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <returns></returns>
        public static EnviromentInfo GetCurrent()
        {
            return new EnviromentInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnviromentInfo"/> class.
        /// </summary>
        internal EnviromentInfo()
        {
            FrameworksVersions = new FrameworksVersionCollection();
            OSVersion = new OperatingSystemInfo(Environment.OSVersion);
            CurrentCulture = Thread.CurrentThread.CurrentCulture.Name;
            CurrentUICulture = Thread.CurrentThread.CurrentUICulture.Name;
            DomainInfo=new DomainInfo();
            Date = DateTime.Now;
            Pathes=new NameValuePair[]
                {
                    new NameValuePair("CommandLine",Environment.CommandLine), 
                    new NameValuePair("CurrentDirectory",Environment.CurrentDirectory), 
                };
        }

        #region Implementation of IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Date", Date.ToString());
            writer.WriteAttributeString("Culture", CurrentCulture);
            writer.WriteAttributeString("UICulture", CurrentUICulture);

            writer.WriteStartElement("OSVersion");
            ((IXmlSerializable)OSVersion).WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Frameworks");
            foreach (var o in FrameworksVersions)
            {
                writer.WriteElementString("Framework", o.ToString());
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Pathes");

            if (Pathes.IsNotEmpty())
                foreach (var pair in Pathes)
                {
                    writer.WriteStartElement("Path");
                    writer.WriteAttributeString("Name", pair.Name);
                    writer.WriteAttributeString("Value", pair.Value.ToString());
                    writer.WriteEndElement();
                }
            writer.WriteEndElement();

            writer.WriteStartElement("DomainInfo");
            ((IXmlSerializable)DomainInfo).WriteXml(writer);
            writer.WriteEndElement();
        }

        #endregion
    }
}
