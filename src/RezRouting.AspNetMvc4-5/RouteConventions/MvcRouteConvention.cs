using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Base class for MVC Route Conventions - contains shared logic for deriving controllers
    /// that handle a resource's routes.
    /// </summary>
    public abstract class MvcRouteConvention : IRouteConvention
    {
        public IEnumerable<Route> Create(ResourceData resource, CustomValueCollection sharedConventionData, CustomValueCollection conventionData, UrlPathSettings urlPathSettings, CustomValueCollection contextItems)
        {
            var controllerTypes = GetControllerTypes(resource, sharedConventionData, conventionData, contextItems);
            return Create(resource, sharedConventionData, conventionData, urlPathSettings, contextItems, controllerTypes);
        }

        protected abstract IEnumerable<Route> Create(ResourceData resource, CustomValueCollection sharedConventionData, CustomValueCollection conventionData, UrlPathSettings urlPathSettings, CustomValueCollection contextItems, IEnumerable<Type> controllerTypes);

        private static IEnumerable<Type> GetControllerTypes(ResourceData resource, CustomValueCollection sharedConventionData, CustomValueCollection conventionData,
            CustomValueCollection contextItems)
        {
            var specific = conventionData.GetControllerTypes();
            var fromHierarchy = ControllerHierarchyHelper.GetControllers(resource, sharedConventionData, contextItems);
            var all = specific.Concat(fromHierarchy);
            return all;
        }
    }
}