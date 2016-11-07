using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls.Docking
{
    /// <summary>
    /// Interaction logic for CategoryInfoCotrol.xaml
    /// </summary>
    public partial class CategoryInfoControl 
    {
        #region Properties

        private readonly IKnowledgeManager _knowledgeManager;

        private int _categoryID;
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>The category ID.</value>
        public int CategoryID
        {
            get { return _categoryID; }
            set
            {
                if (_categoryID != value)
                {
                    _categoryID = value;
                    CategoryChanged();

                }
            }
        }


        private int SelectedCategoryID
        {
            get
            {
                return listCategories.SelectedItem == null ? 0
                           : (int)((DataRowView)listCategories.SelectedItem)["CategoryID"];
            }
        }
        /// <summary>
        /// Gets or sets the category params.
        /// </summary>
        /// <value>The category params.</value>
        public ItemParams CategoryParams
        {
            get { return (ItemParams)GetValue(CategoryParamsProperty); }
            private set { SetValue(CategoryParamsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KnowledgeParams.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoryParamsProperty =
            DependencyProperty.Register("CategoryParams", typeof(ItemParams), typeof(CategoryInfoControl), new UIPropertyMetadata(null));


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryInfoControl"/> class.
        /// </summary>
        public CategoryInfoControl()
        {
            InitializeComponent();

            if (this.IsDesignMode())return;

            _knowledgeManager = KbContext.CurrentKb.ManagerKnowledge;
            listItems.itemRefresh.Click += ItemRefreshClick;
            listCategories.ContextMenu.DataContext = this;
            AppCore.Workspace.Categories.CollectionChanged += Categories_CollectionChanged;
        }

        



        /// <summary>
        /// Reloads the items.
        /// </summary>
        public void ReloadItems()
        {
            if (_categoryID > 0)
            {
                listItems.Show(_knowledgeManager.GetShortInfoByCategoryID(_categoryID));
                listItems.Visibility = Visibility.Visible;
            }
            else
            {
                listItems.Show(null);
                listItems.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set to true internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            CategoryChanged();
        }

        #region Event Handlers

        void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ReloadCategories();
        }


        private void ListCategoriesMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listCategories.SelectedItem != null)
                AppCommands.View.Execute(CategoryParams, this);
        }

        void ItemRefreshClick(object sender, RoutedEventArgs e)
        {
            ReloadCategories();
        }

        private void ListItemsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryParams = listCategories.SelectedItem == null ? null :
                new ItemParams(ObjectType.Category, SelectedCategoryID);
        }
        #endregion

        private void ReloadCategories()
        {
            listCategories.ItemsSource = AppCore.Workspace.Categories.GetInfoByParentCategoryID(_categoryID);
        }

        private void CategoryChanged()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            var permission = KbContext.CurrentKb.GetPermissionsForCategory(_categoryID);
            itemSeparanor.Visibility = itemAdd.Visibility = itemEdit.Visibility = itemDelete.Visibility =
                   permission.CanEditCategory() ? Visibility.Visible : Visibility.Collapsed;

            listItems.itemKnowldegeAdd.CommandParameter = new ItemParams(ObjectType.Knowledge, _categoryID);
            itemAdd.CommandParameter = new ItemParams(ObjectType.Category, _categoryID);

            if (_categoryID > 0)
            {
                var category = KbContext.CurrentKb.ManagerCategory.GetByID(_categoryID);
                if (category == null) return;

                //Title = "Category: " + category.Name;
            }
            else
            {
            //    Title = "Category: Root";

            }
            ReloadCategories();
            ReloadItems();
        }



    }
}
