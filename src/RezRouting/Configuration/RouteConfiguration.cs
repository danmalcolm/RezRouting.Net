using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Settings that apply to routes set up by a RouteMapper
    /// </summary>
    internal class RouteConfiguration
    {
        public RouteConfiguration(List<RouteType> routeTypes, IResourceNameConvention resourceNameConvention, IResourcePathFormatter resourcePathFormatter, string routeNamePrefix)
        {
            RouteTypes = routeTypes;
            ResourceNameConvention = resourceNameConvention;
            ResourcePathFormatter = resourcePathFormatter;
            RouteNamePrefix = routeNamePrefix;
        }

        public List<RouteType> RouteTypes { get; private set; }

        public IResourceNameConvention ResourceNameConvention { get; private set; }

        public IResourcePathFormatter ResourcePathFormatter { get; private set; }

        public string RouteNamePrefix { get; private set; }
    }
}