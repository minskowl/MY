namespace Bashni.Game
{
    public class Place
    {
        public int Row { get; set; }
        public int Column { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Place"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Place(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Place"/> class.
        /// </summary>
        public Place()
        {
        }



        public override string ToString()
        {
            return string.Format("[{0},{1}]", Column, Row);
        }
    }
}
