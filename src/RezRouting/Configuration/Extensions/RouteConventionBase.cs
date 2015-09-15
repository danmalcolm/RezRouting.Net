using System.Collections.Generic;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Utility;

namespace RezRouting.Configuration.Extensions
{
    /// <summary>
    /// Base class for extensions that add routes to the root resource and its
    /// descendants during route configuration.
    /// </summary>
    public abstract class RouteConventionBase : IExtension
    {
        public void Extend(ResourceData root, ConfigurationContext context, ConfigurationOptions options)
        {
            var resources = root.Expand();
            foreach (var resource in resources)
            {
                var routes = CreateRoutes(resource, context, options);
                routes.Each(resource.AddRoute);
            }
        }

        protected abstract IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options);
    }
}