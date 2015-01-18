using System;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Configures and creates a collection Resource
    /// </summary>
    public class CollectionBuilder : ResourceBuilderBase, ICollectionConfigurator
    {
        private readonly CollectionItemBuilder itemBuilder;
        private string urlPath;

        /// <summary>
        /// Creates a new CollectionBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionBuilder(string name, string itemName = null) : base(name, ResourceType.Collection)
        {
            if (name == null) throw new ArgumentNullException("name");
            // Use specified name, attempt to singularise, or append Item if it can't be singularised
            itemName = itemName ?? name.Singularize() ?? string.Format("{0}Item", name);
            itemBuilder = new CollectionItemBuilder(itemName);
            AddChild(itemBuilder, x => {});
        }

        /// <inheritdoc />
        public void Items(Action<CollectionItemBuilder> configure)
        {
            if (configure == null) throw new ArgumentNullException("configure");

            configure(itemBuilder);
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
        protected override IUrlSegment GetUrlSegment(ResourceOptions options)
        {
            string path = urlPath ?? options.UrlPathSettings.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}