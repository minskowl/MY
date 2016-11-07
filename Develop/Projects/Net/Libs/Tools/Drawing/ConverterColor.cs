using System;
using System.Drawing;
using System.Globalization;

namespace Savchin.Drawing
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
        /// <param name="HTMLColor">Color of the HTML.</param>
        /// <returns></returns>
        public static Color ToColor(string HTMLColor)
        {

            return ColorTranslator.FromHtml(HTMLColor);

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
