using Prodigy.Models;
using Prodigy.Models.Math;
using Savchin.Wpf.Core;

namespace Prodigy.Pages
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
