using System.Windows;
using System.Linq;
using AvalonDock;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Desktop.Core;
using Savchin.Text;

namespace KnowledgeBase.Desktop.Controls.Docs
{
    /// <summary>
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults
    {

        #region Properties


        private SearchFilter _filter;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public SearchFilter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                if (_filter != null)
                {
                    SearchItems();
                }
            }
        }

        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResults"/> class.
        /// </summary>
        public SearchResults()
        {
            InitializeComponent();
            listItems.itemRefresh.Click += new RoutedEventHandler(ItemsRefresh_Click);
            listItems.itemKnowldegeAdd.Visibility = Visibility.Collapsed;
        }

        #region Event handlers


        /// <summary>
        /// Handles the Click event of the ItemsRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemsRefresh_Click(object sender, RoutedEventArgs e)
        {
            SearchItems();
        }
        #endregion

        private void SearchItems()
        {
            var data = KbContext.CurrentKb.ManagerKnowledge.Search(
                        _filter.Text,
                        _filter.Types,
                        _filter.Categories,
                        _filter.Keywords,
                        _filter.Statuses);

            listItems.Show(data);
            if (!string.IsNullOrWhiteSpace(_filter.Text))
            {
                Title = "Search result: " + string.IsNullOrEmpty(_filter.Text);
            }
            else if (_filter.Keywords != null && _filter.Keywords.Count > 0)
            {
                var keywords = KbContext.CurrentKb.ManagerKeyword.GetByListID(_filter.Keywords);
                Title = "Search result: " + keywords.Select(e => e.Name).Join(",");
            }
        }


    }
}
