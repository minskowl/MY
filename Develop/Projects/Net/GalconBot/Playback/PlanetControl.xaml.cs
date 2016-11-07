using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Playback.Core;

namespace Savchin.Bubbles.Controls
{
    /// <summary>
    /// Interaction logic for Bubble.xaml
    /// </summary>
    public partial class PlanetControl : UserControl
    {
     

        #region Properties

        public string Text
        {
            set { eLabel.Text = value; }
        }



        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner",
            typeof(int), typeof(PlanetControl),
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


        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetControl"/> class.
        /// </summary>
        public PlanetControl()
        {
            InitializeComponent();

            elBack.Fill = Settings.AvailableColors[0];

        }




        /// <summary>
        /// Called when [color property changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PlanetControl b = d as PlanetControl;
            if (b == null) return;

            var color = (int)e.NewValue;
            b.elBack.Fill = Settings.AvailableColors[color];
        }



    }
}
