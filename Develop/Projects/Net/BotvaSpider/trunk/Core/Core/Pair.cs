using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    public sealed class Pair<T1,T2>
    {
        // Fields
        public T1 First;
        public T2 Second;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;T1, T2&gt;"/> class.
        /// </summary>
        public Pair()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Pair(T1 x, T2 y)
        {
            First = x;
            Second = y;
        }
    }

}
