using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Bashni.Game
{
    public abstract class Field : IXmlSerializable, IEquatable<Field>, ICloneable
    {
        protected int? _hashCodeCache = null;

        public int Columns { get; private set; }
        public int Rows { get; private set; }



        public abstract Brick this[int row, int column] { get; set; }

        private Field()
        {
        }


        protected Field(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

        }

        public static Field Create(int rows, int columns)
        {
            return new LightField(rows, columns);
        }

        public void Fill(Field s)
        {
            for (int rowIndex = 0; rowIndex < s.Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < s.Columns; columnIndex++)
                {
                    this[rowIndex, columnIndex] = s[rowIndex, columnIndex];
                }
            }
        }

        public byte GetProgress()
        {
            byte result = 0;


            for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                Brick prev = null;
                var lastBrickFind = false;
                for (var rowIndex = Rows - 1; rowIndex >= 0; rowIndex--)
                {
                    var b = this[rowIndex, columnIndex];
                    if (b == null) break;

                    if (prev == null || prev.Color != b.Color || prev.Width != b.Width + 1)
                    {
                        if (lastBrickFind || b.Width != Brick.MaxWidth)
                        {
                            result++;
                        }
                        else
                        {
                            lastBrickFind = true;
                        }
                    }
                    prev = b;
                }
            }

            return result;
        }

        public abstract object Clone();
        protected abstract bool IsEmpty(int rowIndex, int columnIndex);

        public void DoMove(Movement movement)
        {
            var destRow = GetTopBrick(movement.To) - 1;
            //Emtry column
            if (destRow < 0)
                destRow = Rows - 1;

            for (int rowIndex = movement.From.Row; rowIndex >= 0; rowIndex--, destRow--)
            {
                var b = this[rowIndex, movement.From.Column];
                if (b == null) break;
                this[destRow, movement.To] = b;
                this[rowIndex, movement.From.Column] = null;
            }
        }

        public int GetMoveBrick(int columnIndex)
        {
            Brick prev = null;
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                var b = this[rowIndex, columnIndex];
                if (b == null) continue;
                if (prev != null && (prev.Color != b.Color || prev.Width + 1 != b.Width))
                {
                    return rowIndex - 1;
                }
                prev = b;
            }
            return prev == null || prev.Width == Brick.MaxWidth ? -1 : Rows - 1;
        }



        public List<Movement> GetPossibleColumns(Place p)
        {
            var result = new List<Movement>();
            var brick = this[p.Row, p.Column];
            var useEmptyColumns = false;
            for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                //Текущая колонка
                if (p.Column == columnIndex) continue;

                var rowIndex = GetTopBrick(columnIndex);
                //В пустую колонку
                if (rowIndex == -1)
                {
                    //Целую колонку
                    if (p.Row == Rows - 1 || useEmptyColumns)
                        continue;
                    useEmptyColumns = true;
                    result.Add(new Movement(p, columnIndex));
                }
                else
                {
                    var topBrick = this[rowIndex, columnIndex];
                    if (topBrick.Color == brick.Color && topBrick.Width > brick.Width)
                        result.Add(new Movement(p, columnIndex));
                }

            }
            return result;
        }

        private int GetTopBrick(int columnIndex)
        {
            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                if (!IsEmpty(rowIndex, columnIndex))
                    return rowIndex;
            }
            return -1;
        }

        #region Implementation of IEquatable<Field>

        public bool Equals(Field other)
        {
            if (Columns != other.Columns || Rows != other.Rows)
                return false;

            for (var rowIndex = Rows - 1; rowIndex >= 0; rowIndex--)
            {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
                {
                    var b1 = this[rowIndex, columnIndex];
                    var b2 = other[rowIndex, columnIndex];
                    if (!Equals(b1, b2))
                        return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Field && Equals((Field)obj);
        }




        private static readonly MD5CryptoServiceProvider HashComputer = new MD5CryptoServiceProvider();
        private static readonly object _lock = new object();


        protected abstract byte[] ToData();
        // ReSharper disable NonReadonlyFieldInGetHashCode
        public override int GetHashCode()
        {

            if (_hashCodeCache.HasValue)
                return _hashCodeCache.Value;

            lock (_lock)
            {
                _hashCodeCache = BitConverter.ToInt32(HashComputer.ComputeHash(ToData()), 0);
            }
            return _hashCodeCache.Value;
        }
        // ReSharper restore NonReadonlyFieldInGetHashCode
        #endregion

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadToDescendant("Brick");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Brick")
            {
                var row = int.Parse(reader["r"]);
                var column = int.Parse(reader["c"]);
                var color = int.Parse(reader["color"]);
                var w = int.Parse(reader["w"]);
                this[row, column] = new Brick(color, w);
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("columns", Columns.ToString());
            writer.WriteAttributeString("rows", Rows.ToString());
            writer.WriteStartElement("Bricks");
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
                {
                    var brick = this[rowIndex, columnIndex];
                    if (brick != null)
                    {
                        writer.WriteStartElement("Brick");

                        writer.WriteAttributeString("r", rowIndex.ToString());
                        writer.WriteAttributeString("c", columnIndex.ToString());
                        writer.WriteAttributeString("color", brick.Color.ToString());
                        writer.WriteAttributeString("w", brick.Width.ToString());

                        writer.WriteEndElement();
                    }
                }
            }
            writer.WriteEndElement();
        }

        #endregion


    }
}
