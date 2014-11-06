using System.Text;

namespace RezRouting.Utility
{
    /// <summary>
    /// Word formatting string utility methods
    /// </summary>
    public class WordFormatter
    {
        /// <summary>
        /// Adds separators at boundaries between words, numbers and acronyms
        /// within a Pascal-case or camel-case string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ExpandCamelHumps(string value, string separator)
        {
            var result = new StringBuilder();
            for (int index = 0; index < value.Length; index++)
            {
                if (NeedsSeparator(value,index)) 
                    result.Append(separator);
                result.Append(value[index]);
            }
            return result.ToString();
        }

        private static bool NeedsSeparator(string value, int index)
        {
            if (index == 0) return false;
            return StartOfNumber(value, index) || StartOfWord(value, index);
        }

        private static bool StartOfNumber(string value, int index)
        {
            return char.IsDigit(value, index) 
                && !char.IsDigit(value, index - 1);
        }

        private static bool StartOfWord(string value, int index)
        {
            if (char.IsUpper(value, index))
            {
                bool withinAcroynym = char.IsUpper(value, index - 1)
                                      && (index == value.Length - 1 || !char.IsLower(value, index + 1));
                return !withinAcroynym;
            }
            else
            {
                return false;
            }
        }
    }
}