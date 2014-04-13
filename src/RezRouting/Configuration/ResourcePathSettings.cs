﻿using System;
using RezRouting.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Settings control how the path to a resource within the route URL is formatted, based
    /// on the resource name.
    /// </summary>
    public class ResourcePathSettings
    {
        /// <summary>
        /// Creates a new instance of ResourcePathSettings
        /// </summary>
        /// <param name="caseStyle"></param>
        /// <param name="wordSeparator">A string added between "camel humps" in the resource name. 
        /// Can be used to hyphens or underscores, e.g. PurchaseOrders => purchase-orders</param>
        public ResourcePathSettings(CaseStyle caseStyle = CaseStyle.Lower, string wordSeparator = null)
        {
            wordSeparator = wordSeparator ?? "";
            if (PathSegmentCleaner.IsValid(wordSeparator))
            {
                throw new ArgumentException("Characters within the separator are not valid for use within a URL path. Only numbers, letters, - and _ may be used", "wordSeparator");
            }

            CaseStyle = caseStyle;
            WordSeparator = wordSeparator;
        }

        public CaseStyle CaseStyle { get; private set; }

        public string WordSeparator { get; private set; }

        public override string ToString()
        {
            return string.Format("CaseStyle: {0}, WordSeparator: {1}", CaseStyle, WordSeparator);
        }
    }
}