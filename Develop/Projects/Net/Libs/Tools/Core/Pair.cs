using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Core
{
    /// <summary>
    /// Pair
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    public class Pair<T1, T2>
    {
        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>The first.</value>
        public T1 First { get; set; }
        /// <summary>
        /// Gets or sets the second.
        /// </summary>
        /// <value>The second.</value>
        public T2 Second { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;T1, T2&gt;"/> class.
        /// </summary>
        public Pair()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }
}
