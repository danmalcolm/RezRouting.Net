using System;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Configures a singular-level Resource
    /// </summary>
    public class SingularBuilder : ResourceBuilder, IConfigureSingular
    {
        private string urlPath;

        /// <summary>
        /// Creates a new SingularBuilder
        /// </summary>
        /// <param name="name"></param>
        public SingularBuilder(string name) : base(name, ResourceLevel.Singular)
        {
            
        }

        /// <inheritdoc />
        public void UrlPath(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (!PathSegmentCleaner.IsValid(path))
            {
                throw new ArgumentException("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.", "path");
            }
            urlPath = path;
        }

        /// <inheritdoc />
        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? options.PathFormatter.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}