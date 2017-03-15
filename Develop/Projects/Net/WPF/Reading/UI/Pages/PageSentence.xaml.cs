using System.Windows.Controls;
using Prodigy.Models;
using Prodigy.Models.Reading;

namespace Prodigy.Pages
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
