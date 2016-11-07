using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A class to render a value that contains a bitwise-OR'ed collection of values.
    /// </summary>
    /// <typeparam name="T">The type of value that holds the bit-OR'ed flag</typeparam>
    public class FlagRenderer<T> : BaseRenderer //where T : IConvertible
    {
        private readonly Dictionary<Int32, Object> imageMap = new Dictionary<Int32, object>();
        private readonly List<Int32> keysInOrder = new List<Int32>();

        /// <summary>
        /// Register the given image to the given value
        /// </summary>
        /// <param name="key">When this flag is present...</param>
        /// <param name="imageSelector">...draw this image</param>
        public void Add(T key, Object imageSelector)
        {
            Int32 k2 = ((IConvertible) key).ToInt32(NumberFormatInfo.InvariantInfo);

            imageMap[k2] = imageSelector;
            keysInOrder.Remove(k2);
            keysInOrder.Add(k2);
        }

        /// <summary>
        /// Draw the flags
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            Int32 v2 = ((IConvertible) Aspect).ToInt32(NumberFormatInfo.InvariantInfo);

            Point pt = r.Location;
            foreach (Int32 key in keysInOrder)
            {
                if ((v2 & key) == key)
                {
                    Image image = GetImage(imageMap[key]);
                    if (image != null)
                    {
                        g.DrawImage(image, pt);
                        pt.X += (image.Width + Spacing);
                    }
                }
            }
        }
    }
}