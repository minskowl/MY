using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePack.Bubles
{
    class Snapshot
    {
        public int Score { get; set; }
        public BubbleColor[,] Bubbles { get; set; }
    }
}
