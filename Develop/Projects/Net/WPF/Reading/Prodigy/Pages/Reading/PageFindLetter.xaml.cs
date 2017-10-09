using System.Windows.Controls;
using Prodigy.Models.Reading;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Reading
{
    /// <summary>
    /// Interaction logic for PageFindLetter.xaml
    /// </summary>
    public partial class PageFindLetter : Page
    {
        public PageFindLetter()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
                DataContext= new FindLetterModel();
        }
    }
}
