using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Savchin.Drawing
{
    public static class ImageHelper
    {
        /// <summary>
        /// Resizes the image file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void ResizeImageFile(string fileName, int width, int height)
        {
            using (var image = Image.FromFile(fileName))
            using (var res = image.Resize(width, height))
                res.Save(fileName, image.RawFormat);
        }

        /// <summary>
        /// Resizes the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static Image Resize(this Image image, Size size)
        {
            return Resize(image, size.Width, size.Height);
        }


        /// <summary>
        /// Resizes the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static Image Resize(this Image image, int width, int height)
        {
            var nPercentW = ((float)width / image.Width);
            var nPercentH = ((float)height / image.Height);
            return Resize(image, Math.Min(nPercentH, nPercentW));
        }

        /// <summary>
        /// Resizes the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="percent">The percent.</param>
        /// <returns></returns>
        public static Image Resize(this Image image, float percent)
        {
            var destWidth = (int)(image.Width * percent);
            var destHeight = (int)(image.Height * percent);

            var result = new Bitmap(destWidth, destHeight);

            using (var g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, destWidth, destHeight);
                g.Dispose();
            }
            return result;
        }
    }
}
