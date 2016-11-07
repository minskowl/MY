using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Utils;

namespace Savchin.Sudoku
{
    [Serializable]
    public class Game
    {
        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        public Difficulty Difficulty { get; set; }
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public int[][] Values { get; set; }


        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName)
        {
            TypeSerializer<Game>.ToXmlFile(fileName, this);
        }

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static Game Load(string fileName)
        {
            return TypeSerializer<Game>.FromXmlFile(fileName);
        }
    }
}
