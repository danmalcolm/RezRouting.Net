using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Used by RouteMapper to configure the root level resource within 
    /// the resource hierarchy
    /// </summary>
    public class BaseBuilder : ResourceBuilder
    {
        private string urlPath;

        /// <summary>
        /// Creates a BaseBuilder
        /// </summary>
        public BaseBuilder()
            : base("", ResourceLevel.Base)
        {
        }

        /// <summary>
        /// Sets the name of the resource
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets the name
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