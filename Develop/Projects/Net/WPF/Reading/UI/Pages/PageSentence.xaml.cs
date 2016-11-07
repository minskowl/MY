using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageSentence.xaml
    /// </summary>
    public partial class PageSentence : Page
    {
        public PageSentence()
        {
            InitializeComponent();

            DataContext = new SentenceModel();
        }
    }
}
