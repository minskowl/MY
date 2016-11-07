using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Bubbles.Core
{
    public enum BubbleStatus
    {
        Selected,
        Killed,
        Normal
    }

    public enum BubbleColor: int 
    {
        Empty=0,
        Red=1,
        Green=2,
        Blue=3,
        Yellow=4,
        Violet=5,
    }
    public enum ShiftStrategy : short
    {
        Standart=0,
        Continuous=1,
        Shifter=2,
        MegaShifter=3
    }
}
