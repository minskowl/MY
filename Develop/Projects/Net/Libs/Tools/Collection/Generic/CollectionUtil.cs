using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Core;

namespace Savchin.Collection.Generic
{
    /// <summary>
    /// CollectionUtil class
    /// </summary>
    public static class CollectionUtil
    {
        /// <summary>
        /// Finds the specified enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static List<T> Find<T>(this IEnumerable enumerable)
        {
            List<T> result = new List<T>();
            foreach (object o in enumerable)
            {
                if (o is T)
                    result.Add((T)o);
            }
            return result;
        }

        /// <summary>
        /// Converts the specified enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static List<T> Convert<T>(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return null;
            var type = typeof(T);

            return (from object o in enumerable select (T) type.Convert(o)).ToList();
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static List<T> ConvertToEnum<T>(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return null;
            var result = new List<T>();
            Type type = typeof(T);
            foreach (object o in enumerable)
            {
                result.Add((T)Enum.ToObject(type, o));
            }
            return result;
        }
    }
}