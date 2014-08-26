using System.Collections.Generic;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class RouteMappingContext
    {
        public RouteMappingContext(IEnumerable<RouteType> routeTypes)
        {
            RouteTypes = routeTypes.ToReadOnlyList();
        }

        public IList<RouteType> RouteTypes { get; private set; }  
    }
}