using Prodigy.Models.Reading;

namespace Prodigy.Pages.Reading
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
