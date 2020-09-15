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

        public static ParameterMode ToParameterMode(this int parameterMode)
        {
            return parameterMode switch
            {
                0 => ParameterMode.Position,
                1 => ParameterMode.Immediate,
                _ => throw new Exception("Failed to convert int to ParameterMode"),
            };
        }

        public static bool HasDuplicate<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source == null)
                throw new ArgumentException(nameof(source));

            HashSet<T> set = new HashSet<T>(comparer);
            foreach (var item in source)
                if (!set.Add(item))
                    return true;

            return false;
        }
    }
}