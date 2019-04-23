using System.Windows;
using AoM.Viewer.ViewModels;

namespace AoM.Viewer.Views
{
    /// <summary>
    /// Interaction logic for LocationsWindow.xaml
    /// </summary>
    public partial class LocationsWindow : Window
    {
        public LocationsWindow()
        {
            InitializeComponent();

            DataContext = new LocationsViewModel();
        }
    }
}
