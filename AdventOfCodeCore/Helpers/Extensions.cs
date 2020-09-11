using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public static class Helpers
    {

        public static string Tail(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source[^tail_length..];
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}