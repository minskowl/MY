using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Savchin.Linq.Dynamic.Core;

namespace Savchin.Linq.Dynamic
{
    public static class DynamicExpression
    {
        /// <summary>
        /// Parses the specified result type.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static Expression Parse(Type resultType, string expression, params object[] values)
        {
            var parser = new ExpressionParser(null, expression, values);
            return parser.Parse(resultType);
        }

        /// <summary>
        /// Parses the lambda.
        /// </summary>
        /// <param name="itType">It type.</param>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
        {
            return ParseLambda(new ParameterExpression[] { Expression.Parameter(itType, "") }, resultType, expression, values);
        }

        /// <summary>
        /// Parses the lambda.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            var expressionBody = new ExpressionParser(parameters, expression, values).Parse(resultType);
            return Expression.Lambda(expressionBody, parameters);
        }

        /// <summary>
        /// Parses the lambda.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
        {
            return (Expression<Func<T, S>>)ParseLambda(typeof(T), typeof(S), expression, values);
        }

        /// <summary>
        /// Creates the class.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static Type CreateClass(params DynamicProperty[] properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }

        /// <summary>
        /// Creates the class.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static Type CreateClass(IEnumerable<DynamicProperty> properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }
    }
}