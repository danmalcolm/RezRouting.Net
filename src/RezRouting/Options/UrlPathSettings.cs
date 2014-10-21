﻿using System;
using RezRouting.Utility;

namespace RezRouting.Options
{
    /// <summary>
    /// Settings that apply to formatting of a resource's path in a route URL
    /// </summary>
    public class UrlPathSettings
    {
        /// <summary>
        /// Creates a new instance of UrlPathSettings
        /// </summary>
        /// <param name="caseStyle">The CaseStyle to be used</param>
        /// <param name="wordSeparator">A string added between "camel humps" in the resource name, e.g. "PurchaseOrders" => "purchase-orders". 
        /// Note that only numbers, letters, "-" and "_" may be used</param>
        public UrlPathSettings(CaseStyle caseStyle = CaseStyle.Lower, string wordSeparator = null)
        {
            wordSeparator = wordSeparator ?? "";
            if (PathSegmentCleaner.IsValid(wordSeparator))
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

        public override string ToString()
        {
            return string.Format("CaseStyle: {0}, WordSeparator: {1}", CaseStyle, WordSeparator);
        }
    }
}