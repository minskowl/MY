using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bashni.Game
{
    public class Brick : IEquatable<Brick>
    {
        public const int MaxWidth = 7;
        public Brick()
        {
        }

        public Brick(int color, int width)
        {
            Color = color;
            Width = width;
        }

        public int Color { get; set; }
        public int Width { get; set; }

        public bool Equals(Brick other)
        {
            return Color == other.Color && Width == other.Width;
        }

        public override int GetHashCode()
        {
            return Color ^ Width;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is Brick && Equals((Brick)obj);

        }

       
        public static Brick UnPack(byte b)
        {
            return b == 0 ? null : new Brick(b & 15, b >> 4);
        }
        public static byte Pack(Brick b)
        {
            return b == null ? (byte)0 : (byte)(b.Color | (b.Width << 4));
        }


    }
}
