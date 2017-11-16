using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1708:IdentifiersShouldDifferByMoreThanCase")]
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ins the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] array)
        {
            return array.Contains(value);
        }


        /// <summary>
        /// Concats the specified second.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, TSource second)
        {
            if (first != null)
                foreach (var element in first) yield return element;
            if (second != null)
                yield return second;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, TSource second, IEnumerable<TSource> third)
        {
            if (first != null)
                foreach (var element in first) yield return element;
            if (second != null)
                yield return second;
            if (third != null)
                foreach (var element in third) yield return element;
        }
        /// <summary>
        /// Concats the specified second.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Concat<TSource>(this TSource first, IEnumerable<TSource> second)
        {
            if (first != null)
                yield return first;

            if (second != null)
                foreach (var element in second) yield return element;

        }
        /// <summary>
        /// Concats the specified arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays">The arrays.</param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] arrays)
        {
            return arrays.Where(array => array != null).SelectMany(array => array);
        }
        public static IEnumerable<T> Concat<T>(params T[][] arrays)
        {
            return arrays.Where(array => array != null).SelectMany(array => array);
        }
        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="arrays">The arrays.</param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, params IEnumerable<T>[] arrays)
        {
            return Enumerable.Concat(items, Concat(arrays));
        }
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] args)
        {
            foreach (T element in first) yield return element;
            foreach (T element in args) yield return element;
        }

        public static IEnumerable<T> ConcatNotNull<T>(this IEnumerable<T> first, params T[] args)
        {
            foreach (T element in first) yield return element;
            foreach (T element in args)
                if (element != null)
                    yield return element;
        }
        /// <summary>
        /// Equalses the specified array1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array1">The array1.</param>
        /// <param name="array2">The array2.</param>
        /// <returns></returns>
        public static bool ArrayEquals<T>(this T[] array1, T[] array2)
        {
            if (ReferenceEquals(array1, array2))
                return true;

            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < array1.Length; i++)
            {
                if (!comparer.Equals(array1[i], array2[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        public static void AddRange(this IList list, IEnumerable items)
        {
            if (items != null && list != null)
                foreach (var item in items)
                    list.Add(item);
        }
        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        public static void RemoveRange(this IList list, IEnumerable items)
        {
            if (items != null && list != null)
                foreach (var item in items)
                    list.Remove(item);
        }

        /// <summary>
        /// Determines whether the specified ar is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The ar.</param>
        /// <returns>
        ///   <c>true</c> if the specified ar is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Determines whether the specified ar is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The items.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified ar].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        /// <summary>
        /// Excepts the specified except.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="filter">The except.</param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] filter)
        {
            return enumerable.Except((IEnumerable<T>)filter);
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null && action != null)
                foreach (var e in enumerable)
                    action(e);
            return enumerable;
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null && action != null)
                foreach (var e in enumerable)
                    action(e);
            return enumerable;
        }


        /// <summary>
        /// Fills the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="data">The data.</param>
        /// <param name="count">The count.</param>
        public static void Fill<T>(this IList<T> list, T data, int count)
        {
            if (list != null)
                for (var i = 0; i < count; i++)
                    list.Add(data);
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
        public static int IndexOf<T>(this IEnumerable<T> items, T item) { return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i)); }
    }
}
