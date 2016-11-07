using System.Diagnostics;

namespace Savchin.Wpf.Converters.Expressions.Nodes
{
    // a node with a single child node
    internal abstract class UnaryNode : Node
    {
        private readonly Node node;

        public Node Node
        {
            get { return this.node; }
        }

        protected UnaryNode(Node node)
        {
            Debug.Assert(node != null);
            this.node = node;
        }
    }
}
