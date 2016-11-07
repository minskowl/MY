using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Advertiser.Entities;

namespace Advertiser.Controls
{
    /// <summary>
    /// Interaction logic for SelectAccontsWindow.xaml
    /// </summary>
    public partial class SelectAccontsWindow : Window
    {
        public SelectAccontsWindow()
        {
            InitializeComponent();
        }

        public Login[] GetAccounts(IEnumerable<Login> logins)
        {
            Owner = Application.Current.MainWindow;
            list.ItemsSource = logins;
            return (ShowDialog() ?? false) ? list.SelectedItems.Cast<Login>().ToArray() : null;

        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите аккаунт для публикации");
                return;
            }
            DialogResult = true;
        }


        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedItems.Count > 0)
                list.SelectedItems.Add(((ListBoxItem)e.Source).Content);
            DialogResult = true;
            Close();
        }
    }
}
