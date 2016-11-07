using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Savchin.Collection.Sorting;
using Savchin.Collection.Generic.Sorting;

namespace Savchin.Collection.Sorting
{
    /// <summary>
    /// Base class of all ComparerBuilder that contains common members and logic.
    /// </summary>
    public class ComparerBuilderBase
    {
        protected class StringListComparer : IEqualityComparer<List<String>>
        {
            #region IEqualityComparer<List<String>> Members

            public bool Equals(List<String> x, List<String> y)
            {
                if (x.Count != y.Count) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (StringComparer.CurrentCultureIgnoreCase.Equals(x[i], y[i]) == false)
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(List<String> obj)
            {
                int hash = 0;
                for (int i = 0; i < obj.Count; i++)
                {
                    hash = 31 * hash + StringComparer.CurrentCultureIgnoreCase.GetHashCode(obj[i]);
                }
                return hash;
            }

            #endregion
        }
        
        /// <summary>
        /// MethodInfo of String.Compare method used to compare string instances.
        /// </summary>
        protected static readonly MethodInfo stringCompareMethod = new Func<String, String, StringComparison, int>(String.Compare).Method; //  typeof(String).GetMethod("Compare", new Type[] { typeof(String), typeof(String), typeof(StringComparison) });
        
        /// <summary>
        /// MethodInfo of Nullable.Compare used to compare Nullable instances.
        /// </summary>
        protected static readonly MethodInfo nullableGenericCompareMethod = typeof(Nullable).GetMethod("Compare");

        /// <summary>
        /// MethodInfo for IComparable.CompareTo interface method.
        /// </summary>
        protected static readonly MethodInfo nonGenericCompareToMethod = typeof(IComparable).GetMethod("CompareTo");
        
        /// <summary>
        /// MethodInfo of ComparerBuilderBase.Compare method used to compare objects where the comparison cannot be infered at dynamic compile time.
        /// </summary>
        protected static readonly MethodInfo generalCompareMethod = new Func<Object, Object, int>(Compare).Method;
        
        /// <summary>
        /// Helper that returns the implementation of the given interface method for a given source type.
        /// </summary>
        /// <param name="sourceType">Type to search for an implementation in.</param>
        /// <param name="interfaceMethod">Interface method to search for.</param>
        /// <returns>The implementing method in sourceType or null if given interface method not implemented in source class.</returns>
        protected static MethodInfo GetMethodImplementation(Type sourceType, MethodInfo interfaceMethod)
        {
            Type interfaceType = interfaceMethod.ReflectedType;
            if (interfaceType.IsAssignableFrom(sourceType))
            {
                InterfaceMapping map = sourceType.GetInterfaceMap(interfaceMethod.ReflectedType);
                int index = Array.IndexOf(map.InterfaceMethods, interfaceMethod);
                if (index < 0)
                {
                    return null;
                }
                return map.TargetMethods[index];

            }
            return null;

        }

        /// <summary>
        /// Helper to get a member (field or property) with the specified name.
        /// </summary>
        /// <param name="type">Type to search in.</param>
        /// <param name="name">Name of member to search for</param>
        /// <returns>MemberInfo representing the field or property.</returns>
        /// <remarks>
        /// Only public properties and fields are included in the search. 
        /// </remarks>
        protected static MemberInfo GetMemberByName(Type type, String name)
        {
            PropertyInfo property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null && property.GetGetMethod() != null)
            {
                return property;
            }
            FieldInfo field = type.GetField(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return field;
        }

        /// <summary>
        /// Creates an expression that represents a getter for the specified property expression.
        /// </summary>
        /// <paramref name="propParts"/> represents a property expression prop0.prop1.prop2 etc where "prop0" is stored in propParts[0] and 
        /// "prop1" is stored in propParts[1], etc.</param>
        /// <param name="baseExpression">Base expression to retrieve the given property or field from, e.g. a ParameterExpression</param>
        /// <returns>An Expression to retrieve the specified property from baseExpression.</returns>
        public static Expression BuildNullSafeGetter(List<String> propParts, Expression baseExpression)
        {
            if (propParts == null) throw new ArgumentNullException("propParts");
            if (propParts.Count == 0) throw new ArgumentException("propParts must not be empty.");
            if (baseExpression == null) throw new ArgumentNullException("baseExpression");
            return BuildNullSafeGetter(propParts, 0, baseExpression);
        }

