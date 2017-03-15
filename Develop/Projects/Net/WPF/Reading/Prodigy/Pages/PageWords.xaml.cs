using Prodigy.Models;
using Prodigy.Models.Reading;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageWords.xaml
    /// </summary>
    public partial class PageWords 
    {
        public PageWords()
        {
            InitializeComponent();

            DataContext = new WordsModel();
        }

 
    }
}
