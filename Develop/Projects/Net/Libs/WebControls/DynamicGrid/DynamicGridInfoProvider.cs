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

namespace Savchin.Web.UI
{
    /// <summary>
    /// Provides information about grid and it's child objects
    /// </summary>
    public class DynamicGridInfoProvider
    {
        #region Fields

        private DynamicGrid _grid = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets id of header check box
        /// </summary>
        public string HeaderCheckBoxID
        {
            get { return _grid.ClientID + "CheckBox"; }
        }

        /// <summary>
        /// Gets checkbox object name
        /// </summary>
        public string HeaderCheckBoxObjName
        {
            get { return _grid.ClientID + "CheckObj"; }
        }

        /// <summary>
        /// Return name of checkbox callback function
        /// </summary>
        public string CheckBoxCreatedFuncName
        {
            get { return _grid.ClientID + "RowCheckBoxCreated"; }
        }

        /// <summary>
        /// Prefix for row checkboxes
        /// </summary>
        public string RowCheckBoxPrefix
        {
            get { return _grid.ClientID + "ChildCheck"; }
        }

        /// <summary>
        /// Checkbox click handler name
        /// </summary>
        public string HeaderCheckBoxClickHandlerName
        {
            get { return HeaderCheckBoxObjName + "Click"; }
        }

        /// <summary>
        /// Gets name of sort function
        /// </summary>
        public string SortFuncName
        {
            get { return _grid.ClientID + "OnSort"; }
        }

        /// <summary>
        /// Get name of object which will store url for xml retriaval
        /// </summary>
        public string XmlSourceUrlObj
        {
            get { return _grid.ClientID + "XmlUrl"; }
        }

        #endregion

        #region Contruction

        /// <summary>
        /// Performs object initialization with specified grid id
        /// </summary>
        /// <param name="gridClientId">Id of grid</param>
        public DynamicGridInfoProvider(DynamicGrid grid)
        {
            _grid = grid;
        }

        #endregion
    }
}
