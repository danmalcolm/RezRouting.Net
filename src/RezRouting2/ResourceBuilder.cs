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
            UrlSegment = new DirectoryUrlSegment(Name);
        }

        protected string Name { get; private set; }

        protected ResourceLevel Level { get; private set; }
        
        protected List<ResourceBuilder> ChildBuilders { get; private set; }
        
        protected IUrlSegment UrlSegment { get; set; }

        public Resource Build()
        {
            var children = ChildBuilders.Select(x => x.Build());
            return new Resource(Name, UrlSegment, Level, children);
        }

        protected void AddChild<T>(T childBuilder, Action<T> configure)
            where T : ResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }

        public void Singular(string name, Action<SingularBuilder> configure)
        {
            AddChild(new SingularBuilder(name), configure);
        }

        public void Collection(string name, Action<CollectionBuilder> configure)
        {
            AddChild(new CollectionBuilder(name), configure);
        }
    }
}