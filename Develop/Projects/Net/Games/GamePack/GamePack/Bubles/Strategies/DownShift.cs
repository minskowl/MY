using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePack.Bubles.Strategies
{
    class DownShift : IShiftStrategy
    {
        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Do(BubbleField field)
        {

            for (var column = 0; column < field.Size; column++)
            {
                ShiftColumn(field, column);
            }
        }

        /// <summary>
        /// Shifts the column.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="column">The column.</param>
        private void ShiftColumn(BubbleField field, int column)
        {
            var shiftCounter = 0;
            for (var row = field.Size - 1; row >= 0; row--)
            {
                var bubble = field[row, column];
                if (bubble == null)
                {
                    shiftCounter++;
                }
                else if (shiftCounter > 0)
                {
                    field.Move(bubble, row + shiftCounter, column);
                }

            }
        }
    }
}
