using System.Text.RegularExpressions;

namespace RezRouting.Utility
{
    /// <summary>
    /// Word formatting string utility methods
    /// </summary>
    public class WordFormatter
    {
        private static readonly Regex SeparatorRegex;

        static WordFormatter()
        {
            const string endOfAcronym = "[A-Z]+(?=[A-Z][a-z0-9])";
            const string endOfWord = "[a-z](?=[A-Z0-9])";
            const string endOfNumber = "[0-9](?=[A-Za-z])";
            string pattern = string.Concat("(", endOfAcronym, "|", endOfWord, "|", endOfNumber, ")");
            SeparatorRegex = new Regex(pattern);
        }

        /// <summary>
        /// Adds separators at boundaries between words, numbers and acronyms
        /// within a Pascal-case or camel-case string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ExpandCamelHumps(string value, string separator)
        {
            return SeparatorRegex.Replace(value, "$1" + separator);
        }
    }
}