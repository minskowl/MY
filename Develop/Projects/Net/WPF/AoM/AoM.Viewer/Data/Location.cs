using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoM.Viewer.Data
{
    public enum LocationType
    {
        Dark,
        Ligth
    }

    public class Location
    {
        public LocationType Type { get; set; }
        public int Act { get; set; }
        public int Part { get; set; }
        public List<Craft> Crafts { get; set; }

        public override string ToString()
        {
            return $"{Type} {Act}-{Part}";
        }
    }
}
