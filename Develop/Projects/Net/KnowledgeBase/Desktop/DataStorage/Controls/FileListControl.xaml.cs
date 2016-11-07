using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Collections;
using KnowledgeBase.Desktop.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for FileListControl.xaml
    /// </summary>
    public partial class FileListControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the item params.
        /// </summary>
        /// <value>The item params.</value>
        public ItemParams ItemParams
        {
            get { return (ItemParams)GetValue(ItemParamsProperty); }
            private set { SetValue(ItemParamsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KnowledgeParams.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemParamsProperty =
            DependencyProperty.Register("ItemParams", typeof(ItemParams), typeof(FileListControl), new UIPropertyMetadata(null));

        private object SelectedFileID
        {
            get { return ((FileInfo)listFiles.SelectedItem).ID; }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        private FileCollectionBase ItemsSource;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FileListControl"/> class.
        /// </summary>
        public FileListControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ItemsSource = e.NewValue as FileCollectionBase;
            if (ItemsSource != null)
            {
                itemAdd.CommandParameter = new ItemParams(ObjectType.File, ItemsSource.OwnerId);
            }
        }

        /// <summary>
        /// Lists the items selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ListItemsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ItemParams = listFiles.SelectedItem == null ? null :
                new ItemParams(ObjectType.File, SelectedFileID);
        }
        private void ItemCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            if (ItemParams == null) return;
            
            if (ItemParams.Type == ObjectType.File)
            {
                e.CanExecute = true;
            }
            else
            {
                e.Handled = false;
            }

        }
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ItemParams == null) return;

            if (ItemParams.Type == ObjectType.File)
            {
                var userFile = ItemsSource.GetById(ItemParams.Id);
                if (userFile != null) userFile.ViewFile();
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Handles the Executed event of the AddCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var param = e.Parameter as ItemParams;
            if (param == null) return;
            if (param.Type == ObjectType.File)
            {
                ItemsSource.CreateNew();
            }
            else
            {
                e.Handled = false;
            }
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ItemParams==null)return;

            if (ItemParams.Type != ObjectType.File)
            {
                e.Handled = false;
                return;
            }

            var userFile = ItemsSource.GetById(ItemParams.Id);
            if (userFile == null) return;
            if (MessageBox.Show(string.Format("Are you sure to delete '{0}' file?", userFile.Name),
                                "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ItemsSource.Remove(userFile);
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            listFiles.Focus();
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ItemsSource.CreateNew(Clipboard.GetImage());


        }

        private void Paste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsImage();
        }


    }
}
