﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Savchin.Core.Monads
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class MaybeIEnumerable
    {
   
      

        /// <summary>
        /// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do (with zero-based index)</param>
        /// <returns>Source collection</returns>
        public static IEnumerable<TSource> Do<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            if (source != null)
            {
                foreach (var element in source.Select((s, i) => new { Source = s, Index = i }))
                {
                    action(element.Source, element.Index);
                }
            }

            return source;
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> collection elements if its not null
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <typeparam name="TResult">Type of result collection elements</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <returns>Converted collection</returns>
        public static IEnumerable<TResult> With<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> action)
        {
            if (source != null)
            {
                return source.Select(action);
            }
            return null;
        }

        /// <summary>
        /// Returns empty collection if null, or source collection if not null.
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns></returns>
        public static IEnumerable<TSource> OrEmptyIfNull<TSource>(this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }
    }
}