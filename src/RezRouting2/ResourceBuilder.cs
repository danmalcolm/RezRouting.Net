using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2
{
    public class ResourceBuilder
    {
        private readonly List<Type> controllerTypes = new List<Type>();

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

        public Resource Build(RouteMappingContext context)
        {
            var children = ChildBuilders.Select(x => x.Build(context));
            var resource = new Resource(Name, UrlSegment, Level, children);
            var routes = from controllerType in controllerTypes
                from routeType in context.RouteTypes
                let route = routeType.BuildRoute(resource, controllerType)
                where route != null
                select route;

            resource.InitRoutes(routes);

            return resource;
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

        public void HandledBy<T>()
        {
            HandledBy(typeof(T));
        }
        
        public void HandledBy(Type type)
        {
            controllerTypes.Add(type);
        }
    }
}