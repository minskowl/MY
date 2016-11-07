using System;
using System.Drawing;
using System.Globalization;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A MultiImageRenderer draws the same image a number of times based on our data value
    /// </summary>
    /// <remarks><para>The stars in the Rating column of iTunes is a good example of this type of renderer.</para></remarks>
    public class MultiImageRenderer : BaseRenderer
    {
        /// <summary>
        /// The image selector that will give the image to be drawn
        /// </summary>
        public Object ImageSelector;

        /// <summary>
        /// Values greater than or equal to this will have MaxNumberImages images drawn
        /// </summary>
        public int MaximumValue = 100;

        /// <summary>
        /// What is the maximum number of images that this renderer should draw?
        /// </summary>
        public int MaxNumberImages = 10;

        /// <summary>
        /// Values less than or equal to this will have 0 images drawn
        /// </summary>
        public int MinimumValue;

        /// <summary>
        /// Make a quiet rendererer
        /// </summary>
        public MultiImageRenderer()
        {
        }

        /// <summary>
        /// Make an image renderer that will draw the indicated image, at most maxImages times.
        /// </summary>
        /// <param name="imageSelector"></param>
        /// <param name="maxImages"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public MultiImageRenderer(Object imageSelector, int maxImages, int minValue, int maxValue)
            : this()
        {
            ImageSelector = imageSelector;
            MaxNumberImages = maxImages;
            MinimumValue = minValue;
            MaximumValue = maxValue;
        }

        /// <summary>
        /// Draw our data value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            Image image = GetImage(ImageSelector);
            if (image == null)
                return;

            // Convert our aspect to a numeric value
            // CONSIDER: Is this the best way to do this?
            if (!(Aspect is IConvertible))
                return;
            double aspectValue = ((IConvertible) Aspect).ToDouble(NumberFormatInfo.InvariantInfo);

            // Calculate how many images we need to draw to represent our aspect value
            int numberOfImages;
            if (aspectValue <= MinimumValue)
                numberOfImages = 0;
            else if (aspectValue < MaximumValue)
                numberOfImages = 1 + (int) (MaxNumberImages*(aspectValue - MinimumValue)/MaximumValue);
            else
                numberOfImages = MaxNumberImages;

            // If we need to shrink the image, what will its on-screen dimensions be?
            int imageScaledWidth = image.Width;
            int imageScaledHeight = image.Height;
            if (r.Height < image.Height)
            {
                imageScaledWidth = (int) (image.Width*(float) r.Height/image.Height);
                imageScaledHeight = r.Height;
            }
            // Calculate where the images should be drawn
            Rectangle imageBounds = r;
            imageBounds.Width = (MaxNumberImages*(imageScaledWidth + Spacing)) - Spacing;
            imageBounds.Height = imageScaledHeight;
            imageBounds = AlignRectangle(r, imageBounds);

            // Finally, draw the images
            for (int i = 0; i < numberOfImages; i++)
            {
                g.DrawImage(image, imageBounds.X, imageBounds.Y, imageScaledWidth, imageScaledHeight);
                imageBounds.X += (imageScaledWidth + Spacing);
            }
        }
    }
}