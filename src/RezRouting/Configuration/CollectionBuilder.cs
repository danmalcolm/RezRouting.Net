using System;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures and creates a collectiontype Resource
    /// </summary>
    public class CollectionBuilder : ResourceBuilderBase, IConfigureCollection
    {
        private readonly CollectionItemBuilder itemBuilder;
        private string urlPath;

        /// <summary>
        /// Creates a new CollectionBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionBuilder(string name) : base(name, ResourceType.Collection)
        {
            if (name == null) throw new ArgumentNullException("name");

            string itemName = name.Singularize() ?? string.Format("{0}Item", name);
            itemBuilder = new CollectionItemBuilder(itemName);
            AddChild(itemBuilder, x => {});
        }

        /// <inheritdoc />
        public void ItemName(string name)
        {
            itemBuilder.ChangeName(name);
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
        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string path = urlPath ?? options.PathFormatter.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }
    }
}