using RezRouting.Options;

namespace RezRouting
{
    /// <summary>
    /// Used by RouteMapper to configure the root level resource within 
    /// the resource hierarchy
    /// </summary>
    public class BaseBuilder : ResourceBuilder
    {
        private string urlPath;

        public BaseBuilder()
            : base("", ResourceLevel.Base)
        {
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void UrlPath(string path)
        {
            urlPath = path;
        }

        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? "";
            return new DirectoryUrlSegment(path);
        }
    }
}