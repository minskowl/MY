using System.Windows;
using System.Windows.Media;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Core;

namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectWindow"/> class.
        /// </summary>
        public ConnectWindow()
        {
            InitializeComponent();

            listType.Items.Add("Server");
            listType.Items.Add("File");
            listType.Items.Add("Google Docs");
            listType.SelectedIndex = 1;

            //FontFamily = (FontFamily)Application.Current.Resources["DefFont"];
        }

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (!DoConnect()) return;

            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow = mainWindow;

            Close();
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool DoConnect()
        {
            var context = (listType.SelectedItem.ToString() == "Server")
                       ? boxServer.Connect()
                       : (listType.SelectedItem.ToString() == "File") ? boxFile.Connect() : boxGoogle.Connect();

            if (context == null) return false;

            KbContext.CurrentKb = context;
            AppCore.Login(context);

            return true;
        }


        private void ListTypeSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0].ToString();
            if (selected == "Server")
            {
                boxGoogle.Visibility = Visibility.Collapsed;
                boxServer.Visibility = Visibility.Visible;
                boxFile.Visibility = Visibility.Collapsed;
            }
            else if (selected == "File")
            {
                boxGoogle.Visibility = Visibility.Collapsed;
                boxServer.Visibility = Visibility.Collapsed;
                boxFile.Visibility = Visibility.Visible;
            }
            else
            {
                boxGoogle.Visibility = Visibility.Visible;
                boxServer.Visibility = Visibility.Collapsed;
                boxFile.Visibility = Visibility.Collapsed;
            }
        }
    }
}
