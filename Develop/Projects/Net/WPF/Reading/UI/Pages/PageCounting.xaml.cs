using Reading.Models;
using Savchin.Wpf.Core;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageCounting.xaml
    /// </summary>
    public partial class PageCounting 
    {
        public PageCounting()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
                DataContext = new CountingModel();
        }
    }
}
