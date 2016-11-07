using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Savchin.Wpf.Controls.ListViewLayout
{
    public sealed class SortedColumn : LayoutColumn
    {
        #region SortExpression
        // Using a DependencyProperty as the backing store for SortExpression.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortExpressionProperty =
            DependencyProperty.RegisterAttached("SortExpression", typeof(string), typeof(SortedColumn), new UIPropertyMetadata(null));

        public static string GetSortExpression(DependencyObject obj)
        {
            return (string)obj.GetValue(SortExpressionProperty);
        }

        public static void SetSortExpression(DependencyObject obj, string value)
        {
            obj.SetValue(SortExpressionProperty, value);
        } 
        #endregion


        #region SortDirection

        public static ListSortDirection? GetSortDirection(DependencyObject obj)
        {
            return (ListSortDirection?)obj.GetValue(SortDirectionProperty);
        }

        public static void SetSortDirection(DependencyObject obj, ListSortDirection? value)
        {
            obj.SetValue(SortDirectionProperty, value);
        }

        // Using a DependencyProperty as the backing store for SortDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.RegisterAttached("SortDirection", typeof(ListSortDirection?), typeof(SortedColumn),
            new UIPropertyMetadata(null)); 
        #endregion



    }
}
