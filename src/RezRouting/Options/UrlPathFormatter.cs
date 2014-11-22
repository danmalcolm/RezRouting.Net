using System.Text.RegularExpressions;
using RezRouting.Utility;

namespace RezRouting.Options
{
    /// <summary>
    /// Formats path segments within route URLs
    /// </summary>
    public class UrlPathFormatter
    {
        private readonly UrlPathSettings settings;

        /// <summary>
        /// Creates a UrlPathFormatter
        /// </summary>
        /// <param name="settings"></param>
        public UrlPathFormatter(UrlPathSettings settings = null)
        {
            this.settings = settings ?? new UrlPathSettings(CaseStyle.Lower, "");
        }

        /// <summary>
        /// Formats a directory name based on the supplied name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string FormatDirectoryName(string name)
        {
            string result = name;

            result = PathSegmentCleaner.Clean(result);

            if (settings.WordSeparator != "")
            {
                result = IntercappedStringHelper.SeparateWords(result, settings.WordSeparator);
            }
            switch (settings.CaseStyle)
            {
                case CaseStyle.Lower:
                    result = result.ToLowerInvariant();
                    break;
                case CaseStyle.Upper:
                    result = result.ToUpperInvariant();
                    break;
            }
            return result;
        }
   }
}