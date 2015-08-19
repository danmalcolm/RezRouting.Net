using System;
using System.Collections.Generic;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Extensions used internally to access items in convention data dictionary
    /// </summary>
    internal static class ExtensionDataExtensions
    {
        /// <summary>
        /// Gets controller types registered with the Controller extension methods used
        /// by the public API
        /// </summary>
        /// <param name="conventionData"></param>
        /// <returns></returns>
        public static List<Type> GetControllerTypes(this CustomValueCollection conventionData)
        {
            return conventionData.GetOrAdd(ExtensionDataKeys.ControllerTypes, () => new List<Type>());
        }

        public static void AddControllerTypes(this CustomValueCollection conventionData, IEnumerable<Type> types)
        {
            var data = conventionData.GetControllerTypes();
            data.AddRange(types);
        }

        /// <summary>
        /// Gets data registered with the ControllerHierarchy extension method used by the public API
        /// </summary>
        /// <param name="conventionData"></param>
        /// <returns></returns>
        public static List<ControllerRoot> GetControllerHierarchies(this CustomValueCollection conventionData)
        {
            return conventionData.GetOrAdd(SharedExtensionDataKeys.ControllerHierarchyRoots, () => new List<ControllerRoot>());
        }
    }
}