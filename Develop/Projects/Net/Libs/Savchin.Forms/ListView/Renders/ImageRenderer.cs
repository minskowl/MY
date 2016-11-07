using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Timer=System.Threading.Timer;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// Render an image that comes from our data source.
    /// </summary>
    /// <remarks>The image can be sourced from:
    /// <list>
    /// <item>a byte-array (normally when the image to be shown is
    /// stored as a value in a database)</item>
    /// <item>an int, which is treated as an index into the image list</item>
    /// <item>a string, which is treated first as a file name, and failing that as an index into the image list</item>
    /// </list>
    /// <para>If an image is an animated GIF, it's state is stored in the SubItem object.</para>
    /// <para>By default, the image renderer does not render animations (it begins life with animations paused).
    /// To enable animations, you must call Unpause().</para>
    /// </remarks>
    public class ImageRenderer : BaseRenderer
    {
        private bool isPaused = true;

        /// <summary>
        /// Make an empty image renderer
        /// </summary>
        public ImageRenderer()
        {
            tickler = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Make an empty image renderer that begins life ready for animations
        /// </summary>
        public ImageRenderer(bool startAnimations)
            : this()
        {
            Paused = !startAnimations;
        }

        /// <summary>
        /// Should the animations in this renderer be paused?
        /// </summary>
        public bool Paused
        {
            get { return isPaused; }
            set
            {
                if (isPaused != value)
                {
                    isPaused = value;
                    if (isPaused)
                    {
                        tickler.Change(Timeout.Infinite, Timeout.Infinite);
                        stopwatch.Stop();
                    }
                    else
                    {
                        tickler.Change(1, Timeout.Infinite);
                        stopwatch.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Draw our image
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);
            DrawAlignedImage(g, r, GetImageFromAspect());
        }

        /// <summary>
        /// Translate our Aspect into an image.
        /// </summary>
        /// <remarks>The strategy is:<list type="unordered">
        /// <item>If its a byte array, we treat it as an in-memory image</item>
        /// <item>If it's an int, we use that as an index into our image list</item>
        /// <item>If it's a string, we try to load a file by that name. If we can't, we use the string as an index into our image list.</item>
        ///</list></remarks>
        /// <returns>An image</returns>
        protected Image GetImageFromAspect()
        {
            if (Aspect == null || Aspect == DBNull.Value)
                return null;

            // If we've already figured out the image, don't do it again
            if (OLVSubItem != null && OLVSubItem.ImageSelector is Image)
            {
                if (OLVSubItem.AnimationState == null)
                    return (Image) OLVSubItem.ImageSelector;
                else
                    return OLVSubItem.AnimationState.image;
            }

            // Try to convert our Aspect into an Image
            // If its a byte array, we treat it as an in-memory image
            // If it's an int, we use that as an index into our image list
            // If it's a string, we try to find a file by that name.
            //    If we can't, we use the string as an index into our image list.
            Image image = null;
            if (Aspect is Byte[])
            {
                using (var stream = new MemoryStream((Byte[]) Aspect))
                {
                    try
                    {
                        image = Image.FromStream(stream);
                    }
                    catch (ArgumentException)
                    {
                        // ignore
                    }
                }
            }
            else if (Aspect is Int32)
            {
                image = GetImage(Aspect);
            }
            else
            {
                var str = Aspect as String;
                if (!String.IsNullOrEmpty(str))
                {
                    try
                    {
                        image = Image.FromFile(str);
                    }
                    catch (FileNotFoundException)
                    {
                        image = GetImage(Aspect);
                    }
                    catch (OutOfMemoryException)
                    {
                        image = GetImage(Aspect);
                    }
                }
            }

            // If this image is an animation, initialize the animation process
            if (OLVSubItem != null && AnimationState.IsAnimation(image))
            {
                OLVSubItem.AnimationState = new AnimationState(image);
            }

            // Cache the image so we don't repeat this dreary process
            if (OLVSubItem != null)
                OLVSubItem.ImageSelector = image;

            return image;
        }

        /// <summary>
        /// Pause any animations
        /// </summary>
        public void Pause()
        {
            Paused = true;
        }

        /// <summary>
        /// Unpause any animations
        /// </summary>
        public void Unpause()
        {
            Paused = false;
        }

        /// <summary>
        /// This is the method that is invoked by the timer. It basically switches control to the listview thread.
        /// </summary>
        /// <param name="state">not used</param>
        public void OnTimer(Object state)
        {
            if (ListView == null || Paused)
                tickler.Change(1000, Timeout.Infinite);
            else
            {
                if (ListView.InvokeRequired)
                    ListView.Invoke((MethodInvoker) delegate { OnTimer(state); });
                else
                    OnTimerInThread();
            }
        }

        /// <summary>
        /// This is the OnTimer callback, but invoked in the same thread as the creator of the ListView.
        /// This method can use all of ListViews methods without creating a CrossThread exception.
        /// </summary>
        protected void OnTimerInThread()
        {
            // MAINTAINER NOTE: This method must renew the tickler. If it doesn't the animations will stop.

            // If this listview has been destroyed, we can't do anything, so we return without
            // renewing the tickler, effectively killing all animations on this renderer
            if (ListView.IsDisposed)
                return;

            // If we're not in Detail view or our column has been removed from the list,
            // we can't do anything at the moment, but we still renew the tickler because the view may change later.
            if (ListView.View != View.Details || Column.Index < 0)
            {
                tickler.Change(1000, Timeout.Infinite);
                return;
            }

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            int subItemIndex = Column.Index;
            long nextCheckAt = elapsedMilliseconds + 1000; // wait at most one second before checking again
            var updateRect = new Rectangle(); // what part of the view must be updated to draw the changed gifs?

            // Run through all the subitems in the view for our column, and for each one that
            // has an animation attached to it, see if the frame needs updating.
            foreach (ListViewItem lvi in ListView.Items)
            {
                // Get the gif state from the subitem. If there isn't an animation state, skip this row.
                var lvsi = (OLVListSubItem) lvi.SubItems[subItemIndex];
                AnimationState state = lvsi.AnimationState;
                if (state == null || !state.IsValid)
                    continue;

                // Has this frame of the animation expired?
                if (elapsedMilliseconds >= state.currentFrameExpiresAt)
                {
                    state.AdvanceFrame(elapsedMilliseconds);

                    // Track the area of the view that needs to be redrawn to show the changed images
                    if (updateRect.IsEmpty)
                        updateRect = lvsi.Bounds;
                    else
                        updateRect = Rectangle.Union(updateRect, lvsi.Bounds);
                }

                // Remember the minimum time at which a frame is next due to change
                nextCheckAt = Math.Min(nextCheckAt, state.currentFrameExpiresAt);
            }

            // Update the part of the listview where frames have changed
            if (!updateRect.IsEmpty)
                ListView.Invalidate(updateRect);

            // Renew the tickler in time for the next frame change
            tickler.Change(nextCheckAt - elapsedMilliseconds, Timeout.Infinite);
        }

        #region Nested type: AnimationState

        /// <summary>
        /// Instances of this class kept track of the animation state of a single image.
        /// </summary>
        internal class AnimationState
        {
            private const int PropertyTagFrameDelay = 0x5100;
            private const int PropertyTagLoopCount = 0x5101;
            private const int PropertyTagTypeLong = 4;
            private const int PropertyTagTypeShort = 3;

            internal int currentFrame;
            internal long currentFrameExpiresAt;
            internal int frameCount;
            internal Image image;
            internal List<int> imageDuration;

            /// <summary>
            /// Create an AnimationState in a quiet state
            /// </summary>
            public AnimationState()
            {
                imageDuration = new List<int>();
            }

            /// <summary>
            /// Create an animation state for the given image, which may or may not
            /// be an animation
            /// </summary>
            /// <param name="image">The image to be rendered</param>
            public AnimationState(Image image)
                : this()
            {
                if (!IsAnimation(image))
                    return;

                // How many frames in the animation?
                this.image = image;
                frameCount = this.image.GetFrameCount(FrameDimension.Time);

                // Find the delay between each frame.
                // The delays are stored an array of 4-byte ints. Each int is the
                // number of 1/100th of a second that should elapsed before the frame expires
                foreach (PropertyItem pi in this.image.PropertyItems)
                {
                    if (pi.Id == PropertyTagFrameDelay)
                    {
                        for (int i = 0; i < pi.Len; i += 4)
                        {
                            //TODO: There must be a better way to convert 4-bytes to an int
                            int delay = (pi.Value[i + 3] << 24) + (pi.Value[i + 2] << 16) + (pi.Value[i + 1] << 8) +
                                        pi.Value[i];
                            imageDuration.Add(delay*10); // store delays as milliseconds
                        }
                        break;
                    }
                }

                // There should be as many frame durations as frames
                Debug.Assert(imageDuration.Count == frameCount,
                             "There should be as many frame durations as there are frames.");
            }

            /// <summary>
            /// Does this state represent a valid animation
            /// </summary>
            public bool IsValid
            {
                get { return (image != null && frameCount > 0); }
            }

            /// <summary>
            /// Is the given image an animation
            /// </summary>
            /// <param name="image">The image to be tested</param>
            /// <returns>Is the image an animation?</returns>
            public static bool IsAnimation(Image image)
            {
                if (image == null)
                    return false;
                else
                    return (new List<Guid>(image.FrameDimensionsList)).Contains(FrameDimension.Time.Guid);
            }

            /// <summary>
            /// Advance our images current frame and calculate when it will expire
            /// </summary>
            public void AdvanceFrame(long millisecondsNow)
            {
                currentFrame = (currentFrame + 1)%frameCount;
                currentFrameExpiresAt = millisecondsNow + imageDuration[currentFrame];
                image.SelectActiveFrame(FrameDimension.Time, currentFrame);
            }
        }

        #endregion

        #region Private variables

        private readonly Stopwatch stopwatch; // clock used to time the animation frame changes
        private readonly Timer tickler; // timer used to tickle the animations

        #endregion
    }
}