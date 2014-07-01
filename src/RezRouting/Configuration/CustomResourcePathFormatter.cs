using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of resource path via a function
    /// </summary>
    internal class CustomResourcePathFormatter : IResourcePathFormatter
    {
        private readonly Func<string, string> format;

        public CustomResourcePathFormatter(Func<string, string> format)
        {
            if (format == null) throw new ArgumentNullException("format");
            this.format = format;
        }

        public string GetResourcePath(string name)
        {
            return format(name);
        }
    }
}