using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;

namespace RezRouting
{
    /// <summary>
    /// Base class for configuring resource types
    /// </summary>
    public abstract class ResourceBuilder : IConfigureResource, IResourceBuilder
    {
        private readonly List<Type> controllerTypes = new List<Type>();
        private readonly Dictionary<string, object> customProperties = new Dictionary<string, object>();
        private readonly List<Route> routes = new List<Route>();

        protected ResourceBuilder(string name, ResourceLevel level)
        {
            Level = level;
            Name = name;
            ChildBuilders = new List<IResourceBuilder>();
        }

        protected string Name { get; set; }

        protected ResourceLevel Level { get; private set; }
        
        protected List<IResourceBuilder> ChildBuilders { get; private set; }

        protected abstract IUrlSegment GetUrlSegment(RouteOptions options);

        public Resource Build(RouteMappingContext context)
        {
            var children = ChildBuilders.Select(x => x.Build(context)).ToList();
            var urlSegment = GetUrlSegment(context.Options);
            var resource = new Resource(Name, urlSegment, Level, customProperties, children);
            var conventionRoutes = from convention in context.RouteConventions
                from route in convention.Create(resource, controllerTypes, context.Options.PathFormatter)
                select route;

            var allRoutes = routes.Concat(conventionRoutes);
            resource.InitRoutes(allRoutes);

            return resource;
        }

        protected void AddChild<T>(T childBuilder, Action<T> configure)
            where T : IConfigureResource, IResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }

        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            AddChild(new SingularBuilder(name), x => configure(x));
        }

        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            AddChild(new CollectionBuilder(name), x => configure(x));
        }

        public void Collection(string name, string itemName, Action<IConfigureCollection> configure)
        {
            AddChild(new CollectionBuilder(name, itemName), x => configure(x));
        }

        public void HandledBy<T>()
        {
            HandledBy(typeof(T));
        }
        
        public void HandledBy(Type type)
        {
            controllerTypes.Add(type);
        }

        public void CustomProperties(IDictionary<string, object> properties)
        {
            foreach (var item in properties)
            {
                customProperties[item.Key] = item.Value;
            }
        }

        public void Route(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string,object> customProperties = null)
        {
            var route = new Route(name, controllerType, action, httpMethod, path, customProperties ?? new Dictionary<string, object>());
            routes.Add(route);
        }
    }
}