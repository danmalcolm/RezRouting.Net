using System;
using System.Reflection;
using System.Web.Mvc;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// ASP.Net MVC specific resource configuration extensions
    /// </summary>
    public static class ResourceConfiguratorExtensions
    {
        /// <summary>
        /// Adds an ASP.Net MVC controller type to a collection of controllers stored
        /// in this resource's convention data, so that ASP.Net MVC route conventions 
        /// can determine which routes are supported.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Controller<T>(this IResourceConfigurator resource)
            where T : Controller
        {
            resource.Controller(typeof(T));
        }

        /// <summary>
        /// Adds an ASP.Net MVC controller type to a collection of controllers stored
        /// in this resource's convention data, so that ASP.Net MVC route conventions 
        /// can determine which routes are supported.
        /// </summary>
        /// <param name="controllerType">The type of controller</param>
        public static void Controller(this IResourceConfigurator resource, Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");
            resource.ExtensionData(data =>
            {
                var controllerTypes = data.GetControllerTypes();
                controllerTypes.Add(controllerType);
            });
        }
    }
}