#region Version & Copyright
/* 
 * $Id: DynamicGridDelegateDataSource.cs 18730 2007-07-07 12:33:13Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System.Data;

namespace Savchin.Web.UI
{
    public delegate DataTable DynamicGridDataTableSource(DataRequestEventArgs args);

    public class DynamicGridDelegateDataSource : DynamicGridDataSource
    {

        private DynamicGridDataTableSource dataTableSource = null;
        /// <summary>
        /// Gets or sets the data table source.
        /// </summary>
        /// <value>The data table source.</value>
        public DynamicGridDataTableSource DataTableSource
        {
            get { return dataTableSource; }
            set { dataTableSource = value; }
        }
        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <returns></returns>
        public  override DataTable GetDataTable(DataRequestEventArgs args)
        {
            if (DataTableSource == null)
                return null;

            return DataTableSource(args);
        }

    }
}
