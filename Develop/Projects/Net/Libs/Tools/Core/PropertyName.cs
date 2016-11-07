using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Savchin.Core
{
    /// <summary>
    /// Responsible for providing property name by LINQ member access <see cref="Expression"/>.
    /// </summary>
    public static class PropertyName
    {
        /// <summary>
        /// Returns the string name of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="expression">The expression with access to the property.</param>
        /// <returns>
        /// The name of the property as a <see cref="string"/>.
        /// </returns>
        public static string For<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }
            if (memberExpression == null || memberExpression.Member.MemberType != MemberTypes.Property)
            {
                throw new ArgumentException(@"Value must be a Property lambda expression", "expression");
            }
            return memberExpression.Member.Name;
        }
        
        /// <summary>
        /// Gets the name of the object's property. This overload may be used in static methods.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="expression">Lambda expression which specifies property.</param>
        /// <returns>Property name.</returns>
        public static string For<TObject>(Expression<Func<TObject, object>> expression)
        {
            MemberExpression memberExpression = null;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException(@"Not a member access", "expression");
            }

            return memberExpression.Member.Name;
        }
    }
}
