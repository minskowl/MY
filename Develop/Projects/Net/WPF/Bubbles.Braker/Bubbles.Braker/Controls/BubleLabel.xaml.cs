using System.Windows;
using System.Windows.Controls;
using Savchin.Bubbles.Core;

namespace Savchin.Bubbles.Controls
{
    /// <summary>
    /// Interaction logic for BubleLabel.xaml
    /// </summary>
    public partial class BubleLabel : UserControl
    {



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BubleLabel), 
            new UIPropertyMetadata(string.Empty, OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BubleLabel)d).label.Text = (string)e.NewValue;
        }


        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public BubbleColor Color
        {
            get { return (BubbleColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(BubbleColor), typeof(BubleLabel),
            new UIPropertyMetadata(BubbleColor.Empty, OnColorPropertyChanged));

        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue == null)
                return;
            ((BubleLabel)d).bubble.Color = (BubbleColor)e.NewValue;
        }


        public BubleLabel()
        {
            InitializeComponent();
        }
    }
}
