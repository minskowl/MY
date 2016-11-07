using System.Collections.Generic;

namespace Savchin.Comparer
{

    public class Filter
    {
        private List<KeyMatcher> primitives = new List<KeyMatcher>();

        /// <summary>
        /// Gets the primitives keys.
        /// </summary>
        /// <value>The primitives keys.</value>
        public List<KeyMatcher> PrimitivesKeys
        {
            get { return primitives; }
        }

        public bool IsMatch(CompareResultBase result)
        {
            return true;
        }
    }
}
