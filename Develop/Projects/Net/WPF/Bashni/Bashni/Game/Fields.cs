using System;

namespace Bashni.Game
{

    public class QuickField : Field
    {
        private readonly Brick[,] _s;


        public QuickField(Field s)
            : this(s.Rows, s.Columns)
        {
            Fill(s);
        }

        public QuickField(int rows, int columns)
            : base(rows, columns)
        {
            _s = new Brick[rows, columns];
        }

        private QuickField(Brick[,] data)
            : base(data.GetLength(0), data.GetLength(1))
        {
            _s = data;
        }

        public override Brick this[int row, int column]
        {
            get { return _s[row, column]; }
            set
            {
                _s[row, column] = value;
                _hashCodeCache = null;
            }
        }

        public override object Clone()
        {
            return new QuickField((Brick[,])_s.Clone());
        }

        protected override bool IsEmpty(int rowIndex, int columnIndex)
        {
            return _s[rowIndex, columnIndex] == null;
        }

        protected override byte[] ToData()
        {
            var Rows = _s.GetLength(0);
            var Columns = _s.GetLength(1);
            var result = new byte[(Rows * Columns)];


            var index = 0;
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
                {
                    var b = _s[rowIndex, columnIndex];
                    result[index] = Brick.Pack(b);
                    index++;
                }
            }
            return result;
        }
    }

    internal class LightField : Field
    {
        
        private byte[,] _s;
        /// <summary>
        /// Initializes a new instance of the <see cref="LightField"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public LightField(Field s)
            : this(s.Rows, s.Columns)
        {
           Fill(s);
        }

        public LightField(int rows, int columns)
            : base(rows, columns)
        {
            _s = new byte[rows, columns];
        }
        private LightField(byte[,] data)
            : base(data.GetLength(0), data.GetLength(1))
        {
            _s = data;
        }
        public override Brick this[int row, int column]
        {
            get { return AsyncSolutionBuilder.UnPack(_s[row, column]); }
            set
            {
                _s[row, column] = Brick.Pack(value);
                _hashCodeCache = null;
            }
        }

        public override object Clone()
        {
            return new LightField((byte[,])_s.Clone());
        }

        protected override bool IsEmpty(int rowIndex, int columnIndex)
        {
            return _s[rowIndex, columnIndex] == 0;
        }

        protected override byte[] ToData()
        {
            var rows = _s.GetLength(0);
            var columns = _s.GetLength(1);

            var result = new byte[rows * columns];
            var index = 0;
        
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    result[index] = _s[rowIndex, columnIndex];
                    index++;
                }
            }
            return result;

        }
    }

}
