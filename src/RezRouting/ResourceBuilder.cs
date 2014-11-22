using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;

namespace RezRouting
{
    /// <summary>
    /// Base class used to configure and create Resources
    /// </summary>
    public abstract class ResourceBuilder : IConfigureResource, IResourceBuilder
    {
        private readonly List<Type> controllerTypes = new List<Type>();
        private readonly Dictionary<string, object> customProperties = new Dictionary<string, object>();
        private readonly List<Route> routes = new List<Route>();

        /// <summary>
        /// Creates a ResourceBuilder
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        protected ResourceBuilder(string name, ResourceLevel level)
        {
            Level = level;
            Name = name;
            ChildBuilders = new List<IResourceBuilder>();
        }

        /// <summary>
        /// The name of the Resource
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// The level of the Resource
        /// </summary>
        protected ResourceLevel Level { get; private set; }
        
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
            var resource = new Resource(Name, urlSegment, Level, customProperties, children);
            var conventionRoutes = from convention in context.RouteConventions
                from route in convention.Create(resource, controllerTypes, context.Options.PathFormatter)
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
        public void Collection(string name, string itemName, Action<IConfigureCollection> configure)
        {
            AddChild(new CollectionBuilder(name, itemName), x => configure(x));
        }

        /// <inheritdoc />
        public void HandledBy<T>()
        {
            HandledBy(typeof(T));
        }

        /// <inheritdoc />
        public void HandledBy(Type type)
        {
            controllerTypes.Add(type);
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
        public void Route(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string,object> customProperties = null)
        {
            var route = new Route(name, controllerType, action, httpMethod, path, customProperties ?? new Dictionary<string, object>());
            routes.Add(route);
        }
    }
}