using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Desktop.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for KnowledgeListControl.xaml
    /// </summary>
    public partial class KnowledgeListControl : UserControl
    {
        #region Properties
        private object SelectedKnowledgeID
        {
            get { return SelectedData["KnowledgeID"]; }
        }

        /// <summary>
        /// Gets the selected data.
        /// </summary>
        /// <value>The selected data.</value>
        private DataRowView SelectedData
        {
            get { return (DataRowView)listItems.SelectedItem; }
        }

        public ItemParams KnowledgeParams
        {
            get { return (ItemParams)GetValue(KnowledgeParamsProperty); }
            private set { SetValue(KnowledgeParamsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KnowledgeParams.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KnowledgeParamsProperty =
            DependencyProperty.Register("KnowledgeParams", typeof(ItemParams), typeof(KnowledgeListControl), new UIPropertyMetadata(null));

        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeListControl"/> class.
        /// </summary>
        public KnowledgeListControl()
        {
            InitializeComponent();

            listItems.ContextMenu.DataContext = this;
        }
        /// <summary>
        /// Shows the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Show(DataTable data)
        {
            if(data==null)
            {
                listItems.ItemsSource =null;
                return;
            }

            var manager = KbContext.CurrentKb.ManagerKnowledge;
            manager.RemoveNotViewable(data);
            manager.AddCanEditColumn(data);
            listItems.ItemsSource = data.DefaultView;
        }

        /// <summary>
        /// Lists the items selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ListItemsSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            KnowledgeParams = listItems.SelectedItem == null ? null :
                new ItemParams(ObjectType.Knowledge, (int)SelectedKnowledgeID);
        }


        /// <summary>
        /// Handles the MouseDoubleClick event of the listItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void listItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listItems.SelectedItem == null) return;

            AppCommands.View.Execute(new ItemParams(ObjectType.Knowledge, (int)SelectedKnowledgeID), this);

        }

        /// <summary>
        /// Handles the Opened event of the ContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var data = SelectedData;
            if (data == null) return;

            var canEdit = (bool)data["CanEdit"];
            itemDelete.Visibility = itemEdit.Visibility = canEdit ? Visibility.Visible : Visibility.Collapsed;

        }
    }
}
