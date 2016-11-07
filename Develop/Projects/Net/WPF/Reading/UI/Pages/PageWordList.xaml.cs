using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
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
