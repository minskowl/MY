using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Visifire.Charts;
using Visifire.Commons;

namespace EffectiveSoft.SilverlightDemo.Core
{
    public static class UIFactory
    {
        public readonly static Color BackgroundColor = ConverterColor.ToColor("#FF8CA2AD").Value;

        public static bool View3D { get; set; }
        public static bool Bevel { get; set; }
        public static bool AnimationEnabled { get; set; }
        public static bool ShadowEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UIFactory"/> is fullscreen.
        /// </summary>
        /// <value><c>true</c> if fullscreen; otherwise, <c>false</c>.</value>
        public static bool Fullscreen
        {
            get { return Application.Current.Host.Content.IsFullScreen; }
            set { Application.Current.Host.Content.IsFullScreen = value; }
        }

        public static ColorSetNames ColorSet { get; set; }


        /// <summary>
        /// Initializes the <see cref="UIFactory"/> class.
        /// </summary>
        static UIFactory()
        {
            View3D = true;
            ColorSet = ColorSetNames.Visifire1;
            Bevel = true;
            AnimationEnabled = true;
            ShadowEnabled = false;
        }

        /// <summary>
        /// Charts the setup.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public static void ChartSetup(Chart chart)
        {
            chart.AnimationEnabled = AnimationEnabled;
            chart.Watermark = false;
            chart.BorderThickness = new Thickness(0);
            chart.Bevel = Bevel;
            chart.ShadowEnabled = ShadowEnabled;
            chart.View3D = View3D;
            chart.ColorSet = ColorSet;
    
        }

        public static void ChartAddTitle(Chart chart, string text)
        {
            chart.Titles.Add(new Title { Text = text, FontSize = 10, HorizontalAlignment = HorizontalAlignment.Left });
        }

        /// <summary>
        /// Creates the legend.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static Legend CreateLegend(string text)
        {
            return new Legend
            {
                EntryMargin = 2,
                Name = text,
                Cursor = Cursors.Hand,
                ToolTipText = "Click for the details.",
                VerticalAlignment = VerticalAlignment.Bottom,
                

                //HorizontalAlignment = HorizontalAlignment.Center,
                //HorizontalContentAlignment = HorizontalAlignment.Center,
                //UseLayoutRounding = false
                //VerticalAlignment = VerticalAlignment.Center,
                //VerticalContentAlignment =   VerticalAlignment.Bottom,

                //LightingEnabled = trueo
            };
        }

    }
}
