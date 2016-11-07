using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bashni.Game
{
    public class Movement
    {
        public Movement()
        {
        }

        public Movement(Place from, int to)
        {
            From = from;
            To = to;
        }

        public Place From { get; set; }
        public int To { get; set; }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", From, To);
        }
    }
}
