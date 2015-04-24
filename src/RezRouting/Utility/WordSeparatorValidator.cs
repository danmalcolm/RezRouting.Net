using System.Text.RegularExpressions;

namespace RezRouting.Utility
{
    internal class WordSeparatorValidator
    {
        private static readonly Regex InvalidCharactersRegex = new Regex("[^a-z0-9-_]", RegexOptions.IgnoreCase);

        public static bool IsValid(string value)
        {
            return !InvalidCharactersRegex.IsMatch(value);
        }
    }
}