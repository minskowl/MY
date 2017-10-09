using System.Windows.Controls;
using Prodigy.Models.Math;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Math
{
    /// <summary>
    /// Interaction logic for PageDigits.xaml
    /// </summary>
    public partial class PageDigits : Page
    {
        public PageDigits()
        {
            InitializeComponent();


            if (!this.IsDesignMode())
                DataContext = new DigitsModel();
        }
    }
}
