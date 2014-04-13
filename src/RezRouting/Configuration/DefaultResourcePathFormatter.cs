﻿using System.Text.RegularExpressions;
using RezRouting.Routing;

namespace RezRouting.Configuration
{
    public class DefaultResourcePathFormatter : IResourcePathFormatter
    {
        private readonly ResourcePathSettings settings;

        public DefaultResourcePathFormatter(ResourcePathSettings settings)
        {
            this.settings = settings;
        }

        public string GetResourcePath(string resourceName)
        {
            string result = resourceName;

            result = PathSegmentCleaner.Clean(result);

            if (settings.WordSeparator != "")
            {
                result = Regex.Replace(result, "([a-z])(?=[A-Z])", "$1-");
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