        /// <summary>
        /// Helper method to build an expression that represents a getter for the specified property expression (recursively).
        /// </summary>
        /// <param name="propParts">Represents the property to get as a list of property names.</param>
        /// <param name="partIndex">Current index in propParts list to build for.</param>
        /// <param name="baseExpression">Base expression to retrieve the given property or field from, e.g. a ParameterExpression</param>
        /// <returns>An Expression to retrieve the specified property.</returns>
        /// <exception cref="System.ArgumentException">If one of the items in propParts is not recognized as a public field or property.</exception>
        protected static Expression BuildNullSafeGetter(List<String> propParts, int partIndex, Expression baseExpression)
        {
            String memberName = propParts[partIndex];
            
            MemberInfo member = GetMemberByName(baseExpression.Type, memberName);
            if (member == null) throw new ArgumentException("'"+propParts[partIndex]+"' not a public property or field.");

            Expression memberExpr = Expression.MakeMemberAccess(baseExpression, member);

            Type memberType = memberExpr.Type;
            if (partIndex == propParts.Count - 1)
            {
                if (memberType.IsEnum)
                {
                    // Treat enums as its underlying type to avoid boxing when using non-generic IComparable interface.
                    return Expression.Convert(memberExpr, Enum.GetUnderlyingType(memberExpr.Type));
                }
                return memberExpr;
            }
            else
            {

                if (memberType.IsValueType)
                {
                    if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(Nullable<>)) // If Nullable
                    {
                        ParameterExpression param = Expression.Parameter(memberType, memberName);
                        Expression childExpr = BuildNullSafeGetter(propParts, partIndex + 1, Expression.Property(param, "Value"));
                        Type childType = childExpr.Type;

                        // Lift to nullable if necessary.
                        if (childType.IsValueType && Nullable.GetUnderlyingType(childType) == null)
                        {
                            childType = typeof(Nullable<>).MakeGenericType(childType);
                            childExpr = Expression.Convert(childExpr, childType);
                        }

                        return Expression.Invoke(Expression.Lambda(
                                                     Expression.Condition(Expression.Property(param, "HasValue"),
                                                                          childExpr,
                                                                          Expression.Constant(null, childType)),
                                                     param),
                                                 memberExpr);
                    }
                    else
                    {
                        return BuildNullSafeGetter(propParts, partIndex + 1, memberExpr);
                    }
                }
                else // if reference type
                {
                    ParameterExpression param = Expression.Parameter(memberType, memberName);
                    Expression childExpr = BuildNullSafeGetter(propParts, partIndex + 1, param);

                    Type childType = childExpr.Type;

                    // Lift to nullable if necessary, i.e. if the child expression is a non-nullable type.
                    if (childType.IsValueType && Nullable.GetUnderlyingType(childType) == null)
                    {
                        childType = typeof(Nullable<>).MakeGenericType(childType);
                        childExpr = Expression.Convert(childExpr, childType);
                    }

                    return Expression.Invoke(Expression.Lambda(
                                                 Expression.Condition(Expression.Equal(param, Expression.Constant(null)),
                                                                      Expression.Constant(null, childType),
                                                                      childExpr),
                                                 param),
                                             memberExpr);

                }
            }

        }

