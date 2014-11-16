using System;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
{
    public class SingularBuilder : ResourceBuilder, IConfigureSingular
    {
        private string urlPath;

        public SingularBuilder(string name) : base(name, ResourceLevel.Singular)
        {
            
        }

        public void UrlPath(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (!PathSegmentCleaner.IsValid(path))
            {
                throw new ArgumentException("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.", "path");
            }
            urlPath = path;
        }

        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? options.PathFormatter.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}