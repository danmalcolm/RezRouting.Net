using System.Text.RegularExpressions;
using RezRouting2.Utility;

namespace RezRouting2.Options
{
    public class UrlPathFormatter
    {
        private readonly UrlPathSettings settings;

        public UrlPathFormatter(UrlPathSettings settings = null)
        {
            this.settings = settings ?? new UrlPathSettings(CaseStyle.Lower, "");
        }

        public string FormatDirectoryName(string name)
        {
            string result = name;

            result = PathSegmentCleaner.Clean(result);

            if (settings.WordSeparator != "")
            {
                result = Regex.Replace(result, "([a-z])(?=[A-Z])", "$1" + settings.WordSeparator);
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