        /// <summary>
        /// Compares two objects whose type was not known at dynamic compile time.
        /// </summary>
        /// <param name="o1">First object to compare</param>
        /// <param name="o2">Second object to compare</param>
        /// <returns>A value less than zero if o1 &lt; o2, zero if o1 = o2, A value greater than zero if o1 &gt; o2.</returns>
        public static int Compare(Object o1, Object o2)
        {
            if (o1 == null)
            {
                if( o2 != null ) return -1;
                return 0;
            }
            else
            {
                if (o2 == null) return 1;
                if (o1 is String && o2 is String)
                {
                    return String.Compare((String)o1, (String)o2, StringComparison.CurrentCultureIgnoreCase);
                }
                IComparable c1 = o1 as IComparable;
                if (c1 != null)
                {
                    return c1.CompareTo(o2);
                }
                return 0;
            }
        }

        
        ///// <summary>
        ///// Creates a comparer for a DataRow.
        ///// </summary>
        ///// <typeparam name="TRow">Type of DataRow</typeparam>
        ///// <param name="source">EnumerableRowCollection&lt;T&gt; representing the data row collection.</param>
        ///// <param name="sortExpression">SQL-like sort expression specifying the sort order.</param>
        ///// <returns>An IComparer&lt;TRow&gt; that compares two rows.</returns>
        ///// <remarks>This method is not used in the OrderBy implementation for EnumerableRowCollections, because
        ///// better performance was obtained by dynamically creating field accessors instead.</remarks>
        //public static IComparer<TRow> CreateDataRowComparer<TRow>(EnumerableRowCollection<TRow> source, string sortExpression) where TRow : DataRow
        //{
        //    if (source == null) throw new ArgumentNullException("source");
        //    if (sortExpression == null) throw new ArgumentNullException("sortExpression");
        //    DataColumnCollection columns;
        //    using( IEnumerator<TRow> enumerator = source.GetEnumerator() )
        //    {
        //        if (enumerator.MoveNext() == false) return new TypeComparer<TRow>(new Comparison<TRow>[0]);
        //        columns = enumerator.Current.Table.Columns;
        //    }
        //    List<Comparison<TRow>> comparisons = new List<Comparison<TRow>>(4);

        //    var tokenizer = new SimpleTokenizer(sortExpression);
        //    do
        //    {
        //        String colName = tokenizer.ReadIdentity();
        //        if (colName.Length == 0)
        //        {
        //            throw new ParserException(tokenizer.Position, tokenizer.Expression, "Column name expected.");
        //        }
        //        int colIndex = columns.IndexOf(colName);
        //        if (colIndex < 0) throw new ParserException(tokenizer.Position, tokenizer.Expression, "Column '" + colName + "' not found in table.");
        //        DataColumn column = columns[colIndex];
        //        Comparison<TRow> comparison;
        //        if (column.AllowDBNull)
        //        {
        //            if (column.DataType == typeof(String))
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     if (r1.IsNull(colIndex))
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 0;
        //                                         return -1;
        //                                     }
        //                                     else
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 1;
        //                                         return String.Compare((String)r1[colIndex], (String)r2[colIndex], StringComparison.CurrentCultureIgnoreCase);
        //                                     }
        //                                 };
        //            }
        //            else if (typeof(IComparable).IsAssignableFrom(column.DataType))
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     if (r1.IsNull(colIndex))
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 0;
        //                                         return -1;
        //                                     }
        //                                     else
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 1;
        //                                         return ((IComparable)r1[colIndex]).CompareTo(r2[colIndex]);
        //                                     }
        //                                 };
        //            }
        //            else
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     if (r1.IsNull(colIndex))
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 0;
        //                                         return -1;
        //                                     }
        //                                     else
        //                                     {
        //                                         if (r2.IsNull(colIndex)) return 1;
        //                                         return ComparerBuilderBase.Compare(r1[colIndex], r2[colIndex]);
        //                                     }
        //                                 };
        //            }
        //        }
        //        else
        //        {
        //            if (column.DataType == typeof(String))
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     return String.Compare((String)r1[colIndex], (String)r2[colIndex], StringComparison.CurrentCultureIgnoreCase);
        //                                 };
        //            }
        //            else if (typeof(IComparable).IsAssignableFrom(column.DataType))
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     return ((IComparable)r1[colIndex]).CompareTo(r2[colIndex]);
        //                                 };
        //            }
        //            else
        //            {
        //                comparison = delegate(TRow r1, TRow r2)
        //                                 {
        //                                     return ComparerBuilderBase.Compare(r1[colIndex], r2[colIndex]);
        //                                 };
        //            }
        //        }
        //        if (tokenizer.AdvanceIfIdent("DESC") || tokenizer.AdvanceIfIdent("DESCENDING"))
        //        {
        //            comparisons.Add((x,y) => -comparison(x, y)) ;
        //        }
        //        else if (tokenizer.AdvanceIfIdent("ASC") || tokenizer.AdvanceIfIdent("ASCENDING") || true )
        //        {
        //            comparisons.Add(comparison);
        //        }
                
        //    }
        //    while( tokenizer.AdvanceIfSymbol(',') );
        //    tokenizer.ExpectEnd();

        //    return new TypeComparer<TRow>(comparisons.ToArray());
        //}
    }
}