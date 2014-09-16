using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Settings that apply to routes mapped by a RouteMapper instance
    /// </summary>
    public class RouteConfiguration
    {
        public RouteConfiguration(IEnumerable<RouteType> routeTypes, IResourceNameConvention resourceNameConvention, 
            IResourcePathFormatter resourcePathFormatter, IRouteNameConvention routeNameConvention, 
            IIdNameConvention idNameConvention, string routeNamePrefix)
        {
            RouteTypes = routeTypes.ToReadOnlyList();
            ResourceNameConvention = resourceNameConvention;
            ResourcePathFormatter = resourcePathFormatter;
            RouteNameConvention = routeNameConvention;
            IdNameConvention = idNameConvention;
            RouteNamePrefix = routeNamePrefix;
        }

        public IList<RouteType> RouteTypes { get; private set; }

        public IResourceNameConvention ResourceNameConvention { get; private set; }

        public IResourcePathFormatter ResourcePathFormatter { get; private set; }

        public IRouteNameConvention RouteNameConvention { get; private set; }

        public IIdNameConvention IdNameConvention { get; private set; }

        public string RouteNamePrefix { get; private set; }
    }
}