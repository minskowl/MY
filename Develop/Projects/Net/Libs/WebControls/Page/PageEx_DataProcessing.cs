using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Savchin.Web.UI
{
    public partial class PageEx
    {
        #region Constants
        private const string _handlerKeyDataSource = "DataSource";
        private const string _handlerKeyAjax = "Ajax";

        private const string _dymanicDataRequestParamName = "DDR";
        private const string _dymanicDataHandler = "DDH";
        private const string _dymanicDataType = "DDT";
        private const string _dymanicDataSourceId = "DDID";
        private const string _startIndexParamName = "posStart";
        private const string _itemCountParamName = "count";
        private const string _dynamicDataColumnCount = "DDCC";
        private const string _columnKey = "columnKey";
        private const string _isAscSort = "sortAsc";

        #endregion

        #region Fields

        private string _callbackArguments = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Specifies type of content for xml
        /// </summary>
        private string ContentTypeName
        {
            get
            {
                return Request["HTTP_ACCEPT"] == "application/xhtml+xml" ? "application/xhtml+xml" : "text/xml";
            }
        }

        #region Request arguments

        /// <summary>
        /// Checks if current request if dymanic data request
        /// </summary>
        public bool IsAjaxDataRequest
        {
            get
            {
                if (Request[_dymanicDataRequestParamName] != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Gets the data source id.
        /// </summary>
        public string DataSourceId
        {
            get
            {
                return Request[_dymanicDataSourceId];
            }
        }

        /// <summary>
        /// Gets start index of items
        /// </summary>
        public long? StartIndex
        {
            get
            {
                if (Request[_startIndexParamName] == null)
                    return null;
                return long.Parse(Request[_startIndexParamName]);
            }
        }

        /// <summary>
        /// Gets item count to get from database
        /// </summary>
        public long ItemCount
        {
            get
            {
                if (Request[_itemCountParamName] == null)
                    return -1;
                return long.Parse(Request[_itemCountParamName]);
            }
        }





        /// <summary>
        /// Returns sort direction flag
        /// </summary>
        /// <value>The is asc sort.</value>
        /// <returns>True of false if sort direction was detected</returns>
        public bool? IsAscSort
        {
            get
            {
                if (Request[_isAscSort] == null)
                    return null;
                return bool.Parse(Request[_isAscSort]);
            }
        }

        /// <summary>
        /// Returns count of cells in requested XML
        /// </summary>
        /// <value>The column count.</value>
        /// <returns>Data type value</returns>
        public long ColumnCount
        {
            get
            {
                if (Request[_dynamicDataColumnCount] == null)
                    return -1;
                return long.Parse(Request[_dynamicDataColumnCount]);
            }
        }


        #endregion

        #endregion

        #region Static methods

        private static string GetHadlerURL(string handlerId)
        {
            return HttpContext.Current.Request.Path + "?" +
                   _dymanicDataRequestParamName + "=1&" +
                   _dymanicDataHandler + "=" + handlerId;
        }
        /// <summary>
        /// Gets the AJAX hadler URL.
        /// </summary>
        /// <returns></returns>
        public static string GetAJAXHadlerURL(AJAXHandler handler)
        {
            return GetHadlerURL(_handlerKeyAjax) + "&" + _dymanicDataSourceId + "=" + handler.ClientID;
        }

        /// <summary>
        /// Gets the dymanic data request URL.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <returns></returns>
        public static string GetDymanicDataRequestUrl(DynamicGrid grid)
        {
            StringBuilder builder = new StringBuilder(GetHadlerURL(_handlerKeyDataSource));
            builder.Append("&" + _dynamicDataColumnCount + "=" + grid.Columns.Count);

            if (!string.IsNullOrEmpty(grid.AssociatedDataKey))
                builder.Append("&" + _dymanicDataType + "=" + HttpUtility.UrlEncode(grid.AssociatedDataKey));


            if (!string.IsNullOrEmpty(grid.DataSourceID))
            {
                DynamicGridDataSource dataSource = ControlHelper.FindControl(grid, grid.DataSourceID) as DynamicGridDataSource;
                builder.AppendFormat("&{0}={1}",
                                       _dymanicDataSourceId,
                                        dataSource == null ? grid.DataSourceID : HttpUtility.UrlEncode(dataSource.ClientID));

            }

            if (!string.IsNullOrEmpty(grid.AdditionalXmlQueryParams))
                builder.Append("&" + grid.AdditionalXmlQueryParams);
            return builder.ToString();
        }


        #endregion


        /// <summary>
        /// Initializes the <see cref="T:System.Web.UI.HtmlTextWriter"/> object and calls on the child controls of the <see cref="T:System.Web.UI.Page"/> to render.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> that receives the page content.</param>
        protected override void Render(HtmlTextWriter writer)
        {

            if (IsAjaxDataRequest)
            {
                Response.Clear();
                Response.ContentType = ContentTypeName;
                try
                {
                    ProcessResponseData();
                }
                catch (Exception ex)
                {
                    Util.Log.Fatal("PageEx.Render.ProcessResponseData", ex);
                }
                Response.End();
                Response.Close();
            
            }
            else
            {
                base.Render(writer);
            }


        }



        #region Virtual methods

        /// <summary>
        /// Sends the data XML.
        /// </summary>
        private void ProcessResponseData()
        {
            DataRequestEventArgs args = new DataRequestEventArgs(
                Request[_dymanicDataHandler],
                Request[_dymanicDataType],
                StartIndex,
                ItemCount,
                ColumnCount,
                Request[_columnKey],
                IsAscSort);

            switch (args.HandlerType)
            {
                case _handlerKeyDataSource:
                    GetDataFromDataSource(args, DataSourceId);
                    break;
                case _handlerKeyAjax:
                    GetDataFromAJAXSource(args);
                    break;
                default:
                    break;
            }


        }

        private void GetDataFromAJAXSource(DataRequestEventArgs args)
        {
            AJAXHandler handler = (AJAXHandler)FindControl(DataSourceId.Replace('_', '$'));
            if (handler != null && handler.ContentProcessor != null)
                Response.Write(handler.ContentProcessor(args));
        }

        private void GetDataFromDataSource(DataRequestEventArgs args, string dataSourceId)
        {
            if (!string.IsNullOrEmpty(dataSourceId))
            {
                dataSourceId = dataSourceId.Replace('_', '$');
                DynamicGridDataSource dataSource = (DynamicGridDataSource)FindControl(dataSourceId);
                if (dataSource == null)
                {
                    OnDataSourceNotFound(dataSourceId);
                    dataSource = (DynamicGridDataSource)FindControl(dataSourceId);
                }
                if (dataSource != null)
                    Response.Write(dataSource.GetDataXml(args));
            }
            else
                OnGetGridXmlData(args);
        }

        /// <summary>
        /// Called when data source was not found in collection
        /// </summary>
        /// <param name="dataSourceId">Id of data source which was not found</param>
        protected virtual void OnDataSourceNotFound(string dataSourceId)
        {
        }


        #endregion

        /// <summary>
        /// Is used to provide grid data
        /// </summary>
        /// <param name="args">The <see cref="NewWayMedia.Common.Controls.DataRequestEventArgs"/> instance containing the event data.</param>
        protected virtual void OnGetGridXmlData(DataRequestEventArgs args)
        {
            Response.Write(args.Output);
        }

        /// <summary>
        /// Pefroms processing of java script callback
        /// </summary>
        /// <param name="callbackArguments">Parameters of call</param>
        /// <returns>String which presents call results</returns>
        protected virtual string OnJavaScriptCallback(string callbackArguments)
        {
            return string.Empty;
        }

        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="source">The source.</param>
        public void AddDataSource(DynamicGridDataSource source)
        {
            Controls.Add(source);
        }

        #region ICallbackEventHandler Members

        /// <summary>
        /// Returns call back processing result
        /// </summary>
        /// <returns>Returns result string</returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            return OnJavaScriptCallback(_callbackArguments);
        }

        /// <summary>
        /// Called during java script request
        /// </summary>
        /// <param name="eventArgument">Params of java script call</param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            _callbackArguments = eventArgument;
        }

        #endregion
    }
}
