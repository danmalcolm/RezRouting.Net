using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Route mapping settings
    /// </summary>
    internal class RouteConfiguration
    {
        public RouteConfiguration(IEnumerable<RouteType> routeTypes, IResourceNameConvention resourceNameConvention, IResourcePathFormatter resourcePathFormatter, IRouteNameConvention routeNameConvention, string routeNamePrefix)
        {
            RouteTypes = routeTypes.ToReadOnlyList();
            ResourceNameConvention = resourceNameConvention;
            ResourcePathFormatter = resourcePathFormatter;
            RouteNameConvention = routeNameConvention;
            RouteNamePrefix = routeNamePrefix;
        }

        public IList<RouteType> RouteTypes { get; private set; }

        public IResourceNameConvention ResourceNameConvention { get; private set; }

        public IResourcePathFormatter ResourcePathFormatter { get; private set; }

        public IRouteNameConvention RouteNameConvention { get; private set; }

        public string RouteNamePrefix { get; private set; }
    }
}