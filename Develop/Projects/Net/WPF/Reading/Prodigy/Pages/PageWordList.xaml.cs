using Prodigy.Models;
using Prodigy.Models.Reading;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageWordList.xaml
    /// </summary>
    public partial class PageWordList 
    {
        public PageWordList()
        {
            InitializeComponent();
            DataContext = new WordListModel();
        }
    }
}
