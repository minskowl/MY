using System;
using System.Collections.Generic;

namespace Savchin.Collection.Generic.Sorting
{
    /// <summary>
    /// Helper class that compares object based on a number of field comparisons given as Comparison delegates.
    /// </summary>
    public sealed class TypeComparer<T> : IComparer<T>, global::System.Collections.IComparer
    {
        private readonly Comparison<T>[] comparisons;

        public TypeComparer(Comparison<T>[] comparisons)
        {
            this.comparisons = comparisons;
        }

        #region IComparer<T> Members

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        public int Compare(T x, T y)
        {

            for (int i = 0; i < comparisons.Length; i++)
            {
                int res = comparisons[i](x, y);
                if (res != 0)
                {
                    return res;
                }
            }
            return 0;
        }

        #endregion

        #region IComparer Members

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value
        /// Condition
        /// Less than zero
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// Zero
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// Greater than zero
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// Neither <paramref name="x"/> nor <paramref name="y"/> implements the <see cref="T:System.IComparable"/> interface.
        /// -or-
        /// <paramref name="x"/> and <paramref name="y"/> are of different types and neither one can handle comparisons with the other.
        /// </exception>
        public int Compare(object x, object y)
        {
            return Compare((T)x, (T)y);
        }

        #endregion
    }
}