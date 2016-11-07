using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Savchin.Collection.Generic;

namespace Savchin.SystemEnvironment
{
    public class DomainInfo : IXmlSerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainInfo"/> class.
        /// </summary>
        public DomainInfo()
        {
            MachineName = Environment.MachineName;
            var identity = WindowsIdentity.GetCurrent();
            UserName = identity.Name;
            var domaniType = typeof(NTAccount);
            Groups = identity.Groups != null ? identity.Groups.Select(gr => gr.Translate(domaniType).Value).ToArray() : new string[0];

        }

        public string MachineName { get; private set; }
        public string UserName { get; private set; }
        public string[] Groups { get; private set; }

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

         void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("MachineName", MachineName);
            writer.WriteAttributeString("UserName", UserName);


             if (Groups.IsNotEmpty())
             {
                 writer.WriteStartElement("Groups");
                 foreach (var g in Groups)
                 {
                     writer.WriteElementString("Group",g);
                 }
                 writer.WriteEndElement();
             }
           
        }
    }
}
