using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
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
