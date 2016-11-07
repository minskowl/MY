using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Savchin.Collection.Sorting;

namespace Savchin.Collection.Generic.Sorting
{
    /// <summary>
    /// Utility class that dynamically create comparer based on property names and implements sort based on these. 
    /// </summary>
    /// <typeparam name="T">Type to be compared and sorted.</typeparam>
    /// <seealso cref="Savchin.Collection.Generic.Sorting.ComparerExtensions"/>
    public class ComparerBuilder<T> : ComparerBuilderBase
    {
        #region Private fields

        /// <summary>
        /// Cache of previously created Comparisons where keys represent property name(s) and
        /// values are Comparision delegates corresponding to these properties.
        /// </summary>
        private static readonly ThreadSafeDictionary<List<String>, Comparison<T>> cache = new ThreadSafeDictionary<List<String>, Comparison<T>>(new StringListComparer());

        
        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor to prevent creating instances of this static class. 
        /// </summary>
        private ComparerBuilder()
        {
        }

        #endregion

        #region Public Constants

        public const String ASC = "ASC";
        public const String ASCENDING = "ASCENDING";
        public const String DESC = "DESC";
        public const String DESCENDING = "DESCENDING";

        #endregion

        #region Public methods

        /// <summary>
        /// Builds a new comparer that compares to instances based on properties specified in a sort expression.
        /// </summary>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        /// <returns>A TypeComparer that implements both IComparer&lt;T&gt; and non-generic IComparer interface."/></returns>
        /// <exception cref="System.ArgumentNullExcpetion">If <paramref name="sortExpression"/> is null.</exception>
        /// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        public static TypeComparer<T> CreateTypeComparer(String sortExpression)
        {
            if (sortExpression == null) throw new ArgumentNullException("sortExpression");
            
            List<Comparison<T>> comparisons = GetFieldComparisons(sortExpression);

            TypeComparer<T> mFieldComparer = new TypeComparer<T>(comparisons.ToArray());
            return mFieldComparer;
        }
        
        /// <summary>
        /// Builds a new Comparison delegate that can compare instances based on properties specified in a sort expression
        /// </summary>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        /// <returns>A Comparison delegate based on the given sort criteria.</returns>
        /// <exception cref="System.ArgumentNullExcpetion">If <paramref name="sortExpression"/> is null.</exception>
        /// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        public static Comparison<T> CreateTypeComparison(String sortExpression)
        {
            if (sortExpression == null)
            {
                throw new ArgumentNullException("sortExpression");
            }
            List<Comparison<T>> comparisons = GetFieldComparisons(sortExpression);

            if (comparisons.Count == 1)
            {
                return comparisons[0];
            }
            else
            {
                TypeComparer<T> mFieldComparer = new TypeComparer<T>(comparisons.ToArray());
                return new Comparison<T>(mFieldComparer.Compare);
            }
        }

        

        /// <summary>
        /// Sorts the elements of a queryable sequence based on the given search criteria. 
        /// </summary>
        /// <param name="source">The IQueryable sequence to be sorted.</param>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name.Length")</param>
        /// <returns>A queryable sequence sorted according to the sort expression</returns>
        /// <exception cref="System.ArgumentNullException">source or sortExpression is null.</exception>
        /// <exception cref="ParserException">if sortExpression is not properly formatted or contains unrecognized property or field names..</exception>
        public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, String sortExpression)
        {
            if (source == null) throw new ArgumentNullException("source");
            
            if (sortExpression == null) throw new ArgumentNullException("sortExpression");
            
            SimpleTokenizer tokenizer = new SimpleTokenizer(sortExpression);

            IQueryable<T> result = source;

            List<String> propParts = new List<string>(4);
            do
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "o");
                
                // Create (nested) member access expression.
                Expression body = param;
                do
                {
                    String property = tokenizer.ReadIdentity();
                    if (property.Length == 0) throw new ParserException(tokenizer.Position, sortExpression, "Property or field expected.");

                    // Implicitely call Value for Nullable properties/fields.
                    if (Nullable.GetUnderlyingType(body.Type) != null)
                    {
                        body = Expression.Property(body, "Value");
                    }

                    MemberInfo member = GetMemberByName(body.Type, property);
                    if( member == null ) throw new ParserException(tokenizer.Position, sortExpression, property + " not a public property or field.");

                    body = Expression.MakeMemberAccess(body, member);
                    
                } 
                while (tokenizer.AdvanceIfSymbol('.'));

