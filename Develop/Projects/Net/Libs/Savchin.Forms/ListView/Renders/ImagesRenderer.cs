using System;
using System.Collections;
using System.Drawing;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// An ImagesRenderer draws zero or more images depending on the data returned by its Aspect.
    /// </summary>
    /// <remarks><para>This renderer's Aspect must return a ICollection of ints, strings or Images,
    /// each of which will be drawn horizontally one after the other.</para></remarks>
    public class ImagesRenderer : BaseRenderer
    {
        /// <summary>
        /// Draw our data value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            var imageSelectors = Aspect as ICollection;
            if (imageSelectors == null)
                return;

            Point pt = r.Location;
            foreach (Object selector in imageSelectors)
            {
                Image image = GetImage(selector);
                if (image != null)
                {
                    g.DrawImage(image, pt);
                    pt.X += (image.Width + Spacing);
                }
            }
        }
    }
}