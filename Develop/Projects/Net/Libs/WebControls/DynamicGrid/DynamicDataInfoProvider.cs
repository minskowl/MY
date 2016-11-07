#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Summary description for DynamicDataInfoProvider
    /// </summary>
    public static class DynamicDataInfoProvider
    {
        #region Constants

        private const string _dymanicDataRequestParamName = "DDR";
        private const string _dymanicDataType = "DDT";
        private const string _dymanicDataSourceId = "DDID";
        private const string _startIndexParamName = "posStart";
        private const string _itemCountParamName = "count";
        private const string _dynamicDataColumnCount = "DDCC";
        private const string _columnKey = "columnKey";
        private const string _isAscSort = "sortAsc";

        #endregion

        #region Static methods

        /// <summary>
        /// Checks if current request if dymanic data request
        /// </summary>
        /// <param name="request">Request to check</param>
        /// <returns>True if this is dynamic data request</returns>
        public static bool IsDymanicDataRequest(HttpRequest request)
        {
            if (request[_dymanicDataRequestParamName] != null)
                return true;
            return false;
        }
        /// <summary>
        /// Gets the data source id.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetDataSourceId(HttpRequest request)
        {
            return request[_dymanicDataSourceId]; 
        }





        /// <summary>
        /// Gets start index of items
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns>Start index values</returns>
        public static long? GetStartIndex(HttpRequest request)
        {
            if (request[_startIndexParamName] == null)
                return null;
            return long.Parse(request[_startIndexParamName]);
        }

        /// <summary>
        /// Gets item count to get from database
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <returns>Number of items to get</returns>
        public static long GetItemCount(HttpRequest request)
        {
            if (request[_itemCountParamName] == null)
                return -1;
            return long.Parse(request[_itemCountParamName]);
        }

        /// <summary>
        /// Returns type of requested data
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns>Data type value</returns>
        public static string GetDataType(HttpRequest request)
        {
            return request[_dymanicDataType];
        }

        /// <summary>
        /// Returns column which should be used during ordering
        /// </summary>
        /// <param name="request">Request to check</param>
        /// <returns>Column name or null</returns>
        public static string GetColumnKey(HttpRequest request)
        {
            return request[_columnKey];
        }

        /// <summary>
        /// Returns sort direction flag
        /// </summary>
        /// <param name="request">Request to check</param>
        /// <returns>True of false if sort direction was detected</returns>
        public static bool? IsAscSort(HttpRequest request)
        {
            if (request[_isAscSort] == null)
                return null;
            return bool.Parse(request[_isAscSort]);
        }

        /// <summary>
        /// Returns count of cells in requested XML
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns>Data type value</returns>
        public static long GetColumnCount(HttpRequest request)
        {
            return long.Parse(request[_dynamicDataColumnCount]);
        }

        #endregion
    }
}
