using System.Windows;
using Bashni.Core;
using Bashni.Game;
using Bashni.Views;

namespace Bashni
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowModel _model;


        public MainWindow()
        {
            InitializeComponent();

            _model = new MainWindowModel();
            _model.SetBuilder(new AsyncSolutionBuilder());
            _model.TabStatistics = tabStatistics;

            DataContext = _model;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            _model.Dispose();
            _model = null;
        }


        private void MenuMultyThreading_OnClick(object sender, RoutedEventArgs e)
        {
            menuMultyThreading.IsChecked = !menuMultyThreading.IsChecked;
            _model.SetBuilder(menuMultyThreading.IsChecked ? (ISolutionBuilder)new AsyncSolutionBuilder() : new SimpleSolutionBuilder());
        }
    }
}

