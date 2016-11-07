using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// OdcTreeNodeCheckEventArgs
    /// </summary>
    public class OdcTreeNodeCheckEventArgs:OdcTreeNodeEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeCheckEventArgs"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        public OdcTreeNodeCheckEventArgs(OdcTreeNode node, bool isChecked)
            : base(node)
        {
            this.IsChecked = isChecked;
        }
    }

    /// <summary>
    /// OdcTreeNodeCheckEventHandler
    /// </summary>
    public delegate void OdcTreeNodeCheckEventHandler(object sender, OdcTreeNodeCheckEventArgs e);
}
