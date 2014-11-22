using System.Collections.Generic;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Contains data used while creating Routes
    /// </summary>
    public class RouteMappingContext
    {
        /// <summary>
        /// Creates a new RouteMappingContext
        /// </summary>
        /// <param name="routeConventions"></param>
        /// <param name="options"></param>
        public RouteMappingContext(IEnumerable<IRouteConvention> routeConventions, RouteOptions options)
        {
            Options = options;
            RouteConventions = routeConventions.ToReadOnlyList();
        }

        /// <summary>
        /// The RouteConventions used to create common types of Route
        /// </summary>
        public IList<IRouteConvention> RouteConventions { get; private set; }

        /// <summary>
        /// The RouteOptions configured for the Routes being created
        /// </summary>
        public RouteOptions Options { get; private set; }
    }
}