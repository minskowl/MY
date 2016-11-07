using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageSyllables.xaml
    /// </summary>
    public partial class PageComposition 
    {
        public PageComposition()
        {
            InitializeComponent();

            DataContext = new CompositionListModel();
        }
    }
}
