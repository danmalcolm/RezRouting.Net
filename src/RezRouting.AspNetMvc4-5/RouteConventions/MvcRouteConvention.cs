using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Extensions;
using RezRouting.Configuration.Options;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Base class for MVC Route Conventions - contains shared logic for identifying
    /// controller types that handle a resource's routes.
    /// </summary>
    public abstract class MvcRouteConvention : RouteConventionBase
    {
        protected override sealed IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options)
        {
            var controllerTypes = GetControllerTypes(resource, context);
            return CreateRoutes(resource, context, options, controllerTypes);
        }

        /// <summary>
        /// Creates the routes for a given resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="controllerTypes"></param>
        /// <returns></returns>
        protected abstract IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options, IEnumerable<Type> controllerTypes);

        private static IEnumerable<Type> GetControllerTypes(ResourceData resource, ConfigurationContext context)
        {
            var extensionData = resource.ExtensionData;
            var specific = extensionData.GetControllerTypes();
            var sharedExtensionData = context.SharedExtensionData;
            var fromHierarchy = ControllerHierarchyHelper.GetControllers(resource, sharedExtensionData, context.Cache);
            var all = specific.Concat(fromHierarchy);
            return all;
        }
    }
}