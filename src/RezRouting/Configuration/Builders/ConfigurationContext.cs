using System.Collections.Generic;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Contains common objects made available when creating resource model. It is 
    /// shared by the entire resource hierarchy as resources and routes are created.
    /// </summary>
    public class ConfigurationContext
    {
        public ConfigurationContext(List<IRouteConvention> routeConventions, CustomValueCollection sharedConventionData)
        {
            RouteConventions = routeConventions;
            SharedConventionData = sharedConventionData;
            Items = new CustomValueCollection();
        }

        /// <summary>
        /// Conventions used to add routes to all resources being configured
        /// </summary>
        public List<IRouteConvention> RouteConventions { get; private set; }

        /// <summary>
        /// Shared convention data specified on the root resource
        /// </summary>
        public CustomValueCollection SharedConventionData { get; set; }

        /// <summary>
        /// A collection of data shared by all builders in the hierarchy as
        /// the resource model is built - suitable for optimizations such as 
        /// caching shared data.
        /// </summary>
        public CustomValueCollection Items { get; private set; }
    }
}