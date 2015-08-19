using System.Reflection;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration;

namespace RezRouting.AspNetMvc
{
    static internal class RootResourceBuilderExtensions
    {
        /// <summary>
        /// Adds assembly and root namespace containing controller classes that 
        /// handle the current resource hierarchy's routes. Any controllers within
        /// a namespace that matches the full resource name will be used by the MVC 
        /// route conventions, as if they had been added individually using the Controller
        /// extension method.
        /// </summary>
        public static void ControllerHierarchy(this IRootResourceBuilder builder,
            Assembly assembly, string rootNamespace)
        {
            var root = new ControllerRoot(assembly, rootNamespace);
            builder.SharedExtensionData(data => data.GetControllerHierarchies().Add(root));
        }
    }
}