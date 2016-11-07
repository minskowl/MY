using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePack.Bubles.Strategies
{
    /// <summary>
    /// EmptyColumnShift
    /// </summary>
    class EmptyColumnShift : IShiftStrategy
    {

        private BubbleField _field;

        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Do(BubbleField field)
        {
            _field = field;
            var shiftCounter = 0;

            for (int column = _field.Size - 1; column >= 0; column--)
            {
                if (field.IsEmptyColumn(column))
                {
                    shiftCounter++;
                }
                else if (shiftCounter > 0)
                {
                    ShiftColumn(column, shiftCounter);
                }
            }
        }

        private void ShiftColumn(int column, int shift)
        {
            for (int row = _field.Size - 1; row >= 0; row--)
            {
                var bubble = _field[row, column];
                if (bubble == null)
                    return;

                _field.Move(bubble, row, column + shift);
            }
        }



    }
}
