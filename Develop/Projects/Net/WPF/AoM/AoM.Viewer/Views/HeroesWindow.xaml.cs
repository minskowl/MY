using System.Windows;
using AoM.Viewer.ViewModels;

namespace AoM.Viewer
{
    /// <summary>
    /// Interaction logic for HeroesWindow.xaml
    /// </summary>
    public partial class HeroesWindow : Window
    {
        public HeroesWindow()
        {
            InitializeComponent();

            DataContext = new HeroesViewModel();
        }
    }
}
