using System;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
{
    public class CollectionBuilder : ResourceBuilder, IConfigureCollection, IResourceBuilder
    {
        private readonly CollectionItemBuilder itemBuilder;
        private string urlPath;

        public CollectionBuilder(string name, string itemName = null) : base(name, ResourceLevel.Collection)
        {
            if (name == null) throw new ArgumentNullException("name");

            itemName = itemName ?? name.Singularize() ?? string.Format("{0}Item", name);
            itemBuilder = new CollectionItemBuilder(itemName);
            AddChild(itemBuilder, x => {});
        }

        public void Items(Action<CollectionItemBuilder> configure)
        {
            configure(itemBuilder);
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