using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageSyllables.xaml
    /// </summary>
    public partial class PageSyllables 
    {
        public PageSyllables()
        {
            InitializeComponent();

            DataContext = new SyllablesModel();
        }
    }
}
