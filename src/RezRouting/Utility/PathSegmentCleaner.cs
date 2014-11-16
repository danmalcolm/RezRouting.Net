using System.Text.RegularExpressions;

namespace RezRouting.Utility
{
    /// <summary>
    /// Validates and cleans path segments used within route URLs
    /// </summary>
    internal class PathSegmentCleaner
    {
        private static readonly Regex InvalidCharactersRegex = new Regex("[^a-z0-9-_]", RegexOptions.IgnoreCase);

        public static bool IsValid(string value)
        {
            return !InvalidCharactersRegex.IsMatch(value);
        }

        public static string Clean(string segment)
        {
            return InvalidCharactersRegex.Replace(segment, "");
        }
    }
}