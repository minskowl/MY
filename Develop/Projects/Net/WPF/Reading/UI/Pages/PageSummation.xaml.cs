using System.Windows;
using System.Windows.Controls;
using Reading.Models;
using Savchin.Wpf.Core;

namespace Reading.Pages
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
