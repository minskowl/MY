using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Sudoku
{
    public struct Position
    {
        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public int Column { get; set; }
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row { get; set; }
    }
}
