using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Savchin.Bubbles.Core;
using Savchin.Collection.Generic;
using Savchin.Utils;

namespace Savchin.Bubbles.Controls
{
    /// <summary>
    /// Interaction logic for Bubble.xaml
    /// </summary>
    public partial class Bubble : UserControl
    {
        private static readonly BiDictionary<BubbleColor, SolidColorBrush> availableColors = new BiDictionary<BubbleColor, SolidColorBrush>
                                 {
                                     {BubbleColor.Red, new SolidColorBrush(Colors.Red)},
                                     {BubbleColor.Green, new SolidColorBrush(Colors.Green)},
                                     {BubbleColor.Blue, new SolidColorBrush(Colors.Blue)},
                                     {BubbleColor.Yellow, new SolidColorBrush(Colors.Yellow)},
                                     {BubbleColor.Violet, new SolidColorBrush(Colors.Violet)},
                                 };

        #region Properties


        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(BubbleStatus), typeof(Bubble),
            new UIPropertyMetadata(BubbleStatus.Normal, OnStatusPropertyChanged));
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public BubbleStatus Status
        {
            get { return (BubbleStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }


        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color",
            typeof(BubbleColor), typeof(Bubble),
            new UIPropertyMetadata(BubbleColor.Empty, OnColorPropertyChanged));

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public BubbleColor Color
        {
            get { return (BubbleColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }


        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="Bubble"/> class.
        /// </summary>
        public Bubble()
        {
            InitializeComponent();



            //var trigger = new Trigger { Property = IsSelectedProperty, Value = true };

            //trigger.Setters.Add(new Setter(ScaleTransform.ScaleXProperty, 2));
            //trigger.Setters.Add(new Setter(ScaleTransform.ScaleYProperty, 2));

            //Triggers.Add(trigger);

        }
        public void KillPlay()
        {
            ((Storyboard)Resources["Kill"]).Begin(this);
            Status = BubbleStatus.Killed;
        }

        private static void OnStatusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Bubble b = d as Bubble;
            if (b == null) return;

            switch ((BubbleStatus)e.NewValue)
            {
                case BubbleStatus.Selected:
                    b.elLense.Fill = (Brush)b.Resources["brushSelected"];
                    break;
                case BubbleStatus.Killed:

                    break;
                case BubbleStatus.Normal:
                    b.elLense.Fill = (Brush)b.Resources["brushNormal"];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Called when [color property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Bubble b = d as Bubble;
            if (b == null) return;

            var color = (BubbleColor)e.NewValue;
            if (color != BubbleColor.Empty)
                b.elBack.Fill = availableColors[color];
        }



    }
}
