using System.Collections;
using System.Windows.Controls;


namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core.Querying
{
    public class QueryControllerFactory
    {
        /// <summary>
        /// Gets the query controller.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="filterData">The filter data.</param>
        /// <param name="itemsSource">The items source.</param>
        /// <returns></returns>
        public static QueryController GetQueryController(System.Windows.Controls.DataGrid dataGrid, FilterData filterData, IEnumerable itemsSource)
        {
            var controller = DataGridExtensions.GetDataGridFilterQueryController(dataGrid);

            if (controller == null)
            {
                controller = new QueryController();
                DataGridExtensions.SetDataGridFilterQueryController(dataGrid, controller);
            }

            controller.ColumnFilterData = filterData;
            controller.ItemsSource = itemsSource;
            controller.CallingThreadDispatcher = dataGrid.Dispatcher;
            controller.UseBackgroundWorker = DataGridExtensions.GetUseBackgroundWorkerForFiltering(dataGrid);

            return controller;
        }
    }
}
