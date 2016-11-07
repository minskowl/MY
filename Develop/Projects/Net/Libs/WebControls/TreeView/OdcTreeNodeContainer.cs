using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// A container for the NodeTemplate and EditNodeTemplate for a OdcTreeNode.
    /// </summary>
    public class OdcTreeNodeContainer:WebControl,IDataItemContainer,INamingContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeContainer"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataIndex">Index of the data.</param>
        public OdcTreeNodeContainer(OdcTreeNode node, int dataIndex)
        {
            this.dataItem = node.DataItem;
            this.dataItemIndex = dataIndex;
            this.displayIndex = dataIndex;
            this.node = node;
        }


        internal OdcTreeNode node;
        private object dataItem;
        private int dataItemIndex;
        private int displayIndex;

        /// <summary>
        /// Gets or sets the sub class.
        /// </summary>
        /// <value>The sub class.</value>
        public string SubClass { get; set; }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <value>The node.</value>
        public OdcTreeNode Node
        {
            get { return node; }
        }

        #region IDataItemContainer Members

        /// <summary>
        /// Gets the data item associated with this node if data bound, otherwise null.
        /// </summary>
        public object DataItem
        {
            get { return node.DataItem; }
        }

        /// <summary>
        /// Gets the index of the associated data item if data bound, otherwise 0.
        /// </summary>
        public int DataItemIndex
        {
            get { return dataItemIndex; }
        }


        /// <summary>
        /// When implemented, gets the position of the data item as displayed in a control.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An Integer representing the position of the data item as displayed in a control.
        /// </returns>
        public int DisplayIndex
        {
            get { return displayIndex; }
        }

        #endregion


        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.RenderContents(writer);
        }


        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.
        /// </returns>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Span;
            }
        }
    }
}
