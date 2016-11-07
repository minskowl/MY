using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Bashni.Game;
using Bashni.Views;

namespace Bashni.Controls
{
    /// <summary>
    /// Interaction logic for TabBests.xaml
    /// </summary>
    public partial class TabPath 
    {
        public TabPath(List<Step> steps): this()
        {
            list.ItemsSource = steps;
        }

        public TabPath()
        {
            InitializeComponent();
        }

        private void TabItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((MainWindowModel) DataContext).BottomTabs.Remove(this);
        }
    }
}
