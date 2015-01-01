using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures the base-level resource within a resource hierarchy belonging to 
    /// an application or section of an application
    /// </summary>
    public class RootBuilder : ResourceBuilderBase
    {
        private string urlPath;

        /// <summary>
        /// Creates a RootBuilder
        /// </summary>
        public RootBuilder()
            : base("", ResourceType.Base)
        {
        }

        /// <summary>
        /// Sets the base URL path
        /// </summary>
        /// <param name="path"></param>
        public void UrlPath(string path)
        {
            urlPath = path;
        }

        /// <inheritdoc />
        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? "";
            return new DirectoryUrlSegment(path);
        }
    }
}