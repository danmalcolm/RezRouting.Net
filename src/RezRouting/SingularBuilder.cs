using RezRouting.Options;

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
            urlPath = path;
        }

        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? options.PathFormatter.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}