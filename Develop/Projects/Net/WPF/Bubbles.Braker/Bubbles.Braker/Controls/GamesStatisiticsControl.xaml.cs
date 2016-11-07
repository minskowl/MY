using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Windows.Controls;
using Bubbles.Braker;
using Savchin.Bubbles.Core;

namespace Savchin.Bubbles.Controls
{
    /// <summary>
    /// Interaction logic for GamesStatisiticsControl.xaml
    /// </summary>
    public partial class GamesStatisiticsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamesStatisiticsControl"/> class.
        /// </summary>
        public GamesStatisiticsControl()
        {
            InitializeComponent();
            Loaded += new System.Windows.RoutedEventHandler(GamesStatisiticsControl_Loaded);
           
        }

        void GamesStatisiticsControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            grid.ItemsSource = App.Current.Statistics.Compute();
        }




    }
}
