using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Bubbles.Controls;

namespace Savchin.Bubbles.Core
{
    internal class RightShift : IShiftStrategy
    {
        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Do(BubbleField field)
        {

            for (int row = field.Size - 1; row >= 0; row--)
            {
                ShiftRow(field, row);
            }
        }

        private void ShiftRow(BubbleField field, int row)
        {
            var shiftCounter = 0;
            for (int column = field.Size - 1; column >= 0; column--)
            {
                var bubble = field[row, column];
                if (bubble == null)
                {
                    shiftCounter++;
                }
                else
                {
                    if (shiftCounter > 0)
                        FieldControl.SetColumn(bubble, column + shiftCounter);
                }
            }
        }
    }
}