                LambdaExpression keySelectorLambda = Expression.Lambda(body, param);

                bool ascending = true;
                if( tokenizer.AdvanceIfIdent(DESC) || tokenizer.AdvanceIfIdent(DESCENDING) )
                {
                    ascending = false;
                }
                else if( tokenizer.AdvanceIfIdent(ASC) || tokenizer.AdvanceIfIdent(ASCENDING) )
                {
                    ascending = true;
                }
                String queryMethod;
                if( result == source )
                {
                    queryMethod = ascending ? "OrderBy" : "OrderByDescending";
                }
                else
                {
                    queryMethod = ascending ? "ThenBy" : "ThenByDescending";
                }
                
                result = result.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), queryMethod, 
                                                                         new Type[] { typeof(T), body.Type }, 
                                                                         result.Expression, 
                                                                         Expression.Quote(keySelectorLambda))); 
                
            }
            while( tokenizer.AdvanceIfSymbol(","));
            tokenizer.ExpectEnd();
            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// Gets a dynamically created Comparison delegate that compare instances based on a single named property 
        /// </summary>
        /// <param name="propertyName">Name of property or field to base comparison on</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparison delegate for the given property.</returns>
        /// <remarks>This class caches dynamically created Comparison delegates for best performance.</remarks>
        /// <exception cref="System.ArgumentNullException">if propertyName is null.</exception>
        /// <exception cref="Systen.ArgumentException">If propertyName is not recognized as a public property or field.</exception>
        public static Comparison<T> GetPropertyComparison(String propertyName, bool ascending)
        {
            if( propertyName == null ) throw new ArgumentNullException("propertyName");
            List<String> propParts = new List<string>(1);
            propParts.Add(propertyName);
            return GetPropertyComparison(propParts, ascending);
        }

        /// <summary>
        /// Gets a dynamically created Comparison delegate that compare instances based on a compound property expression
        /// </summary>
        /// <param name="properties">List of property names representing the property or fields to get. E.g. prop0.prop1.prop2 is represented as a list with items "prop0", "prop1" and "prop2".</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparison delegate for the given property.</returns>
        /// <exception cref="System.ArgumentNullException">if properties is null.</exception>
        /// <exception cref="Systen.ArgumentException">If any of the strings in properties is not recognized as a public property or field.</exception>
        public static Comparison<T> GetPropertyComparison(List<String> properties, bool ascending)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            Comparison<T> comparison;
                            
            if (cache.TryGetValue(properties, out comparison) == false)
            {
                comparison = CreatePropertyComparison(properties, true);
                cache[new List<String>(properties)] = comparison;
            }               
           
            if (ascending)
            {
                return comparison;
            }
            else
            {
                return (Comparison<T>)delegate(T x, T y) { return -comparison(x, y); };
            }

        }

        /// <summary>
        /// Dynamically creates a Comparison delegate that compare instances based on a single named property or field.
        /// </summary>
        /// <param name="propertyName">Name of public property or field to base comparison on.</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparision delegate for the given property.</returns>
        /// <remarks>For higher performance, use GetPropertyComparer, which caches Comparisons once created.</remarks>
        /// <exception cref="System.ArgumentNullException">if propertyName is null.</exception>
        /// <exception cref="Systen.ArgumentException">If propertyName is not recognized as a public property or field.</exception>
        public static Comparison<T> CreatePropertyComparison(String propertyName, bool ascending)
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            List<String> propParts = new List<string>(1);
            propParts.Add(propertyName);
            return CreatePropertyComparison(propParts, ascending);
        }

        /// <summary>
        /// Dynamically creates Comparison delegate that compare instances based on a compound property expression
        /// </summary>
        /// <param name="properties">List of property names representing the property or field expression to get. E.g. prop0.prop1.prop2 is represented as a list with items "prop0", "prop1" and "prop2".</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparison delegate for the given property.</returns>
        /// <remarks>For higher performance in repetitive calls use GetPropertyComparison, which caches results, instead.</remarks>
        /// <exception cref="System.ArgumentNullException">if properties is null.</exception>
        /// <exception cref="Systen.ArgumentException">If any of the strings in properties is not recognized as a public property or field.</exception>
        public static Comparison<T> CreatePropertyComparison(List<String> propertyExpression, bool ascending)
        {
            Expression<Comparison<T>> compareExpression = CreatePropertyComparisonExpression(propertyExpression, ascending);
            return compareExpression.Compile();
        }

        /// <summary>
        /// Dynamically creates a lambda expression representing a Comparison delegate that compare instances based on a single named property expression
        /// </summary>
        /// <param name="properties">List of property names representing the property expression to get. E.g. prop0.prop1.prop2 is represented as a list with items "prop0", "prop1" and "prop2".</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparison delegate expression for the given property.</returns>
        /// <exception cref="System.ArgumentNullException">if properties is null.</exception>
        /// <exception cref="Systen.ArgumentException">If any of the strings in properties is not recognized as a public property or field.</exception>
        public static Expression<Comparison<T>> CreatePropertyComparisonExpression(List<String> properties, bool ascending)
        {
            if (properties == null) throw new ArgumentNullException("properties");

            Type t = typeof(T);

            ParameterExpression param1 = ParameterExpression.Parameter(t, "o1");
            ParameterExpression param2 = ParameterExpression.Parameter(t, "o2");
            Expression body = null;

            Expression getProp1 = BuildNullSafeGetter(properties, 0, param1);
            Expression getProp2 = BuildNullSafeGetter(properties, 0, param2);

            Type propType = getProp1.Type;
            if (propType == typeof(String))
            {
                // Call String.Compare(o1,o2,StringComparison.CurrentCultureIgnoreCase)
                body = Expression.Call(stringCompareMethod, getProp1, getProp2, Expression.Constant(StringComparison.CurrentCultureIgnoreCase, typeof(StringComparison)));
            }
            else
            {
                Type nullableUnderlyingType = Nullable.GetUnderlyingType(propType);
                if( nullableUnderlyingType != null )
                {
                    propType = nullableUnderlyingType;
                    // In case of a Nullable<enum-type> cast to underlying type (see below) and use generic IComparable<T> to avoid boxing.
                    if (propType.IsEnum)
                    {
                        propType = Enum.GetUnderlyingType(propType);
                    }
                }
                // Try typed CompareTo (defined in IComparable<T> generic interface type) first.
                MethodInfo compareToMethod = null;
                Type genericIComparableType = typeof(IComparable<>).MakeGenericType(propType);
                if (genericIComparableType.IsAssignableFrom(propType))
                {
                    compareToMethod = genericIComparableType.GetMethod("CompareTo");
                }
                else
                {
                    // Then try non-generic IComparable interface.
                    if (typeof(IComparable).IsAssignableFrom(propType))
                    {
                        compareToMethod = nonGenericCompareToMethod;
                    }
                    else
                    {
                        if (propType.IsValueType)
                        {
                            // If no comparison method found, all values are considered equal
                            body = Expression.Constant(0, typeof(int));
                        }
                        else
                        {
                            // Call ComparerBuilderBase.Compare(object,object) 
                            body = Expression.Call(generalCompareMethod, 
                                                   Expression.Convert(getProp1, typeof(Object)), 
                                                   Expression.Convert(getProp2, typeof(Object)));

                        }
                     }
                }
                if (compareToMethod != null)
                {
                    if (nullableUnderlyingType != null)  // If a Nullable type.
                    {
                        body = Expression.Call(typeof(Nullable), "Compare", new Type[] { nullableUnderlyingType }, getProp1, getProp2);
                        
                    }
                    else if (propType.IsValueType)
                    {
                        // No null-checks is neccessary, simply call CompareTo method, boxing argument if calling non-generic IComparable.CompareTo(object) 
                        body = Expression.Call(getProp1, compareToMethod, compareToMethod == nonGenericCompareToMethod ? (Expression)Expression.Convert(getProp2, typeof(Object)) : getProp2);
                    }
                    else
                    {
                        // Reference type: Check for null values before calling CompareTo method.
                        ParameterExpression p1 = Expression.Parameter(getProp1.Type, "p1");
                        ParameterExpression p2 = Expression.Parameter(getProp1.Type, "p2");
                        body = Expression.Invoke(Expression.Lambda(Expression.Condition(Expression.NotEqual(p1, Expression.Constant(null)),
                                                                                     Expression.Condition(Expression.NotEqual(p2, Expression.Constant(null)),
                                                                                     Expression.Call(p1, compareToMethod, compareToMethod == nonGenericCompareToMethod ? (Expression)Expression.Convert(p2, typeof(Object)) : p2),
                                                                                         Expression.Constant(1, typeof(int))
                                                                                     ),
                                                                                     Expression.Condition(Expression.NotEqual(p2, Expression.Constant(null)),
                                                                                         Expression.Constant(-1, typeof(int)),
                                                                                         Expression.Constant(0, typeof(int))
                                                                                     )
                                                                                  ), p1, p2), getProp1, getProp2);
                    }
                }
            }
            if (ascending == false)
            {
                body = Expression.Negate(body);
            }
            return Expression.Lambda<Comparison<T>>(body, param1, param2);
        }

        /// <summary>
        /// Dynamically creates a Comparison delegate that compare instances based on a single named property using System.Reflection.Emit.
        /// </summary>
        /// <param name="propName">Name of property to base comparison on</param>
        /// <param name="ascending">true to search in ascending order, false to sort in descending order</param>
        /// <returns>A Comparison delegate for the given property.</returns>
        /// <remarks>This cannot be used for value types and does not support fields.</remarks>
        /// <exception cref="System.InvalidOperationException">If T is a value type.</exception>
        /// <exception cref="System.ArgumentException">If propName is not a name of a public readable property in class T</exception>
        /// <exception cref="System.ArgumentNullException">If propName is null.</exception>
        public static Comparison<T> CreatePropertyComparisonThroughEmit(String propName, bool ascending)
        {
            if (propName == null) throw new ArgumentNullException("propName");

            Type t = typeof(T);
            if (t.IsValueType)
            {
                throw new InvalidOperationException("Cannot create property comparer using CreatePropertyComparerThroughEmit for value types.");
            }
            PropertyInfo property = t.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

            if (property == null)
            {
                throw new ArgumentException("Public property named '" + propName + "' not found in type "+t.Name);
            }
            Type propertyType = property.PropertyType;
            MethodInfo propGetMethod = property.GetGetMethod();
            if (propGetMethod == null )
            {
                throw new ArgumentException("Public get method not found for property '" + propName + "' not found in type "+t.Name);
            }
            DynamicMethod dynMethod = new DynamicMethod("Compare" + propName, typeof(int), new Type[] { t, t }, t);
            ILGenerator ilgen = dynMethod.GetILGenerator();

            if (propertyType == typeof(String))
            {
                // Call String.Compare(o1.<prop>,s2.<prop>,StringComparison.CurrentCultureIgnoreCase)
                ilgen.Emit(OpCodes.Ldarg_0);
                ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                ilgen.Emit(OpCodes.Ldarg_1);
                ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                ilgen.Emit(OpCodes.Ldc_I4_1);
                ilgen.EmitCall(OpCodes.Call, stringCompareMethod, null);
                
            }
            else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Call Nullable<T>.Compare
                ilgen.Emit(OpCodes.Ldarg_0);
                ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                ilgen.Emit(OpCodes.Ldarg_1);
                ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                MethodInfo compareMethod = nullableGenericCompareMethod.MakeGenericMethod(Nullable.GetUnderlyingType(propertyType));
                ilgen.EmitCall(OpCodes.Call, compareMethod, null);
                
            }
            else
            {
                // Try typed CompareTo (defined in IComparable<T> generic interface type) first.
                Type genericIComparableType = typeof(IComparable<>).MakeGenericType(propertyType);
                MethodInfo genericIComparableCompareToMethod = genericIComparableType.GetMethod("CompareTo");
                MethodInfo compareToMethod = GetMethodImplementation(propertyType, genericIComparableCompareToMethod);
                bool nonGenericCompare = false;
                if (compareToMethod == null)
                {
                    // Then, try non-generic CompareTo (defined in IComparable interface type)

                    compareToMethod = GetMethodImplementation(propertyType, nonGenericCompareToMethod);
                    if (compareToMethod == null)
                    {
                        throw new ArgumentException("Type of property '" + propName + "' does not support comparison");
                    }
                    nonGenericCompare = true;
                }

                if (propertyType.IsValueType == false)
                {
                    // Call o1.CompareTo(o2);
                    Label p1NotNullLabel = ilgen.DefineLabel();
                    Label p2NotNullLabel = ilgen.DefineLabel();

                    ilgen.Emit(OpCodes.Ldarg_0);
                    ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                    ilgen.Emit(OpCodes.Stloc_0);
                    ilgen.Emit(OpCodes.Ldarg_1);
                    ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                    ilgen.Emit(OpCodes.Stloc_1);
                    ilgen.Emit(OpCodes.Ldloc_0);
                    ilgen.Emit(OpCodes.Brtrue_S, p1NotNullLabel);
                    ilgen.Emit(OpCodes.Ldloc_1);
                    ilgen.Emit(OpCodes.Brtrue_S, p2NotNullLabel);
                    ilgen.Emit(OpCodes.Ldc_I4_0);
                    ilgen.Emit(OpCodes.Ret);
                    ilgen.MarkLabel(p2NotNullLabel);
                    if (ascending)
                    {
                        ilgen.Emit(OpCodes.Ldc_I4_M1);
                    }
                    else
                    {
                        ilgen.Emit(OpCodes.Ldc_I4_1);
                    }
                    ilgen.Emit(OpCodes.Ret);
                    ilgen.MarkLabel(p1NotNullLabel);
                    ilgen.Emit(OpCodes.Ldloc_0);
                    ilgen.Emit(OpCodes.Ldloc_1);
                    ilgen.EmitCall(OpCodes.Call, compareToMethod, null);
                    
                }
                else
                {
                    // Call o1.<prop>.CompareTo(o2.<prop>);
                    ilgen.DeclareLocal(propertyType);

                    ilgen.Emit(OpCodes.Ldarg_0);
                    ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                    ilgen.Emit(OpCodes.Stloc_0);
                    ilgen.Emit(OpCodes.Ldloca_S, (byte)0);
                    ilgen.Emit(OpCodes.Ldarg_1);
                    ilgen.EmitCall(OpCodes.Callvirt, propGetMethod, null);
                    if (nonGenericCompare)
                    {
                        ilgen.Emit(OpCodes.Box);
                    }
                    ilgen.EmitCall(OpCodes.Call, compareToMethod, null);
                    
                }
            }
            if (ascending == false)
            {
                ilgen.Emit(OpCodes.Neg);
            }
            ilgen.Emit(OpCodes.Ret);
            return (Comparison<T>)dynMethod.CreateDelegate(typeof(Comparison<T>));


        }


        #endregion

        #region Private helpers

        /// <summary>
        /// Private helper to parse a sort expression string and create Comparison delegates based on each property.
        /// </summary>
        /// <param name="sortExpression">Sort expression to parse.</param>
        /// <returns>List of Comparison delegates, one for each property.</returns>
        private static List<Comparison<T>> GetFieldComparisons(String sortExpression)
        {
            SimpleTokenizer parser = new SimpleTokenizer(sortExpression);
            List<Comparison<T>> comparisons = new List<Comparison<T>>(4);
            List<String> propertyParts = new List<string>(4);
            Boolean moreProperties;
            do
            {
                do
                {
                    String property = parser.ReadIdentity();
                    if (property.Length == 0) throw new ParserException(parser.Position, sortExpression, "Field or property expected");

                    propertyParts.Add(property);
                }
                while (parser.AdvanceIfSymbol('.'));
                
                moreProperties = parser.AdvanceIfSymbol(',');
                
                bool ascending = true;
                if (moreProperties == false)
                {
                    if (parser.AdvanceIfIdent(DESC) || parser.AdvanceIfIdent(DESCENDING))
                    {
                        ascending = false;
                        moreProperties = parser.AdvanceIfSymbol(',');
                    }
                    else if (parser.AdvanceIfIdent(ASC) || parser.AdvanceIfIdent(ASCENDING))
                    {
                        moreProperties = parser.AdvanceIfSymbol(',');
                    }
                    
                }

                try
                {
                    comparisons.Add(GetPropertyComparison(propertyParts, ascending));
                }
                catch (ArgumentException ex)
                {
                    throw new ParserException(parser.Position, parser.Expression, ex.Message);
                }
                propertyParts.Clear();

            }
            while (moreProperties);
            parser.ExpectEnd();
            return comparisons;
        }

        #endregion

    }

    
}
