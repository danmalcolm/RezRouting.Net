using System;
using System.Collections.Generic;
using RezRouting.Options;

namespace RezRouting
{
    public class RouteMapper
    {
        private readonly BaseBuilder baseBuilder = new BaseBuilder();
        private readonly List<IRouteType> routeTypes = new List<IRouteType>();
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

        public void RouteTypes(params IRouteType[] routeTypes)
        {
            this.routeTypes.AddRange(routeTypes);
        }

        public void RouteTypes(IEnumerable<IRouteType> routeTypes)
        {
            this.routeTypes.AddRange(routeTypes);
        }

        public void Options(Action<IConfigureOptions> configure)
        {
            configure(optionsBuilder);
        }

        /// <summary>
        /// Sets a base path, within which all resource URLs will be nested
        /// </summary>
        /// <param name="path"></param>
        public void BasePath(string path)
        {
            baseBuilder.UrlPath(path);
        }

        public ResourcesModel Build()
        {
            var options = optionsBuilder.Build();
            var context = new RouteMappingContext(routeTypes, options);
            var rootResource = baseBuilder.Build(context);
            return new ResourcesModel(rootResource.Children);
        }
    }
}