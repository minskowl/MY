using System;
using Savchin.Core;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core
{
    /// <summary>
    /// Corresponds to the FilterCurrentData templates (DataTemplate) 
    /// of the DataGridColumnFilter defined in the Generic.xaml>
    /// </summary>
    public enum FilterType
    {
        Numeric,
        NumericBetween,
        Text,
        List,
        Boolean,
        DateTime,
        DateTimeBetween
    }

    internal static class FilterTypeHelper
    {
        /// <summary>
        /// Gets the type of the filter.
        /// </summary>
        /// <param name="valuePropertyType">Type of the value property.</param>
        /// <param name="isAssignedDataGridColumnComboDataGridColumn">if set to <c>true</c> [is assigned data grid column combo data grid column].</param>
        /// <param name="isBetweenType">if set to <c>true</c> [is between type].</param>
        /// <returns></returns>
        internal static FilterType GetFilterType(Type valuePropertyType, bool isAssignedDataGridColumnComboDataGridColumn, bool isBetweenType)
        {
            if (isAssignedDataGridColumnComboDataGridColumn)
            {
                return FilterType.List;
            }
            if (valuePropertyType == typeof(Boolean) || valuePropertyType == typeof(bool?))
            {
                return FilterType.Boolean;
            }
            if (valuePropertyType.IsNumeric())
            {
                return isBetweenType ? FilterType.NumericBetween : FilterType.Numeric;
            }
            if (valuePropertyType == typeof(DateTime) || valuePropertyType == typeof(DateTime?))
            {
                return isBetweenType ? FilterType.DateTimeBetween : FilterType.DateTime;
            }
            return FilterType.Text;
        }
    }
}
