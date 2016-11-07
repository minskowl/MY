using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Collections;
using KnowledgeBase.Desktop.Core;
using Savchin.Wpf.Controls.DataGrid.Filtering;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls.Docking
{
    /// <summary>
    /// Interaction logic for KeywordList.xaml
    /// </summary>
    public partial class KeywordList 
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
            DependencyProperty.Register("ItemParams", typeof(ItemParams), typeof(KeywordList), new UIPropertyMetadata(null));

        private Keyword SelectedKeyword
        {
            get { return grid.SelectedItem as Keyword; }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public KeywordCollection ItemsSource
        {
            get { return (KeywordCollection)grid.ItemsSource; }
            set
            {
                grid.ItemsSource = value;
               
            }
        } 
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordList"/> class.
        /// </summary>
        public KeywordList()
        {
            InitializeComponent();

            if (this.IsDesignMode()) return;

            FillList();

            var b = new Binding
                        {
                            Path = new PropertyPath(DataGridExtensions.ClearFilterCommandProperty),
                            Source = grid
                        };
            BindingOperations.SetBinding(itemClearFilter, MenuItem.CommandProperty, b);
            DataContext = this;
        }

        /// <summary>
        /// Fills the list.
        /// </summary>
        public void FillList()
        {
            ItemsSource = AppCore.Workspace.Keywords;
        }


        private void itemRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemParams = SelectedKeyword == null ? null : 
                new ItemParams(ObjectType.Keyword, SelectedKeyword.KeywordID);
        }

        /// <summary>
        /// Handles the Executed event of the DeleteCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var param = (ItemParams)e.Parameter;
            if (param.Type != ObjectType.Keyword)
            {
                e.Handled = false;
                return;
            }

            var item = ItemsSource.GetById(param.IntId);
            if (item == null) return;
            if (MessageBox.Show(string.Format("Are you sure to delete '{0}' keyword?", item.Name),
                                "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ItemsSource.Remove(item);
            }
        }

    }
}
