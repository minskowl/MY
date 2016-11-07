using TVSeriesTracker.Models;

namespace TVSeriesTracker.Views
{
    /// <summary>
    /// Interaction logic for WatchListPage.xaml
    /// </summary>
    public partial class WatchListPage 
    {
        public WatchListPage()
        {
            InitializeComponent();
            DataContext = App.Container.GetInstance<IWatchListPageModel>();
        }

    }
}
