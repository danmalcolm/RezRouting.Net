using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2
{
    public class RouteMapper
    {
        private readonly List<ResourceBuilder> builders = new List<ResourceBuilder>();
        private readonly List<RouteType> routeTypes = new List<RouteType>();

        public void Collection(string name, Action<CollectionBuilder> configure)
        {
            AddBuilder(new CollectionBuilder(name), configure);
        }

        public void Singular(string name, Action<SingularBuilder> configure)
        {
            AddBuilder(new SingularBuilder(name), configure);
        }

        private void AddBuilder<T>(T builder, Action<T> configure)
            where T : ResourceBuilder
        {
            builders.Add(builder);
            configure(builder);
        }

        public IEnumerable<Resource> Build()
        {
            var context = new RouteMappingContext(routeTypes);
            return builders.Select(x => x.Build(context));
        }

        public void RouteTypes(params RouteType[] types)
        {
            this.routeTypes.AddRange(types);
        }
    }
}