using System.Windows;
using Savchin.Wpf.Controls.DataGrid.Filtering.Core.Querying;

namespace Savchin.Wpf.Controls.DataGrid.Filtering
{
    /// <summary>
    /// DataGridExtensions
    /// </summary>
    public class DataGridExtensions
    {
        #region DataGridFilterQueryControllerProperty
        public static DependencyProperty DataGridFilterQueryControllerProperty =
    DependencyProperty.RegisterAttached("DataGridFilterQueryController", typeof(QueryController), typeof(DataGridExtensions));

        /// <summary>
        /// Gets the data grid filter query controller.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static QueryController GetDataGridFilterQueryController(DependencyObject target)
        {
            return (QueryController)target.GetValue(DataGridFilterQueryControllerProperty);
        }

        /// <summary>
        /// Sets the data grid filter query controller.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public static void SetDataGridFilterQueryController(DependencyObject target, QueryController value)
        {
            target.SetValue(DataGridFilterQueryControllerProperty, value);
        } 
        #endregion

        #region ClearFilterCommandProperty
        public static DependencyProperty ClearFilterCommandProperty =
    DependencyProperty.RegisterAttached("ClearFilterCommand", typeof(DataGridFilterCommand), typeof(DataGridExtensions));

        /// <summary>
        /// Gets the clear filter command.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static DataGridFilterCommand GetClearFilterCommand(DependencyObject target)
        {
            return (DataGridFilterCommand)target.GetValue(ClearFilterCommandProperty);
        }

        /// <summary>
        /// Sets the clear filter command.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public static void SetClearFilterCommand(DependencyObject target, DataGridFilterCommand value)
        {
            target.SetValue(ClearFilterCommandProperty, value);
        } 
        #endregion

        #region IsFilterVisibleProperty
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty IsFilterVisibleProperty =
            DependencyProperty.RegisterAttached("IsFilterVisible", typeof(bool?), typeof(DataGridExtensions), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets the is filter visible.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool? GetIsFilterVisible(DependencyObject target)
        {
            return (bool)target.GetValue(IsFilterVisibleProperty);
        }

        /// <summary>
        /// Sets the is filter visible.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public static void SetIsFilterVisible(DependencyObject target, bool? value)
        {
            target.SetValue(IsFilterVisibleProperty, value);
        } 
        #endregion

        #region UseBackgroundWorkerForFilteringProperty
        public static DependencyProperty UseBackgroundWorkerForFilteringProperty =
    DependencyProperty.RegisterAttached("UseBackgroundWorkerForFiltering", typeof(bool), typeof(DataGridExtensions), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the use background worker for filtering.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool GetUseBackgroundWorkerForFiltering(DependencyObject target)
        {
            return (bool)target.GetValue(UseBackgroundWorkerForFilteringProperty);
        }

        /// <summary>
        /// Sets the use background worker for filtering.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetUseBackgroundWorkerForFiltering(DependencyObject target, bool value)
        {
            target.SetValue(UseBackgroundWorkerForFilteringProperty, value);
        } 
        #endregion

        #region IsClearButtonVisibleProperty
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty IsClearButtonVisibleProperty =
            DependencyProperty.RegisterAttached("IsClearButtonVisible", typeof(bool), typeof(DataGridExtensions), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets the is clear button visible.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool GetIsClearButtonVisible(DependencyObject target)
        {
            return (bool)target.GetValue(IsClearButtonVisibleProperty);
        }

        /// <summary>
        /// Sets the is clear button visible.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsClearButtonVisible(DependencyObject target, bool value)
        {
            target.SetValue(IsClearButtonVisibleProperty, value);
        } 
        #endregion
    }
}
