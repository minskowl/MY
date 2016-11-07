using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePack.Bubles.Strategies
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

        /// <summary>
        /// Shifts the row.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="row">The row.</param>
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
                else if (shiftCounter > 0)
                {
                    field.Move(bubble, row, column + shiftCounter);
                }
            }
        }
    }
}
