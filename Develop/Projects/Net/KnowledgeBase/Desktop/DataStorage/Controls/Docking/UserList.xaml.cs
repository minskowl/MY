using System.Windows;
using System.Windows.Controls;
using AvalonDock;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Desktop.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls.Docking
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList 
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
            DependencyProperty.Register("ItemParams", typeof(ItemParams), typeof(UserList), new UIPropertyMetadata(null));

        private User SelectedUser
        {
            get { return grid.SelectedItem as User; }
        }

        ///// <summary>
        ///// Gets or sets the items source.
        ///// </summary>
        ///// <value>The items source.</value>
        //public KeywordCollection ItemsSource
        //{
        //    get { return (KeywordCollection)grid.ItemsSource; }
        //    set
        //    {
        //        grid.ItemsSource = value;

        //    }
        //}
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="UserList"/> class.
        /// </summary>
        public UserList()
        {
            InitializeComponent();

            if (this.IsDesignMode() || !KbContext.CurrentKb.HasUserAdminPermission) return;
         
            DataContext = this;
            itemAdd.CommandParameter = new ItemParams(ObjectType.User, 0);
            ResetData();
        }
        /// <summary>
        /// Resets the data.
        /// </summary>
        public  void ResetData()
        {
            grid.ItemsSource = KbContext.CurrentKb.ManagerUser.GetAll();
        }


        private void itemRefresh_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
        }


        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemParams = SelectedUser == null ? null :
           new ItemParams(ObjectType.User, SelectedUser.UserID); 

        }
    }
}
