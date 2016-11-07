using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// OdcTreeNodeEventArgs
    /// </summary>
    public class OdcTreeNodeEventArgs:EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeEventArgs"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public OdcTreeNodeEventArgs(OdcTreeNode node)
            : base()
        {
            this.Node = node;
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public OdcTreeNode Node { get; private set; }
    }

    /// <summary>
    /// OdcTreeNodeEventHandler
    /// </summary>
    public delegate void OdcTreeNodeEventHandler(object sender, OdcTreeNodeEventArgs e);
}
