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
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Specifies column alignment
    /// </summary>
    public enum ColumnAlignment
    {
        /// <summary>
        /// Column content will be aligned by right border
        /// </summary>
        Right,
        /// <summary>
        /// Column content will be aligned by right border
        /// </summary>
        Left,
        /// <summary>
        /// Column content will be aligned at center
        /// </summary>
        Center
    }

    /// <summary>
    /// Holds description of dynamic column column
    /// </summary>
    [Serializable]
    public class GridColumn
    {
        #region Fields

        private string          _text       = "Column";
        private Unit            _width      = new Unit(30);
        private ColumnAlignment  _alignment = ColumnAlignment.Left;
        private string               _columnType = "ed";
        private string             _dataKey = string.Empty;
        private string       _headerPattern = string.Empty;
        private bool             _applySort = false;
        private int?              _minWidth = null;

        #endregion

        #region Properties

        /// <summary>
        /// Column caption
        /// </summary>
        public string Text
        {
            get { return _text;  }
            set { _text = value; }
        }

        /// <summary>
        /// Width of column
        /// </summary>
        public Unit Width
        {
            get { return _width;  }
            set { _width = value; }
        }

        /// <summary>
        /// Column content alignment
        /// </summary>
        public ColumnAlignment Alignment
        {
            get { return _alignment;  }
            set { _alignment = value; }
        }

        /// <summary>
        /// Type of column data
        /// </summary>
        public string ColumnType
        {
            get { return _columnType;  }
            set { _columnType = value; }
        }

        /// <summary>
        /// Name of column
        /// </summary>
        public string DataKey
        {
            get { return _dataKey;  }
            set { _dataKey = value; }
        }

        /// <summary>
        /// Patten used to display header
        /// </summary>
        public string HeaderPattern
        {
            get { return _headerPattern;  }
            set { _headerPattern = value; }
        }

        /// <summary>
        /// Minimal column width
        /// </summary>
        public int? MinWidth
        {
            get { return _minWidth;  }
            set { _minWidth = value; }
        }

        /// <summary>
        /// Specifies whether sort should be applied of not
        /// </summary>
        public bool ApplySort
        {
            get { return _applySort;  }
            set { _applySort = value; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Default contructor
        /// </summary>
        public GridColumn()
        {
        }

        /// <summary>
        /// Non-default contructor
        /// </summary>
        /// <param name="text">Column name</param>
        /// <param name="width">Column width</param>
        /// <param name="align">Column alignment</param>
        public GridColumn(string text, Unit width, ColumnAlignment align)
        {
            _text = text;
            _width = width;
            _alignment = align;
        }

        /// <summary>
        /// Non-default contructor
        /// </summary>
        /// <param name="text">Column name</param>
        /// <param name="width">Column width</param>
        /// <param name="align">Column alignment</param>
        /// <param name="dataKey">Name of column dataset</param>
        public GridColumn(string text, Unit width, ColumnAlignment align, string dataKey):
            this(text, width, align)
        {
            _dataKey = dataKey;
        }

        /// <summary>
        /// Non-default contructor
        /// </summary>
        /// <param name="text">Column name</param>
        /// <param name="width">Column width</param>
        /// <param name="align">Column alignment</param>
        /// <param name="dataKey">Name of column dataset</param>
        /// <param name="applySort">Specified whether sort should be applied or not</param>
        public GridColumn(string text, Unit width, ColumnAlignment align, 
            string dataKey, bool applySort):
            this(text, width, align, dataKey)
        {
            _applySort = applySort;
        }

        #endregion
    }

}
