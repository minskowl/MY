using System;
using System.Collections.Generic;
using System.Linq;

namespace Savchin.Collection.Generic
{
    /// <summary>
    /// Enumerable
    /// </summary>
    public static class EnumerableEx
    {
        /// <summary>
        /// Determines whether the specified ar is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <returns>
        ///   <c>true</c> if the specified ar is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> ar)
        {
            return ar == null || !ar.Any();
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified ar].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ar">The ar.</param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> ar)
        {
            return ar != null && ar.Any();
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable)
            {
                action(e);
            }
        }

        //<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
        ///<summary>Finds the index of the first occurence of an item in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }

        public static IEnumerable<T[]> Split<T>(this IEnumerable<T> items, int cnt)
        {
            if (items == null) yield break;

            int i = 0;
            var length = items.Count();
            while (i < length)
            {
                yield return items.Skip(i).Take(cnt).ToArray();
                i = i + cnt;
            }
        }
    }
}
