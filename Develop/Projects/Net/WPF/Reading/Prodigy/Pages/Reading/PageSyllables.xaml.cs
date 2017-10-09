using Prodigy.Models.Reading;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Reading
{
    /// <summary>
    /// Interaction logic for PageSyllables.xaml
    /// </summary>
    public partial class PageSyllables 
    {
        public PageSyllables()
        {
            InitializeComponent();
            if (!this.IsDesignMode())
                DataContext = new SyllablesModel();
        }
    }
}
