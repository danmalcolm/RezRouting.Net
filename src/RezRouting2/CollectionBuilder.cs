using System;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class CollectionBuilder : ResourceBuilder, IConfigureCollection, IResourceBuilder
    {
        private readonly CollectionItemBuilder itemBuilder;

        public CollectionBuilder(string name) : base(name, ResourceLevel.Collection)
        {
            string itemName = name.Singularize(Plurality.Plural);
            itemBuilder = new CollectionItemBuilder(itemName);
            AddChild(itemBuilder, x => {});
        }

        public void Items(Action<CollectionItemBuilder> configure)
        {
            configure(itemBuilder);
        }

        public void UrlPath(string path)
        {
            UrlSegment = new DirectoryUrlSegment(path);
        }
    }
}