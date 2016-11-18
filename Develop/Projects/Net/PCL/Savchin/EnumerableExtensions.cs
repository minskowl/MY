using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savchin.Core
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null || action == null) return;
            foreach (var e in enumerable)
                action(e);
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

        /// <summary>
        /// Clones the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static IEnumerable<T> Clone<T>(T value, int count)
        {
            var res = new T[count];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = value;
            }
            return res;
        }
    }
}
