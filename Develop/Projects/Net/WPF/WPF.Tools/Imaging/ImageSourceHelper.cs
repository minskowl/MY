using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Savchin.Wpf.Imaging
{
    public static class ImageSourceHelper
    {
        /// <summary>
        /// Toes the source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static BitmapSource ToImageSource(this System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// Toes the source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static BitmapSource ToImageSource(this System.Drawing.Icon source)
        {
            return ToImageSource(source.ToBitmap());
        }

        /// <summary>
        /// Toes the source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static BitmapSource ToImageSource(this string source)
        {
            return new BitmapImage(new Uri(source));
        }

    }
}
