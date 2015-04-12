using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RezRouting.AspNetMvc.UrlGeneration
{
    /// <summary>
    /// Contains methods to build URLs based on routes created by RezRouting.ASPNetMvc
    /// </summary>
    public static class UrlHelperExtensions
    {
        static UrlHelperExtensions()
        {
            Index = new RouteModelIndex();
        }

        /// <summary>
        /// A shared RouteModelIndex instance used by the resource URL generation methods
        /// </summary>
        public static RouteModelIndex Index { get; private set; }
        
        /// <summary>
        /// Generates a fully qualified URL for a resource route based on the specified 
        /// controller type and action. Only routes created by RezRouting are supported.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ResourceUrl<TController>(this UrlHelper helper, string action)
            where TController : Controller
        {
            return helper.ResourceUrl(typeof(TController), action, null);
        }

        /// <summary>
        /// Generates a fully qualified URL for a resource route based on the specified 
        /// controller type, action and route values. Only routes created by RezRouting
        /// are supported.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action)
        {
            return helper.ResourceUrl(controllerType, action, null);
        }

        /// <summary>
        /// Generates a fully qualified URL for a resource route based on the specified 
        /// controller type, action and route values. Only routes created by RezRouting
        /// are supported.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string ResourceUrl<TController>(this UrlHelper helper, string action, object routeValues)
            where TController : Controller
        {
            return helper.ResourceUrl(typeof(TController), action, routeValues);
        }

        /// <summary>
        /// Generates a fully qualified URL for a resource route based on the specified 
        /// controller type, action and route values. Only routes created by RezRouting
        /// are supported.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, object routeValues)
        {
            var rvd = routeValues != null ? new RouteValueDictionary(routeValues) : null;
            return helper.ResourceUrl(controllerType, action, rvd);
        }

        /// <summary>
        /// Generates a fully qualified URL for a resource route based on the specified 
        /// controller type, action, route values, protocol and host name. Only routes 
        /// added to the RouteCollection by RezRouting are supported.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <param name="protocol"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, RouteValueDictionary routeValues, string protocol = null, string hostName = null)
        {
            var routeModels = Index.GetRoutes(helper.RouteCollection, controllerType, action);

            var routeUrl = routeModels
                .Select(route => helper.RouteUrl(route.FullName, routeValues, protocol, hostName))
                .FirstOrDefault(url => url != null);
            return routeUrl;
        }
    }
}