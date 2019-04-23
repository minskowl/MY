using System.Collections.Generic;

namespace AoM.Viewer.Data
{
    public class Hero
    {
        public int Index;
        public string Name;
        public string Fraction;
        public List<Gear> Gears;

        public override string ToString()
        {
            return Name;
        }
    }
}

