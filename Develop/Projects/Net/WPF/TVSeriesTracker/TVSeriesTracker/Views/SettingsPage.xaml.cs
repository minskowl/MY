using System.Windows.Controls;
using TVSeriesTracker.Models;

namespace TVSeriesTracker.Views
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage 
    {
        public SettingsPage()
        {
            InitializeComponent();

            DataContext = new SettingsPageModel();
        }
    }
}
