using System;
using System.Collections.Generic;
using RezRouting.Options;

namespace RezRouting
{
    public class RouteMapper
    {
        private readonly BaseBuilder baseBuilder = new BaseBuilder();
        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>();
        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            baseBuilder.Collection(name, configure);
        }

        public void Collection(string name, string itemName, Action<IConfigureCollection> configure)
        {
            baseBuilder.Collection(name, itemName, configure);
        }

        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            baseBuilder.Singular(name, configure);
        }

        public void RouteConventions(params IRouteConvention[] conventions)
        {
            this.routeConventions.AddRange(conventions);
        }

        public void RouteConventions(IEnumerable<IRouteConvention> conventions)
        {
            this.routeConventions.AddRange(conventions);
        }

        public void Options(Action<IConfigureOptions> configure)
        {
            configure(optionsBuilder);
        }

        /// <summary>
        /// Sets a base path for resource URLs. All URLs will be nested below the specified
        /// path.
        /// </summary>
        /// <param name="path"></param>
        public void BasePath(string path)
        {
            baseBuilder.UrlPath(path);
        }

        /// <summary>
        /// Sets a base name for resources. The specified name will be included in the
        /// full names of all resources and routes mapped by this RouteMapper.
        /// </summary>
        /// <param name="name"></param>
        public void BaseName(string name)
        {
            baseBuilder.SetName(name);
        }
        
        public virtual ResourcesModel Build()
        {
            var options = optionsBuilder.Build();
            var context = new RouteMappingContext(routeConventions, options);
            var rootResource = baseBuilder.Build(context);
            return new ResourcesModel(rootResource.Children);
        }
    }
}