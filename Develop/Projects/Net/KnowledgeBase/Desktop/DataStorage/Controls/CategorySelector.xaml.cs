using System.Windows;
using System.Windows.Controls;
using KnowledgeBase.Desktop.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for CategorySelector.xaml
    /// </summary>
    public partial class CategorySelector 
    {


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(CategorySelector), new UIPropertyMetadata(0, OnValueChanged));




        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySelector"/> class.
        /// </summary>
        public CategorySelector()
        {
            Title = "Category";
            InitializeComponent();

        }

        private void OnSelectedCategoryChanged(object sender, ObjectIdentifierEventArgs e)
        {
            Value = listCategories.SelectedCategoryId;
        }

        private void ExCategory_Expanded(object sender, RoutedEventArgs e)
        {
            SetCategoryLabel(listCategories.SelectedNode);
        }

        private void ExCategory_Collapsed(object sender, RoutedEventArgs e)
        {
            SetCategoryLabel(listCategories.SelectedNode);
        }
        private void SetCategoryLabel(TreeNode node)
        {
            labelCategory.Text = exCategory.IsExpanded || node == null ?
                "Title" : "Title: " + node.Text;
        }
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CategorySelector)d).SetCategory((int)e.NewValue);
        }
        private bool SetCategory(int id)
        {
            var node = listCategories.SelectNode(id);
            SetCategoryLabel(node);
            return (node != null);
        }

    }
}
