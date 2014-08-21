using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2
{
    public class ResourceBuilder
    {
        public ResourceBuilder(string name, ResourceLevel level)
        {
            Level = level;
            Name = name;
            ChildBuilders = new List<ResourceBuilder>();
        }

        protected string Name { get; set; }

        protected ResourceLevel Level { get; set; }
        
        protected List<ResourceBuilder> ChildBuilders { get; set; }

        public Resource Build()
        {
            var children = ChildBuilders.Select(x => x.Build());
            return new Resource(Name, Name, Level, children);
        }

        public void Singular(string name, Action<SingularResourceBuilder> configure)
        {
            AddChild(new SingularResourceBuilder(name), configure);
        }

        public void Collection(string name, Action<CollectionResourceBuilder> configure)
        {
            AddChild(new CollectionResourceBuilder(name), configure);
        }

        protected void AddChild<T>(T childBuilder, Action<T> configure)
            where T : ResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }
    }
}