using System.Collections.Generic;
using RezRouting.Configuration.Conventions;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Contains common objects made available when creating resource model. It is 
    /// shared by the entire resource hierarchy as resources and routes are created.
    /// </summary>
    public class ConfigurationContext
    {
        public ConfigurationContext(List<IRouteConvention> routeConventions)
        {
            RouteConventions = routeConventions;
        }

        /// <summary>
        /// Conventions used to add routes to all resources being configured
        /// </summary>
        public List<IRouteConvention> RouteConventions { get; private set; }

        /// <summary>
        /// A collection of data made available to all builders in the hierarchy as
        /// the resource model is built - suitable for optimizations such as 
        /// caching reusable data.
        /// </summary>
        public Dictionary<string, object> Items { get; private set; }
    }
}