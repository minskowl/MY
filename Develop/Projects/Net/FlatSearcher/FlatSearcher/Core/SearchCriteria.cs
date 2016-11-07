using System.Xml.Serialization;

namespace FlatSearcher.Core
{
    public class SearchCriteria
    {
        public int RoomCount { get; set; }
        public string Polygons { get; set; }
        [XmlAttribute]
        public int FlatsPerPage { get; set; }
        [XmlAttribute]
        public string Town { get; set; }
        [XmlAttribute]
        public bool IgnoreFirstFloor { get; set; }
        [XmlAttribute]
        public bool IgnoreLastFloor { get; set; }

        public decimal? KitcherFrom { get; set; }
        public int? YearTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public decimal? LivingSpaceFrom { get; set; }
    }
}
