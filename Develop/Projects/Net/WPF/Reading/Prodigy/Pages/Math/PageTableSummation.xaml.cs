using Prodigy.Models;
using Prodigy.Models.Math;
using Savchin.Wpf.Core;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageSummation.xaml
    /// </summary>
    public partial class PageTableSummation 
    {
        public PageTableSummation()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                DataContext = new TableSummationModel();
            }

           
        }
    }
}
