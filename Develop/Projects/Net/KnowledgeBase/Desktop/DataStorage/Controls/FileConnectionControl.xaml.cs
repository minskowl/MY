using System;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Core;
using KnowledgeBase.SqlLite.Dal.Core;
using Microsoft.Win32;
using Savchin.Wpf.Controls.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for FileConnectionControl.xaml
    /// </summary>
    public partial class FileConnectionControl : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FileConnectionControl"/> class.
        /// </summary>
        public FileConnectionControl()
        {
            InitializeComponent();

            if(this.IsDesignMode())return;
            boxFile.Text = KnowledgeBase.Core.Properties.Settings.Default.PreviousFile;
        }
        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns></returns>
        public KbContext Connect()
        {
            try
            {
                var filePath = string.IsNullOrEmpty(boxFile.Text) ?
                    AppCore.Settings.LocalDatabasePath : boxFile.Text;

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("File not exist  " + filePath);
                    return null;
                }

                var result= LocalFileContext.OpenDatabase(filePath);
                KnowledgeBase.Core.Properties.Settings.Default.PreviousFile = filePath;
                return result;
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Error connect", ex);
                return null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
                             {
                                 Title = "Select database file",
                                 Filter = LocalFileContext.FileFilter
                             };

            if ((dialog.ShowDialog(Application.Current.MainWindow) ?? false))
                boxFile.Text = dialog.FileName;
        }
    }
}
