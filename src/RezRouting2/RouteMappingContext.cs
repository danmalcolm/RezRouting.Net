using System.Collections.Generic;
using RezRouting2.Options;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class RouteMappingContext
    {
        public RouteMappingContext(IEnumerable<IRouteType> routeTypes, RouteOptions options)
        {
            Options = options;
            RouteTypes = routeTypes.ToReadOnlyList();
        }

        public IList<IRouteType> RouteTypes { get; private set; }

        public RouteOptions Options { get; private set; }
    }
}