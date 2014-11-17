using System.Collections.Generic;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
{
    public class RouteMappingContext
    {
        public RouteMappingContext(IEnumerable<IRouteConvention> routeConventions, RouteOptions options)
        {
            Options = options;
            RouteConventions = routeConventions.ToReadOnlyList();
        }

        public IList<IRouteConvention> RouteConventions { get; private set; }

        public RouteOptions Options { get; private set; }
    }
}