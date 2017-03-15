using System.Windows.Controls;
using Prodigy.Models;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageSettings.xaml
    /// </summary>
    public partial class PageSettings : Page
    {
        public PageSettings()
        {
            InitializeComponent();

            DataContext = new SettingsModel();
        }
    }
}
