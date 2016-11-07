﻿using System.Windows;


namespace Savchin.Wpf.Converters.Expressions.Nodes
{
    internal sealed class TernaryConditionalNode : TernaryNode
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(TernaryConditionalNode));

        protected override string OperatorSymbols
        {
            get
            { 
                return "?";
            }
        }

        public TernaryConditionalNode(Node firstNode, Node secondNode, Node thirdNode)
            : base(firstNode, secondNode, thirdNode)
        {
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            var firstNodeValue = this.FirstNode.Evaluate(evaluationContext);

            if (firstNodeValue == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var firstNodeValueType = GetNodeValueType(firstNodeValue);

            exceptionHelper.ResolveAndThrowIf(firstNodeValueType != NodeValueType.Boolean, "FirstNodeMustBeBoolean", this.OperatorSymbols, firstNodeValueType);

            if ((bool)firstNodeValue)
            {
                return this.SecondNode.Evaluate(evaluationContext);
            }
            else
            {
                return this.ThirdNode.Evaluate(evaluationContext);
            }
        }
    }
}
