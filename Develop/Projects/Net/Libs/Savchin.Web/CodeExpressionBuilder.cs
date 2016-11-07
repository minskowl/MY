using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace Savchin.Web
{
    /// <summary>
    /// CodeExpressionBuilder
    /// </summary>
    [ExpressionPrefix("QueryString")]
    public class CodeExpressionBuilder : ExpressionBuilder
    {
        /// <summary>
        /// When overridden in a derived class, returns code that is used during page execution to obtain the evaluated expression.
        /// </summary>
        /// <param name="entry">The object that represents information about the property bound to by the expression.</param>
        /// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)"/>.</param>
        /// <param name="context">Contextual information for the evaluation of the expression.</param>
        /// <returns>
        /// A <see cref="T:System.CodeDom.CodeExpression"/> that is used for property assignment.
        /// </returns>
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry,object parsedData, ExpressionBuilderContext context)
        {
            return new CodeSnippetExpression(entry.Expression);
        }
    }
}
