using System.Windows;
using Prodigy.Models.Math;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Math
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
