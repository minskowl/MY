#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Holds information about dynamic data request
    /// </summary>
    public class DataRequestEventArgs : EventArgs
    {
        #region Fields
        private string _handlerType = null;
        private string _dataType = null;
        private long? _startIndex = null;
        private long _count = -1;
        private long _columnCount = -1;
        private string _columnKey = null;
        private bool? _isAscSort = null;
        private string _output = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Requested data (Contacts, e-mails and etc.)
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
        }

        /// <summary>
        /// Index of start element
        /// </summary>
        public long? StartIndex
        {
            get { return _startIndex; }
        }

        /// <summary>
        /// Specifies if all content should be loaded
        /// </summary>
        public bool LoadAllContent
        {
            get { return !_startIndex.HasValue; }
        }

        /// <summary>
        /// Number of items to return
        /// </summary>
        public long Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Numer of cells in requested XML
        /// </summary>
        public long ColumnCount
        {
            get { return _columnCount; }
        }

        /// <summary>
        /// Column key
        /// </summary>
        public string ColumnKey
        {
            get { return _columnKey; }
        }

        /// <summary>
        /// Specifies sort direction
        /// </summary>
        public bool? IsAscSort
        {
            get { return _isAscSort; }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; }
        }

        /// <summary>
        /// Gets or sets the type of the handler.
        /// </summary>
        /// <value>The type of the handler.</value>
        public string HandlerType
        {
            get { return _handlerType; }
            set { _handlerType = value; }
        }

        #endregion

        #region Construction


        /// <summary>
        /// Non-default contructor
        /// </summary>
        public DataRequestEventArgs(string handlerType ,string dataType,
            long? startIndex, long count, long columnCount, string columnKey, bool? isAscSort)
        {
            _handlerType = handlerType;
            _columnCount = columnCount;
            _columnKey   = columnKey;
            _isAscSort  = isAscSort; 
            _dataType = dataType;
            _startIndex = startIndex;
            _count = count;

            if (_startIndex.HasValue)
            {
                if (_startIndex < 0)
                {
                    _count += _startIndex.Value;
                    _startIndex = 0;
                }

                if (_count < 0)
                    _count = 0;
            }


        }

        #endregion
    }
}
