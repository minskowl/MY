using System.Windows.Controls;
using TVSeriesTracker.Models;

namespace TVSeriesTracker.Views
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage 
    {
        public SearchPage()
        {
            InitializeComponent();

            DataContext = new SearchPageModel();
        }
    }
}
