using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Base class for builders used to configure different types of resource
    /// </summary>
    public abstract class ResourceBuilderBase : IConfigureResource, IResourceBuilder
    {
        private readonly List<IResourceHandler> handlers = new List<IResourceHandler>();
        private readonly Dictionary<string, object> customProperties = new Dictionary<string, object>();
        private readonly List<Route> routes = new List<Route>();

        /// <summary>
        /// Creates a ResourceBuilderBase
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        protected ResourceBuilderBase(string name, ResourceType type)
        {
            Type = type;
            Name = name;
            ChildBuilders = new List<IResourceBuilder>();
        }

        /// <summary>
        /// The name of the Resource
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// The type of the Resource
        /// </summary>
        protected ResourceType Type { get; private set; }
        
        /// <summary>
        /// A collection of builders used to configure child resources
        /// </summary>
        protected List<IResourceBuilder> ChildBuilders { get; private set; }

        /// <summary>
        /// Gets URL segment within the resource's route URL template
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract IUrlSegment GetUrlSegment(RouteOptions options);

        /// <inheritdoc />
        public Resource Build(RouteMappingContext context)
        {
            var children = ChildBuilders.Select(x => x.Build(context)).ToList();
            var urlSegment = GetUrlSegment(context.Options);
            var resource = new Resource(Name, urlSegment, Type, customProperties, children);
            var conventionRoutes = from convention in context.RouteConventions
                from route in convention.Create(resource, handlers, context.Options.PathFormatter)
                select route;

            var allRoutes = routes.Concat(conventionRoutes);
            resource.InitRoutes(allRoutes);

            return resource;
        }

        /// <summary>
        /// Adds a child resource to the current resource being configured
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="childBuilder"></param>
        /// <param name="configure"></param>
        protected void AddChild<T>(T childBuilder, Action<T> configure)
            where T : IConfigureResource, IResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }

        /// <inheritdoc />
        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            AddChild(new SingularBuilder(name), x => configure(x));
        }

        /// <inheritdoc />
        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            AddChild(new CollectionBuilder(name), x => configure(x));
        }

        /// <inheritdoc />
        public void HandledBy(IResourceHandler handler)
        {
            handlers.Add(handler);
        }

        /// <inheritdoc />
        public void CustomProperties(IDictionary<string, object> properties)
        {
            foreach (var item in properties)
            {
                customProperties[item.Key] = item.Value;
            }
        }

        /// <inheritdoc />
        public void Route(string name, IRouteHandler handler, string httpMethod, string path, IDictionary<string,object> customProperties = null)
        {
            var route = new Route(name, handler, httpMethod, path, customProperties ?? new Dictionary<string, object>());
            routes.Add(route);
        }

        /// <summary>
        /// Changes the name of the resource
        /// </summary>
        /// <param name="name"></param>
        internal protected void ChangeName(string name)
        {
            Name = name;
        }
    }
}