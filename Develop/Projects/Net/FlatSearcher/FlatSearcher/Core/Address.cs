using System.Xml.Serialization;

namespace FlatSearcher.Core
{
    public class Address
    {
        public string Key { get; set; }
        [XmlAttribute]
        public bool Visible { get; set; }
        [XmlAttribute]
        public bool Manual { get; set; }
    }
}
