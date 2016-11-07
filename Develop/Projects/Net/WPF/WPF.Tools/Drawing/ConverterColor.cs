using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;

namespace Savchin.Wpf.Drawing
{
    public class ConverterColor
    {



        #region ToHTMLColor
        /// <summary>
        /// Toes the color of the HTML.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static string ToHTMLColor(int r, int g, int b)
        {
            return String.Format("H{0:X}{1:X}{2:X}", r, g, b);
        }

        /// <summary>
        /// Toes the color of the HTML.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static string ToHTMLColor(Color color)
        {
            return ToHTMLColor(color.R, color.G, color.B);
        }
        /// <summary>
        /// Toes the color of the HTML.
        /// </summary>
        /// <param name="hsbColor">Color of the HSB.</param>
        /// <returns></returns>
        public static string ToHTMLColor(HSBColor hsbColor)
        {
            return ToHTMLColor(HSBColor.FromHSB(hsbColor));
        }
        #endregion






        #region To RGB Color
        /// <summary>
        /// Toes the color.
        /// </summary>
        /// <param name="htmlColor">Color of the HTML.</param>
        /// <returns></returns>
        public static Color ToColor(string htmlColor)
        {


            if (string.IsNullOrEmpty(htmlColor))
                return new Color();


            if ((htmlColor[0] == '#') && ((htmlColor.Length == 7) || (htmlColor.Length == 4)))
            {
                if (htmlColor.Length == 7)
                {
                    return Color.FromArgb(255, Convert.ToByte(htmlColor.Substring(1, 2), 0x10), Convert.ToByte(htmlColor.Substring(3, 2), 0x10), Convert.ToByte(htmlColor.Substring(5, 2), 0x10));
                }
                else
                {
                    string str = char.ToString(htmlColor[1]);
                    string str2 = char.ToString(htmlColor[2]);
                    string str3 = char.ToString(htmlColor[3]);
                    return Color.FromArgb(255, Convert.ToByte(str + str, 0x10), Convert.ToByte(str2 + str2, 0x10), Convert.ToByte(str3 + str3, 0x10));
                }
            }
            if (string.Equals(htmlColor, "LightGrey", StringComparison.OrdinalIgnoreCase))
            {
                return Colors.LightGray;
            }



            return (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(htmlColor);




        }

        /// <summary>
        /// Toes the color.
        /// </summary>
        /// <param name="hsbColor">Color of the HSB.</param>
        /// <returns></returns>
        public static Color ToColor(HSBColor hsbColor)
        {
            return hsbColor.Color;
        }
        #endregion

        #region ToHSBColor

        /// <summary>
        /// Toes the color of the HSB.
        /// </summary>
        /// <param name="HTMLColor">Color of the HTML.</param>
        /// <returns></returns>
        public static HSBColor ToHSBColor(string HTMLColor)
        {
            return HSBColor.FromColor(ToColor(HTMLColor));
        }

        /// <summary>
        /// Toes the color of the HSB.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static HSBColor ToHSBColor(Color color)
        {
            return HSBColor.FromColor(color);
        }
        #endregion
    }
}
