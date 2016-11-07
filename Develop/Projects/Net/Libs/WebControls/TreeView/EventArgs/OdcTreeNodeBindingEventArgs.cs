

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// OdcTreeNodeBindingEventArgs
    /// </summary>
    public class  OdcTreeNodeBindingEventArgs:OdcTreeNodeEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeBindingEventArgs"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="bindings">The bindings.</param>
        public OdcTreeNodeBindingEventArgs(OdcTreeNode node, OdcTreeNodeBinding binding, object dataItem, OdcTreeNodeBindings bindings)
            : base(node)
        {
            Binding = binding;
            DataItem = dataItem;
            this.Bindings = bindings;
        }

        /// <summary>
        /// Gets or sets the binding to use for this node.
        /// </summary>
        public OdcTreeNodeBinding Binding { get; set; }

        /// <summary>
        /// Gets or sets the data item;
        /// </summary>
        public object DataItem { get; set; }

        /// <summary>
        /// Gets a collection of OdcTreeNodeBindings which are possible to use.
        /// </summary>
        public OdcTreeNodeBindings Bindings { get; private set; }
    }
}