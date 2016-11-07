using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Playback.Core;

namespace Playback
{
    /// <summary>
    /// Interaction logic for FleetControl.xaml
    /// </summary>
    public partial class FleetControl : UserControl
    {
        public string Text
        {
            set { Label.Text = value; }
        }

        public static readonly DependencyProperty OwnerProperty =
        DependencyProperty.Register("Owner",
        typeof(int), typeof(FleetControl),
        new UIPropertyMetadata(0, OnColorPropertyChanged));

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public int Owner
        {
            get { return (int)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        public FleetControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Called when [color property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FleetControl b = d as FleetControl;
            if (b == null) return;

            var color = (int)e.NewValue;
            b.Label.Foreground = Settings.AvailableColors[color];
        }


    }
}
