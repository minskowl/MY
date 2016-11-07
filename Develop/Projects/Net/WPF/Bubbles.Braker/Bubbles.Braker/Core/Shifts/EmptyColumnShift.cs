using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Bubbles.Controls;

namespace Savchin.Bubbles.Core
{
    public class EmptyColumnShift : IShiftStrategy
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

            for (int column = _field.Size- 1; column >= 0; column--)
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
            for (int row = _field.Size-1; row >=0 ; row--)
            {
                var bubble = _field[row, column];
                if (bubble == null)
                    return;

                FieldControl.SetColumn(bubble, column + shift);
            } 
        }



    }
}
