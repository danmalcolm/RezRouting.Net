using System.Collections.Generic;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
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