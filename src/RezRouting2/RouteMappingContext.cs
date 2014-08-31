using System.Collections.Generic;
using RezRouting2.Options;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class RouteMappingContext
    {
        public RouteMappingContext(IEnumerable<RouteType> routeTypes, RouteOptions options)
        {
            Options = options;
            RouteTypes = routeTypes.ToReadOnlyList();
        }

        public IList<RouteType> RouteTypes { get; private set; }

        public RouteOptions Options { get; private set; }
    }
}