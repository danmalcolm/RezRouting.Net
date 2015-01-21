using System;
using System.Web.Mvc;
using RezRouting.Configuration;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// ASP.Net MVC specific resource configuration extensions
    /// </summary>
    public static class ResourceConfiguratorExtensions
    {
        /// <summary>
        /// Adds an ASP.Net MVC controller type to the collection of controllers that 
        /// ASP.Net MVC route conventions can use to determine which routes are supported.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void HandledBy<T>(this IResourceConfigurator resource)
            where T : Controller
        {
            var handler = new MvcController(typeof (T));
            resource.HandledBy(handler);
        }

        /// <summary>
        /// Adds an ASP.Net MVC controller type to the collection of controllers that 
        /// ASP.Net MVC route conventions can use to determine which routes are supported.
        /// </summary>
        /// <param name="controllerType">The type of controller</param>
        public static void HandledBy(this ResourceGraphBuilder resource, Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");
            var handler = new MvcController(controllerType);
            resource.HandledBy(handler);
        }
    }
}