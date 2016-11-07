using System;
using System.Collections;
using System.Linq;
using Savchin.Collection.Sorting;


namespace Savchin.Collection
{
    /// <summary>
    /// Class containing extension methods for comparison and sorting using textual sort expressions.
    /// </summary>
    public static class ComparerExtensions
    {
        ///// <summary>
        ///// Sort the elements of a list according to the specified sort expression.
        ///// </summary>
        ///// <typeparam name="T">List item type</typeparam>
        ///// <param name="list">List to be sorted</param>
        ///// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        ///// <exception cref="System.ArgumentNullException"><paramref name="list"/> is null or <paramref name="sortExpression"/> is null</exception>
        ///// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        //public static void Sort<T>(this List<T> list, String sortExpression)
        //{
        //    Comparison<T> comparison = ComparerBuilder.CreateTypeComparison(sortExpression);
        //    list.Sort(comparison);
        //}

        ///// <summary>
        ///// Sorts the elements in a range of elements in a list using the specified sort expression 
        ///// </summary>
        ///// <typeparam name="T">List item type</typeparam>
        ///// <param name="list">List to be sorted</param>
        ///// <param name="index">Index of first item to be sorted</param>
        ///// <param name="count">Number of items to be sorted.</param>
        ///// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        ///// <exception cref="System.ArgumentNullException"><paramref name="list"/> is null or <paramref name="sortExpression"/> is null</exception>
        ///// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        ///// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> not a valid list index or <paramref name="count"/> out of range.</exception>
        //public static void Sort<T>(this List<T> list, int index, int count, String sortExpression)
        //{
        //    IComparer<T> comparer = ComparerBuilder<T>.CreateTypeComparer(sortExpression);
        //    list.Sort(index, count, comparer);
        //}

        ///// <summary>
        ///// Sort the elements in an array according specified sort expression.
        ///// </summary>
        ///// <typeparam name="T">Type of array items</typeparam>
        ///// <param name="array">Array to be sorted</param>
        ///// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        ///// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null or <paramref name="sortExpression"/> is null</exception>
        ///// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>

        //public static void Sort<T>(this T[] array, String sortExpression)
        //{
        //    if (array == null)
        //    {
        //        throw new ArgumentNullException("sortExpression");
        //    }
        //    if (sortExpression == null)
        //    {
        //        throw new ArgumentNullException("sortExpression");
        //    }
        //    Comparison<T> comparison = ComparerBuilder<T>.CreateTypeComparison(sortExpression);
        //    Array.Sort(array, comparison);

        //}

        ///// <summary>
        ///// Sorts the elements in a range of elements in an array using the specified sort expression.
        ///// </summary>
        ///// <typeparam name="T">Type of elements in array</typeparam>
        ///// <param name="array">Array to be sorted.</param>
        ///// <param name="index">Index of first element to be sorted</param>
        ///// <param name="length">Number of elements to be sorted.</param>
        ///// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        ///// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null or <paramref name="sortExpression"/> is null</exception>
        ///// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        ///// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="length" /> out of range.</exception>
        //public static void Sort<T>(this T[] array, int index, int length, String sortExpression)
        //{
        //    if (array == null)
        //    {
        //        throw new ArgumentNullException("array");
        //    }
        //    if (sortExpression == null)
        //    {
        //        throw new ArgumentNullException("sortExpression");
        //    }
        //    IComparer<T> comparer = ComparerBuilder<T>.CreateTypeComparer(sortExpression);
        //    Array.Sort(array, index, length, comparer);
        //}

        /// <summary>
        /// Tests whether the elements in the given sequence is sorted according to the specified fields.
        /// </summary>
        /// <typeparam name="T">Type of items in sequence</typeparam>
        /// <param name="source">Sequence to test</param>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        /// <returns>True if all elements in sequence is sorted according to the specified fields (or if sequence is empty), otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null or <paramref name="sortExpression"/> is null</exception>
        /// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        public static bool IsOrderedBy(this IEnumerable source, String sortExpression)
        {
            if (source == null) throw new ArgumentNullException("source");

            if (sortExpression == null) throw new ArgumentNullException("sortExpression");

            var enumerator = source.GetEnumerator();

            if (enumerator.MoveNext() == false)
            {
                return true;
            }
            var lastItem = enumerator.Current;
            Comparison comparison = ComparerBuilder.CreateTypeComparison(lastItem.GetType(), sortExpression);
            while (enumerator.MoveNext())
            {
                var thisItem = enumerator.Current;
                if (comparison(lastItem, thisItem) > 0) return false;
            }
            return true;
        }

        ///// <summary>
        ///// Checks that the given sort expression is valid for the given sequence.
        ///// </summary>
        ///// <remarks>
        ///// If you do not have a reference to an instance of the expression you can directly call 
        ///// ComparerBuilder&lt;T&gt;.CreateTypeComparison(sortExpression) to validate a sort expression.
        ///// </remarks>
        ///// <typeparam name="T">Type of items in sequence</typeparam>
        ///// <param name="source">Sequence to be tested</param>
        ///// <param name="sortExpression">Sort expression to be verified.</param>
        ///// <exception cref="System.ArgumentNullException"><paramref name="sortExpression"/> is null</exception>
        ///// <exception cref="ParserException">If sort expression is not valid.</exception>
        //public static void ValidateSortExpression<T>(this IEnumerable<T> source, String sortExpression)
        //{
        //    ComparerBuilder<T>.CreateTypeComparison(sortExpression);
        //}

        /// <summary>
        /// Sort the elements of a sequence according to the specified textual sort expression.
        /// </summary>
        /// <param name="source">Sequence to be sorted</param>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null or <paramref name="sortExpression"/> is null</exception>
        /// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        public static IEnumerable OrderBy(this IEnumerable source, String sortExpression)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sortExpression == null)
            {
                throw new ArgumentNullException("sortExpression");
            }
            //Type type = null;

            //var ar = new ArrayList();
            //foreach (var obj in source)
            //{
            //    if (type == null) type = obj.GetType();
            //    ar.Add(obj);
            //}
            //var comparer = ComparerBuilder.CreateTypeComparer(type, sortExpression);
            //if (ar.Count > 0) ar.Sort(comparer);
            // return ar;
            return source.AsQueryable().OrderBy(sortExpression);
        }

        /// <summary>
        /// Sort the element of a queryable sequence by a given sort expression.
        /// </summary>
        /// <typeparam name="T">Type of items in sequence.</typeparam>
        /// <param name="source">Sequence to be sorted.</param>
        /// <param name="sortExpression">A SQL-like sort expression with comma separated property names (and optional direction specifiers) (e.g. "Age DESC, Name")</param>
        /// <returns>A queryable object that can enumerate the elements in the input sequence ordered according to the given sort expression.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="source"/> is null or <paramref name="sortExpression"/> is null</exception>
        /// <exception cref="ParserException">If <paramref name="sortExpression"/> is an invalid sort expression.</exception>
        public static IOrderedQueryable OrderBy(this IQueryable source, String sortExpression)
        {
            return ComparerBuilder.OrderBy(source.ElementType, source, sortExpression);

        }




    }
}


