using System;
using System.Windows;
using Advertiser.Views;
using Savchin.Logging;
using Savchin.Wpf.Imaging;

namespace Advertiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var view = new MainWindowView(txtLog);
                view.SelectedItems = listItems.SelectedItems;

                DataContext = view;
                Icon = Properties.Resources.logo.ToImageSource();
            }
            catch (Exception  ex)
            {

                txtLog.LogAction(ex.ToString());
            }
           
        }



        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var model = DataContext as MainWindowView;
            if (model == null) return;
            model.SaveDataBaseCommand.Execute(null);
        }

        private void OnSelectAll(object sender, RoutedEventArgs e)
        {
            listItems.SelectedItems.Clear();
            foreach (var i in listItems.Items)
                listItems.SelectedItems.Add(i);
        }

        private void OnUnSelectAll(object sender, RoutedEventArgs e)
        {
            listItems.SelectedItems.Clear();
        }
    }
}
