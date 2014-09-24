using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;

namespace RezRouting
{
    public class RouteMapper
    {
        private readonly List<ResourceBuilder> builders = new List<ResourceBuilder>();
        private readonly List<IRouteType> routeTypes = new List<IRouteType>();
        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            AddBuilder(new CollectionBuilder(name), x => configure(x));
        }

        public void Collection(string name, string itemName, Action<IConfigureCollection> configure)
        {
            AddBuilder(new CollectionBuilder(name, itemName), x => configure(x));
        }

        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            AddBuilder(new SingularBuilder(name), x => configure(x));
        }

        private void AddBuilder<T>(T builder, Action<T> configure)
            where T : ResourceBuilder,IConfigureResource
        {
            builders.Add(builder);
            configure(builder);
        }

        public IEnumerable<Resource> Build()
        {
            var options = optionsBuilder.Build();
            var context = new RouteMappingContext(routeTypes, options);
            return builders.Select(x => x.Build(context));
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
    }
}