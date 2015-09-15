using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration.Builders;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.ControllerDiscovery
{
    public static class ControllerHierarchyHelper
    {
        private static readonly string IndexKey =
            typeof (ControllerHierarchyHelper).FullName + "." + "ControllerHierarchyIndex";

        public static IEnumerable<Type> GetControllers(ResourceData resource, CustomValueCollection conventionData, CustomValueCollection contextItems)
        {
            // Scan assemblies once and cache within contextItems for reuse
            var index = contextItems.GetOrAdd(IndexKey, () => IndexControllers(conventionData));
            return from item in index.Items
                where item.Key.StartsWith(resource.FullName)
                // Any controller in the current or child namespace is included, but any that
                // are linked to child resources are excluded
                where !resource.Children.Any(child => item.Key.StartsWith(child.FullName))
                select item.Type;
        }

        public static ControllerIndex IndexControllers(CustomValueCollection conventionData)
        {
            var roots = conventionData.GetControllerHierarchies();
            var index = ControllerIndex.Create(roots);
            return index;
        }
    }
}