
using System.Windows;
using System.Windows.Controls;

namespace Savchin.Wpf.Controls.ListViewLayout
{
    // ------------------------------------------------------------------------
    public sealed class FixedColumn : LayoutColumn
    {
        // ----------------------------------------------------------------------
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached(
                "Width",
                typeof (double),
                typeof (FixedColumn));

        // ----------------------------------------------------------------------
        private FixedColumn()
        {
        } // FixedColumn

        // ----------------------------------------------------------------------
        public static double GetWidth(DependencyObject obj)
        {
            return (double) obj.GetValue(WidthProperty);
        } // GetWidth

        // ----------------------------------------------------------------------
        public static void SetWidth(DependencyObject obj, double width)
        {
            obj.SetValue(WidthProperty, width);
        } // SetWidth

        // ----------------------------------------------------------------------
        public static bool IsFixedColumn(GridViewColumn column)
        {
            if (column == null)
            {
                return false;
            }
            return HasPropertyValue(column, WidthProperty);
        } // IsFixedColumn

        // ----------------------------------------------------------------------
        public static double? GetFixedWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, WidthProperty);
        } // GetFixedWidth

        // ----------------------------------------------------------------------
        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double width)
        {
            SetWidth(gridViewColumn, width);
            return gridViewColumn;
        } // ApplyWidth
    } // class FixedColumn
} // namespace Itenso.Windows.Controls.ListViewLayout
// -- EOF -------------------------------------------------------------------