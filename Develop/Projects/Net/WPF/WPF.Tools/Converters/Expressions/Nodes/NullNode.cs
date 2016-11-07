namespace Savchin.Wpf.Converters.Expressions.Nodes
{
    internal sealed class NullNode : Node
    {
        public static readonly NullNode Instance = new NullNode();

        private NullNode()
        {
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            return null;
        }
    }
}
