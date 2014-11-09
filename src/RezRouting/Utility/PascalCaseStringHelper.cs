using System.Text.RegularExpressions;

namespace RezRouting.Utility
{
    public class PascalCaseStringHelper
    {
        private static readonly Regex SeparatorRegex;

        static PascalCaseStringHelper()
        {
            const string pattern = @"
                (?<!^) # Not start
                (
                    # Digit, not preceded by another digit
                    (?<!\d)\d 
                    |
                    # Upper-case letter, must be followed by lower-case letter if
                    # preceded by another upper-case letter, e.g. 'G' in HTMLGuide
                    (?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z])
                )";
            SeparatorRegex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
        }

        public static string SeparateWords(string value, string separator)
        {
            return SeparatorRegex.Replace(value, separator + "$1");
        }
    }
}