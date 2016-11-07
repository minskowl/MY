using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Savchin.Wpf.Core
{
    /// <summary>
    /// FrameworkElementHelper
    /// </summary>
    public static class FrameworkElementHelper
    {
        /// <summary>
        /// Toes the image.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static Image ToImage(this FrameworkElement obj)
        {
            var bmp = new RenderTargetBitmap((int)obj.ActualWidth, (int)obj.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(obj);


            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (var myStream = new MemoryStream())
            {
                encoder.Save(myStream);
                return Image.FromStream(myStream);
            }
        }
    }
}
