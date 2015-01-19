using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Base class with shared functionality for builders used to configure each type of resource
    /// </summary>
    public abstract class ResourceBuilderBase : IResourceConfigurator, IResourceBuilder
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
        private List<IResourceBuilder> ChildBuilders { get; set; }

        /// <summary>
        /// Gets URL segment within the resource's route URL template
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract IUrlSegment GetUrlSegment(ResourceOptions options);

        /// <inheritdoc />
        public Resource Build(ResourceOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            var children = ChildBuilders.Select(x => x.Build(options)).ToList();
            var urlSegment = GetUrlSegment(options);
            var resource = new Resource(Name, urlSegment, Type, customProperties, children);
            var conventionRoutes = from convention in options.RouteConventions
                from route in convention.Create(resource, handlers, options.UrlPathSettings)
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
            where T : IResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }

        /// <inheritdoc />
        public void Singular(string name, Action<ISingularConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (configure == null) throw new ArgumentNullException("configure");

            AddChild(new SingularBuilder(name), x => configure(x));
        }

        /// <inheritdoc />
        public void Collection(string name, Action<ICollectionConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (configure == null) throw new ArgumentNullException("configure");

            AddChild(new CollectionBuilder(name), x => configure(x));
        }

        /// <inheritdoc />
        public void Collection(string name, string itemName, Action<ICollectionConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (itemName == null) throw new ArgumentNullException("itemName");

            AddChild(new CollectionBuilder(name, itemName), x => configure(x));
        }

        /// <inheritdoc />
        public void HandledBy(IResourceHandler handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");

            handlers.Add(handler);
        }

        /// <inheritdoc />
        public void CustomProperties(IDictionary<string, object> properties)
        {
            if (properties == null) throw new ArgumentNullException("properties");

            foreach (var item in properties)
            {
                customProperties[item.Key] = item.Value;
            }
        }

        /// <inheritdoc />
        public void Route(string name, IRouteHandler handler, string httpMethod, string path, IDictionary<string,object> customProperties = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (handler == null) throw new ArgumentNullException("handler");

            var route = new Route(name, handler, httpMethod, path, customProperties ?? new Dictionary<string, object>());
            routes.Add(route);
        }
    }
}