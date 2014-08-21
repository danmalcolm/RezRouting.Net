using System.Linq;

namespace RezRouting2.Utility
{
    internal static class ArrayExtensions
    {
        internal static T[] Append<T>(this T[] values, T value)
        {
            return values.Concat(new[] { value }).ToArray();
        }
    }
}