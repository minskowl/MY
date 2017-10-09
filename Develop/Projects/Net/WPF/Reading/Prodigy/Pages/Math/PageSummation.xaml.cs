using System.Windows;
using Prodigy.Models.Math;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Math
{
    /// <summary>
    /// Interaction logic for PageSummation.xaml
    /// </summary>
    public partial class PageSummation 
    {
        public PageSummation()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                DataContext = new SummationModel();
                boxResult.Width = (double)Application.Current.Resources["SyllableFontSize"];
            }

           
        }
    }
}
