using System.Linq;
using System.Windows.Controls;

namespace Advertiser.Controls
{
    /// <summary>
    /// Interaction logic for WheelSizeControl.xaml
    /// </summary>
    public partial class WheelSizeControl 
    {
        public WheelSizeControl()
        {
            InitializeComponent();

            cmbR.ItemsSource = Enumerable.Range(12, 20).ToArray();
        }
    }
}
