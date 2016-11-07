using System;
using System.Collections;
using System.Collections.Generic;

namespace Savchin.Collection.Sorting
{
    public delegate int Comparison(object x, object y);

    /// <summary>
    /// Helper class that compares object based on a number of field comparisons given as Comparison delegates.
    /// </summary>
    public sealed class TypeComparer : IComparer
    {
        private readonly Comparison[] comparisons;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeComparer"/> class.
        /// </summary>
        /// <param name="comparisons">The comparisons.</param>
        public TypeComparer(Comparison[] comparisons)
        {
            this.comparisons = comparisons;
        }

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
        public int Compare(object x, object y)
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


    }
}