using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePack.Bubles
{
    /// <summary>
    /// IShiftStrategy
    /// </summary>
    interface IShiftStrategy
    {

        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        void Do(BubbleField field);
    }
}
