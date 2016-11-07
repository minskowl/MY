using System.Windows;
using Savchin.Wpf.Imaging;

namespace Reading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo.ToImageSource();
        }

        private void navigator_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            panelStatus.DataContext = ((FrameworkElement) navigator.Content).DataContext;
        }
    }
}
