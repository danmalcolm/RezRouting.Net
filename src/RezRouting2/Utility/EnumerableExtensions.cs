using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2.Utility
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
}