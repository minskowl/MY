using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Sudoku.Controls;

namespace Savchin.Sudoku
{
    public class Region
    {
        private Difficulty difficulty;
        private int width;
        private int height;
        private int startColumn;
        private int startRow;
        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        public Region(Difficulty difficulty, int column, int row)
        {
            this.difficulty = difficulty;
            switch (difficulty)
            {
                case Difficulty.F4:
                    width = 2;
                    height = 2;
                    break;
                case Difficulty.F6:
                    width = 3;
                    height = 2;
                    break;
                case Difficulty.F9:
                    width = 3;
                    height = 3;
                    break;
                case Difficulty.F12:
                    width = 4;
                    height = 3;
                    break;
                case Difficulty.F16:
                    width = 4;
                    height = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("difficulty");
            }
            startColumn = (column / width) * width;
            startRow = (row / height) * height;
        }

        /// <summary>
        /// Gets the positions.
        /// </summary>
        /// <value>The positions.</value>
        public IEnumerable<Position> Positions
        {
            get
            {
                var result = new List<Position>(width * height);
                for (var row = 0; row < height; row++)
                    for (var column = 0; column < width; column++)
                    {
                        result.Add(new Position { Column = column + startColumn, Row = row + startRow });
                    }
                return result;
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <param name="layoutPanel">The layout panel.</param>
        /// <returns></returns>
        public IEnumerable<Field> GetFields(TableLayoutPanel layoutPanel)
        {
            foreach (var position in Positions)
            {
                yield return (Field)layoutPanel.GetControlFromPosition(position.Column, position.Row);
            }
        }

        /// <summary>
        /// Counts the specified layout.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public int Count(TableLayoutPanel layout, Func<Field, bool> predicate)
        {
            return GetFields(layout).Count(predicate);
        }
    }
}
