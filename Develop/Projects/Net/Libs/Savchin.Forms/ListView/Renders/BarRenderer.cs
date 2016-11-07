using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// Render our Aspect as a progress bar
    /// </summary>
    public class BarRenderer : BaseRenderer
    {
        #region Constructors

        /// <summary>
        /// Make a BarRenderer
        /// </summary>
        public BarRenderer()
        {
            Pen = new Pen(Color.Blue, 1);
            Brush = Brushes.Aquamarine;
            BackgroundBrush = Brushes.White;
            StartColor = Color.Empty;
        }

        /// <summary>
        /// Make a BarRenderer for the given range of data values
        /// </summary>
        public BarRenderer(int minimum, int maximum)
            : this()
        {
            MinimumValue = minimum;
            MaximumValue = maximum;
        }

        /// <summary>
        /// Make a BarRenderer using a custom bar scheme
        /// </summary>
        public BarRenderer(Pen pen, Brush brush)
            : this()
        {
            Pen = pen;
            Brush = brush;
            UseStandardBar = false;
        }

        /// <summary>
        /// Make a BarRenderer using a custom bar scheme
        /// </summary>
        public BarRenderer(int minimum, int maximum, Pen pen, Brush brush)
            : this(minimum, maximum)
        {
            Pen = pen;
            Brush = brush;
            UseStandardBar = false;
        }

        /// <summary>
        /// Make a BarRenderer that uses a horizontal gradient
        /// </summary>
        public BarRenderer(Pen pen, Color start, Color end)
            : this()
        {
            Pen = pen;
            SetGradient(start, end);
        }

        /// <summary>
        /// Make a BarRenderer that uses a horizontal gradient
        /// </summary>
        public BarRenderer(int minimum, int maximum, Pen pen, Color start, Color end)
            : this(minimum, maximum)
        {
            Pen = pen;
            SetGradient(start, end);
        }

        #endregion

        #region Public variables

        /// <summary>
        /// The brush that will be used to fill the background of the bar
        /// </summary>
        public Brush BackgroundBrush;

        /// <summary>
        /// The brush that will be used to fill the bar
        /// </summary>
        public Brush Brush;

        /// <summary>
        /// The end color when a gradient is used to fill the bar
        /// </summary>
        public Color EndColor;

        /// <summary>
        /// Regardless of how high the cell is  the progress bar will never be taller than this
        /// </summary>
        public int MaximumHeight = 16;

        /// <summary>
        /// The maximum value for the range. Values greater than this will give a full bar
        /// </summary>
        public int MaximumValue = 100;

        /// <summary>
        /// Regardless of how wide the column become the progress bar will never be wider than this
        /// </summary>
        public int MaximumWidth = 100;

        /// <summary>
        /// The minimum data value expected. Values less than this will given an empty bar
        /// </summary>
        public int MinimumValue;

        /// <summary>
        /// How many pixels in from our cell border will this bar be drawn
        /// </summary>
        public int Padding = 2;

        /// <summary>
        /// The Pen that will draw the frame surrounding this bar
        /// </summary>
        public Pen Pen;

        /// <summary>
        /// The first color when a gradient is used to fill the bar
        /// </summary>
        public Color StartColor;

        /// <summary>
        /// Should this bar be drawn in the system style
        /// </summary>
        public bool UseStandardBar = true;

        #endregion

        /// <summary>
        /// Draw this progress bar using a gradient
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetGradient(Color start, Color end)
        {
            StartColor = start;
            EndColor = end;
            UseStandardBar = false;
        }

        /// <summary>
        /// Draw our aspect
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        public override void Render(Graphics g, Rectangle r)
        {
            DrawBackground(g, r);

            Rectangle frameRect = Rectangle.Inflate(r, 0 - Padding, 0 - Padding);
            frameRect.Width = Math.Min(frameRect.Width, MaximumWidth);
            frameRect.Height = Math.Min(frameRect.Height, MaximumHeight);
            frameRect = AlignRectangle(r, frameRect);

            // Convert our aspect to a numeric value
            // CONSIDER: Is this the best way to do this?
            if (!(Aspect is IConvertible))
                return;
            double aspectValue = ((IConvertible) Aspect).ToDouble(NumberFormatInfo.InvariantInfo);

            Rectangle fillRect = Rectangle.Inflate(frameRect, -1, -1);
            if (aspectValue <= MinimumValue)
                fillRect.Width = 0;
            else if (aspectValue < MaximumValue)
                fillRect.Width = (int) (fillRect.Width*(aspectValue - MinimumValue)/MaximumValue);

            if (UseStandardBar && ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(g, frameRect);
                ProgressBarRenderer.DrawHorizontalChunks(g, fillRect);
            }
            else
            {
                g.FillRectangle(BackgroundBrush, frameRect);
                if (fillRect.Width > 0)
                {
                    // FillRectangle fills inside the given rectangle, so expand it a little
                    fillRect.Width++;
                    fillRect.Height++;
                    if (StartColor == Color.Empty)
                        g.FillRectangle(Brush, fillRect);
                    else
                    {
                        using (
                            var gradient = new LinearGradientBrush(frameRect, StartColor, EndColor,
                                                                   LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(gradient, fillRect);
                        }
                    }
                }
                g.DrawRectangle(Pen, frameRect);
            }
        }
    }
}