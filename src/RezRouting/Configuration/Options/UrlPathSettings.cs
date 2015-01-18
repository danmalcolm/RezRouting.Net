using System;
using RezRouting.Utility;

namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Controls the formatting of a resource's path in route URLs
    /// </summary>
    public class UrlPathSettings
    {
        /// <summary>
        /// Creates a new UrlPathSettings instance
        /// </summary>
        /// <param name="caseStyle">The CaseStyle to be used</param>
        /// <param name="wordSeparator">A string added between "camel humps" in the resource name, e.g. "PurchaseOrders" => "purchase-orders". 
        /// Note that only numbers, letters, "-" and "_" may be used</param>
        public UrlPathSettings(CaseStyle caseStyle = CaseStyle.Lower, string wordSeparator = null)
        {
            wordSeparator = wordSeparator ?? "";
            if (!PathSegmentCleaner.IsValid(wordSeparator))
            {
                throw new ArgumentException("Characters within the separator are not valid for use within a URL path. Only numbers, letters, - and _ may be used", "wordSeparator");
            }

            CaseStyle = caseStyle;
            WordSeparator = wordSeparator;
        }

        /// <summary>
        /// The CaseStyle to be used
        /// </summary>
        public CaseStyle CaseStyle { get; private set; }

        /// <summary>
        /// A string added between "camel humps" in the resource name, e.g. "PurchaseOrders" => "purchase-orders". 
        /// Note that only numbers, letters, "-" and "_" may be used
        /// </summary>
        public string WordSeparator { get; private set; }

        /// <summary>
        /// Formats a directory name based on the supplied name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string FormatDirectoryName(string name)
        {
            string result = name;

            result = PathSegmentCleaner.Clean(result);

            if (WordSeparator != "")
            {
                result = IntercappedStringHelper.SeparateWords(result, WordSeparator);
            }
            switch (CaseStyle)
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

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("CaseStyle: {0}, WordSeparator: {1}", CaseStyle, WordSeparator);
        }
    }
}