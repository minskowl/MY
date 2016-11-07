using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Bubbles.Controls;

namespace Savchin.Bubbles.Core
{
    public interface IShiftStrategy
    {

        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        void Do(BubbleField field);
    }
}
