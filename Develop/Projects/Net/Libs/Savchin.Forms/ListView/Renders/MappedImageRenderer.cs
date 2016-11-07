using System;
using System.Collections;
using System.Drawing;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// This class maps a data value to an image that should be drawn for that value.
    /// </summary>
    /// <remarks><para>It is useful for drawing data that is represented as an enum or boolean.</para></remarks>
    public class MappedImageRenderer : BaseRenderer
    {
        /// <summary>
        /// Make a new empty renderer
        /// </summary>
        public MappedImageRenderer()
        {
            map = new Hashtable();
        }

        /// <summary>
        /// Make a new renderer that will show the given image when the given key is the aspect value
        /// </summary>
        /// <param name="key">The data value to be matched</param>
        /// <param name="image">The image to be shown when the key is matched</param>
        public MappedImageRenderer(Object key, Object image)
            : this()
        {
            Add(key, image);
        }

        /// <summary>
        /// Make a new renderer that will show the given images when it receives the given keys
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="image1"></param>
        /// <param name="key2"></param>
        /// <param name="image2"></param>
        public MappedImageRenderer(Object key1, Object image1, Object key2, Object image2)
            : this()
        {
            Add(key1, image1);
            Add(key2, image2);
        }

        /// <summary>
        /// Build a renderer from the given array of keys and their matching images
        /// </summary>
        /// <param name="keysAndImages">An array of key/image pairs</param>
        public MappedImageRenderer(Object[] keysAndImages)
            : this()
        {
            if ((keysAndImages.GetLength(0)%2) != 0)
                throw new ArgumentException("Array must have key/image pairs");

            for (int i = 0; i < keysAndImages.GetLength(0); i += 2)
                Add(keysAndImages[i], keysAndImages[i + 1]);
        }

        /// <summary>
        /// Return a renderer that draw boolean values using the given images
        /// </summary>
        /// <param name="trueImage">Draw this when our data value is true</param>
        /// <param name="falseImage">Draw this when our data value is false</param>
        /// <returns>A Renderer</returns>
        public static MappedImageRenderer Boolean(Object trueImage, Object falseImage)
        {
            return new MappedImageRenderer(true, trueImage, false, falseImage);
        }

        /// <summary>
        /// Return a renderer that draw tristate boolean values using the given images
        /// </summary>
        /// <param name="trueImage">Draw this when our data value is true</param>
        /// <param name="falseImage">Draw this when our data value is false</param>
        /// <param name="nullImage">Draw this when our data value is null</param>
        /// <returns>A Renderer</returns>
        public static MappedImageRenderer TriState(Object trueImage, Object falseImage, Object nullImage)
        {
            return new MappedImageRenderer(new[] {true, trueImage, false, falseImage, null, nullImage});
        }

        /// <summary>
        /// Register the image that should be drawn when our Aspect has the data value.
        /// </summary>
        /// <param name="value">Value that the Aspect must match</param>
        /// <param name="image">An ImageSelector -- an int, string or image</param>
        public void Add(Object value, Object image)
        {
            if (value == null)
                nullImage = image;
            else
                map[value] = image;
        }

        /// <summary>
        /// Render our value
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            if (Aspect is ICollection)
                RenderCollection(g, r, (ICollection) Aspect);
            else
                RenderOne(g, r, Aspect);
        }

        private void RenderCollection(Graphics g, Rectangle r, ICollection imageSelectors)
        {
            Image image = null;
            Point pt = r.Location;
            foreach (Object selector in imageSelectors)
            {
                if (selector == null)
                    image = GetImage(nullImage);
                else if (map.ContainsKey(selector))
                    image = GetImage(map[selector]);
                else
                    image = null;

                if (image != null)
                {
                    g.DrawImage(image, pt);
                    pt.X += (image.Width + Spacing);
                }
            }
        }

        private void RenderOne(Graphics g, Rectangle r, Object selector)
        {
            Image image = null;
            if (selector == null)
                image = GetImage(nullImage);
            else if (map.ContainsKey(selector))
                image = GetImage(map[selector]);

            if (image != null)
                DrawAlignedImage(g, r, image);
        }

        #region Private variables

        private readonly Hashtable map; // Track the association between values and images
        private Object nullImage; // image to be drawn for null values (since null can't be a key)

        #endregion
    }
}