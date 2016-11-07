using System.Collections.Generic;
using System.ComponentModel;

namespace Advertiser.Entities
{
    public enum AdvState
    {
        New = 0,
        Published = 1
    }


    public class Advertisement : Entity
    {
        public AdvState State { get; set; }


        public List<int> Phones { get; set; }

        public Advertisement()
        {
       
            Phones = new List<int>();
        }
    }
}