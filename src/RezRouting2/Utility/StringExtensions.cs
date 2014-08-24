using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2.Utility
{
    internal static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return string.Equals(value, other, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool ContainsIgnoreCase(this IEnumerable<string> sequence, string value)
        {
            return sequence.Contains(value, StringComparer.InvariantCultureIgnoreCase);
        }

        public static string ToCamelCase(this string value)
        {
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}