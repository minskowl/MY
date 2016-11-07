using Savchin.Bubbles.Controls;

namespace Savchin.Bubbles.Core
{
    public class DownShift : IShiftStrategy
    {
        /// <summary>
        /// Does the bubles shift.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Do(BubbleField field)
        {

            for (int column = 0; column < field.Size; column++)
            {
                ShiftColumn(field, column);
            }
        }

        private void ShiftColumn(BubbleField field, int column)
        {
            var shiftCounter = 0;
            for (int row = field.Size - 1; row >= 0; row--)
            {
                var bubble = field[row, column];
                if (bubble == null)
                {
                    shiftCounter++;
                }
                else
                {
                    if (shiftCounter > 0)
                        FieldControl.SetRow(bubble, row + shiftCounter);
                }
            }
        }
    }
}
