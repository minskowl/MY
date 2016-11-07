using Savchin.Utils;

namespace GamePack.Bubles.Strategies
{
    class NewColumnShift : IShiftStrategy
    {
        private BubbleField _field;

        public void Do(BubbleField field)
        {
            _field = field;


            for (int column = _field.Size - 1; column >= 0; column--)
            {
                if (field.IsEmptyColumn(column))
                {
                    GenerateColumn(column);
                }

            }
        }

        /// <summary>
        /// Generates the column.
        /// </summary>
        /// <param name="column">The column.</param>
        private void GenerateColumn(int column)
        {
            var count = Randomizer.GetIntegerBetween(3, _field.Size);
            for (int i = 1; i <= count; i++)
            {
                _field.AddBubble(_field.Size - i, column);
            }
        }
    }
}
