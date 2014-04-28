using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting.Utility
{
    internal static class EnumerableExtensions
    {
        internal static IList<T> ToReadOnlyList<T>(this IEnumerable<T> sequence)
        {
            return sequence.ToList().AsReadOnly();
        }

        internal static void Each<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }
    }

    internal static class ArrayExtensions
    {
        internal static T[] Append<T>(this T[] values, T value)
        {
            return values.Concat(new[] {value}).ToArray();
        }
    }
}