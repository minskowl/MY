using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Bubbles.Core
{
    public class StrategiesCollection : Dictionary<ShiftStrategy, IShiftStrategy[]>
    {
        private readonly IShiftStrategy down = new DownShift();
        private readonly IShiftStrategy right = new RightShift();

        private readonly IShiftStrategy emptyColumn = new EmptyColumnShift();
        private readonly IShiftStrategy newColumn = new NewColumnShift();

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategiesCollection"/> class.
        /// </summary>
        public StrategiesCollection()
        {
            Add(ShiftStrategy.Standart, new[] { down, emptyColumn });
            Add(ShiftStrategy.Continuous, new[] { down, emptyColumn, newColumn });
            Add(ShiftStrategy.Shifter, new[] { down, right });
            Add(ShiftStrategy.MegaShifter, new[] { down, right, newColumn, right });
        }
    }
}
