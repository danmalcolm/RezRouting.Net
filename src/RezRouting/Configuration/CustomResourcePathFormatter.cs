using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of resource path via a function
    /// </summary>
    public class CustomResourcePathFormatter : IResourcePathFormatter
    {
        private readonly Func<string, string> create;

        public CustomResourcePathFormatter(Func<string, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            this.create = create;
        }

        public string GetResourcePath(string resourceName)
        {
            return create(resourceName);
        }
    }
}