using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageMatcher
{
    class Package
    {
        public Hero[] Heroes { get; set; }
    }

    public class Hero
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? Evo { get; set; }
        public int Level { get; set; }
        public int Skill { get; set; }
        public bool Required { get; set; }

        public override string ToString()
        {
            return $"{Name} Эво{Evo} Ур{Level}";
        }
    }
}
