using Prodigy.Models;
using Savchin.Wpf.Core;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageSummation.xaml
    /// </summary>
    public partial class PageFindPair
    {
        public PageFindPair()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                DataContext = new FindPairModel();
            }

           
        }
    }
}
