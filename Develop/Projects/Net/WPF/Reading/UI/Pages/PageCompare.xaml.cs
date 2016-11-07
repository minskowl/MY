using System.Windows;
using System.Windows.Controls;
using Reading.Models;
using Savchin.Wpf.Core;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageCompare.xaml
    /// </summary>
    public partial class PageCompare 
    {
        public PageCompare()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                DataContext = new CompareModel();
                var size = (double) Application.Current.Resources["SyllableFontSize"]/2;
                list.FontSize = size;
                list.Width = size;
            }


        }
    }
}
