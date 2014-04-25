using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Settings that apply to routes set up by a RouteMapper
    /// </summary>
    internal class RouteConfiguration
    {
        public RouteConfiguration(IEnumerable<RouteType> routeTypes, IResourceNameConvention resourceNameConvention, IResourcePathFormatter resourcePathFormatter, string routeNamePrefix)
        {
            RouteTypes = routeTypes.ToReadOnlyList();
            ResourceNameConvention = resourceNameConvention;
            ResourcePathFormatter = resourcePathFormatter;
            RouteNamePrefix = routeNamePrefix;
        }

        public IList<RouteType> RouteTypes { get; private set; }

        public IResourceNameConvention ResourceNameConvention { get; private set; }

        public IResourcePathFormatter ResourcePathFormatter { get; private set; }

        public string RouteNamePrefix { get; private set; }
    }
}