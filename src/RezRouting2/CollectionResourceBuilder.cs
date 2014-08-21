using System;

namespace RezRouting2
{
    public class CollectionResourceBuilder : ResourceBuilder
    {
        public CollectionResourceBuilder(string name) : base(name, ResourceLevel.Collection)
        {
        }

        public void Items(Action<CollectionItemResourceBuilder> configure)
        {
            AddChild(new CollectionItemResourceBuilder("Item"), configure);
        }
    }
}