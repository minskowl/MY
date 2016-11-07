using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// OdcTreeViewCommandEventArgs
    /// </summary>
    public class OdcTreeViewCommandEventArgs:CommandEventArgs
    {
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public OdcTreeNode Node { get; private set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public object Source { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeViewCommandEventArgs"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        /// <param name="node">The node.</param>
        public OdcTreeViewCommandEventArgs(Object source, CommandEventArgs e, OdcTreeNode node)
            : base(e)
        {
            this.Node = node;
            this.Source = source;
        }
    }

    /// <summary>
    /// OdcTreeViewCommandEventHandler
    /// </summary>
    public delegate void OdcTreeViewCommandEventHandler(object sender, OdcTreeViewCommandEventArgs e);
}
