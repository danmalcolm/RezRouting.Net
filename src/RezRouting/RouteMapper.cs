using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Model;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// The entry point for mapping routes for resources within a web application
    /// </summary>
    public class RouteMapper
    {
        private readonly RouteConfigurationBuilder configurationBuilder 
            = new RouteConfigurationBuilder(StandardRouteTypes.Build());
        private readonly List<ResourceBuilder> builders = new List<ResourceBuilder>();
        
        /// <summary>
        /// Customises all routes by mapped by the current RouteMapper by changing 
        /// shared route configuration options
        /// </summary>
        /// <param name="configure"></param>
        public void Configure(Action<RouteConfigurationBuilder> configure)
        {
            configure(configurationBuilder);
        }

        /// <summary>
        /// Sets up routes for a collection of resources
        /// </summary>
        /// <param name="configure"></param>
        public void Collection(Action<CollectionBuilder> configure)
        {
            var builder = new CollectionBuilder();
            configure(builder);
            builders.Add(builder);
        }

        /// <summary>
        /// Sets up routes for a singular resource
        /// </summary>
        /// <param name="configure"></param>
        public void Singular(Action<SingularBuilder> configure)
        {
            var builder = new SingularBuilder();
            configure(builder);
            builders.Add(builder);
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
            var resources = BuildResources();
            resources.Each(x => x.MapRoutes(routes));
            return routes;
        }

        /// <summary>
        /// Gets a plain text summary containing information about all configured resources
        /// </summary>
        /// <returns></returns>
        public string DebugSummary()
        {
            var resources = BuildResources();
            var summary = new StringBuilder();
            resources.Each(x => x.DebugSummary(summary, 0));
            return summary.ToString();
        }

        private IEnumerable<Resource> BuildResources()
        {
            var configuration = configurationBuilder.Build();
            var context = new ResourceBuildContext(new string[0], null);
            return builders.Select(x => x.Build(configuration, context));
        }
    }
}