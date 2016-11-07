using System.Windows;
using System.Windows.Controls;

namespace Savchin.Wpf.Controls
{

    public class TreeViewEx : TreeView
    {
        #region SelectedItem Property

        public object SelectedNode
        {
            get { return (object)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(object), typeof(TreeViewEx), new UIPropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as TreeViewItem;
            if (item != null)
            {
                item.SetValue(TreeViewItem.IsSelectedProperty, true);
            }
        }

        #endregion

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);
            this.SelectedNode = e.NewValue;
        }
       
    }
}