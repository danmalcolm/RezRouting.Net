using RezRouting.Options;

namespace RezRouting
{
    /// <summary>
    /// Used by RouteMapper to configure resources at root / base level
    /// within the resource hierarchy
    /// </summary>
    public class BaseBuilder : ResourceBuilder
    {
        private string urlPath;

        public BaseBuilder()
            : base("", ResourceLevel.Base)
        {
        }

        public void UrlPath(string path)
        {
            urlPath = path;
        }

        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? options.PathFormatter.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}