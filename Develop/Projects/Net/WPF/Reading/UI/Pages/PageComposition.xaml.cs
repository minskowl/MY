using Prodigy.Models;
using Prodigy.Models.Reading;

namespace Prodigy.Pages
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
