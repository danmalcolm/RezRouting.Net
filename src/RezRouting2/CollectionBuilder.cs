using System;

namespace RezRouting2
{
    public class CollectionBuilder : ResourceBuilder
    {
        private readonly CollectionItemBuilder itemBuilder;

        public CollectionBuilder(string name) : base(name, ResourceLevel.Collection)
        {
            itemBuilder = new CollectionItemBuilder("Item");
            itemBuilder.UrlPath("{id}");
            AddChild(itemBuilder, x => {});
        }

        public void Items(Action<CollectionItemBuilder> configure)
        {
            configure(itemBuilder);
        }
    }
}