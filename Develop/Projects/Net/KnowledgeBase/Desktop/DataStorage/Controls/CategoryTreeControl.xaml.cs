using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KnowledgeBase.Core;
using KnowledgeBase.Desktop.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for CategoryTree.xaml
    /// </summary>
    public partial class CategoryTreeControl : UserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [show checkboxes].
        /// </summary>
        /// <value><c>true</c> if [show checkboxes]; otherwise, <c>false</c>.</value>
        public bool ShowCheckboxes
        {
            get { return (bool)GetValue(ShowCheckboxesProperty); }
            set { SetValue(ShowCheckboxesProperty, value); }
        }

        /// <summary>
        /// Gets the selected category ID.
        /// </summary>
        /// <value>The selected category ID.</value>
        public int SelectedCategoryId
        {
            get
            {
                if (treeView1.SelectedItem == null)
                {
                    var node = RootNode.FindSelected();
                    return node == null ? 0 : node.Id;
                }
                else
                {
                    return ((TreeNode)treeView1.SelectedItem).Id;
                }

            }
        }
        /// <summary>
        /// Gets the selected node.
        /// </summary>
        /// <value>The selected node.</value>
        public TreeNode SelectedNode
        {
            get
            {
                return treeView1.SelectedItem == null ?
                    null : ((TreeNode)treeView1.SelectedItem);
            }
        }
        // Using a DependencyProperty as the backing store for ShowCheckboxes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCheckboxesProperty =
            DependencyProperty.Register("ShowCheckboxes", typeof(bool), typeof(CategoryTreeControl), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        private IEnumerable<CategoryNode> Nodes
        {
            get { return treeView1.Items.Cast<CategoryNode>(); }
        }
        private CategoryNode _rootNode;
        /// <summary>
        /// Gets the root node.
        /// </summary>
        /// <value>The root node.</value>
        public CategoryNode RootNode
        {
            get { return _rootNode; }
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
            DependencyProperty.Register("CategoryParams", typeof(ItemParams), typeof(CategoryTreeControl), new UIPropertyMetadata(null));


        #endregion

        /// <summary>
        /// Occurs when [selected category changed].
        /// </summary>
        public event EventHandler<ObjectIdentifierEventArgs> SelectedCategoryChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTreeControl"/> class.
        /// </summary>
        public CategoryTreeControl()
        {
            InitializeComponent();

            if (this.IsDesignMode()) return;
            AppCore.Workspace.Categories.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Categories_CollectionChanged);
            AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(TreeView1Expanded), true);
            InitTree();
            treeView1.ContextMenu.DataContext = this;
        }



        private void InitTree()
        {
            treeView1.Items.Clear();
            _rootNode = CategoryNode.CreateRoot();
            treeView1.Items.Add(RootNode);
        }

        #region Interface
        /// <summary>
        /// Gets the selected categories.
        /// </summary>
        /// <returns></returns>
        public List<int> GetSelectedCategories()
        {
            var result = new List<int>();
            Nodes.GetSelectedIds(result);
            return result;
        }

        /// <summary>
        /// Selects the node.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TreeNode SelectNode(int id)
        {
            var node = RootNode.FindById(id);
            if (node == null) return null;

            node.IsSelected = true;
            return node;
        }

        /// <summary>
        /// Checks the node.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TreeNode CheckNode(int id)
        {
            var node = RootNode.FindById(id);
            if (node == null) return null;

            node.IsChecked = true;
            return node;
        }
        #endregion

        #region Event Handlers

        void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
              InitTree();
        }

        private void TreeView1SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var newSelectedNode = e.NewValue as TreeNode;
            if (newSelectedNode == null)
            {
                CategoryParams = null;
            }
            else
            {
                var categoryId = newSelectedNode.Id;
                OnSelectedCategoryChanged(new ObjectIdentifierEventArgs(categoryId));
                CategoryParams = new ItemParams(ObjectType.Category, categoryId);
            }

        }




        /// <summary>
        /// Trees the view1 expanded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TreeView1Expanded(object sender, RoutedEventArgs e)
        {
            if (this.IsDesignMode()) return;

            // Get the source
            var item = e.OriginalSource as TreeViewItem;
            if (item == null) return;
            var currentNode = item.DataContext as CategoryNode;

            // If there is no data
            if (currentNode != null) currentNode.IsExpanded = true;

        }

        /// <summary>
        /// Raises the <see cref="E:SelectedCategoryChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="KnowledgeBase.Desktop.Core.ObjectIdentifierEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSelectedCategoryChanged(ObjectIdentifierEventArgs e)
        {
            if (SelectedCategoryChanged != null) SelectedCategoryChanged(this, e);
        }


        /// <summary>
        /// Handles the OnClick event of the ItemRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            InitTree();
        } 
        #endregion
    }
}
