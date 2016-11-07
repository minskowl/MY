using System.Collections;

namespace Savchin.Collection
{
    /// <summary>
    /// Enumerable
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// Counts the specified enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static int Count(this IEnumerable enumerable)
        {
            var count = 0;
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }
            return count;
        }

  
    }
}
