using System;
using System.Linq;
using System.Text;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Model;

namespace RezRouting
{
    /// <summary>
    /// The entry point used for mapping routes for resources within a web application
    /// </summary>
    public class RootResourceBuilder : ResourceBuilder
    {
        private readonly RouteConfiguration configuration = new RouteConfiguration();

        public RootResourceBuilder()
        {
            CustomName("Root");
            CustomUrlPath("");
            Include("Show");
        }

        protected override ResourceType ResourceType
        {
            get { return ResourceType.Singular; }
        }

        /// <summary>
        /// Maps routes for all configured resources to a new RouteCollection
        /// </summary>
        /// <returns>New RouteCollection</returns>
        public RouteCollection MapRoutes()
        {
            return MapRoutes(new RouteCollection());
        }

        /// <summary>
        /// Maps routes for all configured resources to an existing RouteCollection
        /// </summary>
        /// <param name="routes"></param>
        /// <returns>The original RouteCollection</returns>
        public RouteCollection MapRoutes(RouteCollection routes)
        {
            var resource = Build(configuration, Enumerable.Empty<Resource>());
            resource.MapRoutes(routes);
            return routes;
        }

        /// <summary>
        /// Gets a plain text summary containing information about all configured resources
        /// </summary>
        /// <returns></returns>
        public string DebugSummary()
        {
            var resource = Build(configuration, Enumerable.Empty<Resource>());
            var summary = new StringBuilder();
            resource.DebugSummary(summary, 0);
            return summary.ToString();
        }

        /// <summary>
        /// Customises all routes by changing shared route configuration. Note that this is
        /// a shared configuration that is applied to all resources. Resource-specific 
        /// configuration methods should be used to override settings on a single resource.
        /// </summary>
        /// <param name="configure"></param>
        public void Configure(Action<RouteConfiguration> configure)
        {
            configure(configuration);
        }
    }
}