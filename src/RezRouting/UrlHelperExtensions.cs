using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Routing;
using RezRouting.Utility;
using RezRouting.Utility.Expressions;

namespace RezRouting
{
    /// <summary>
    /// UrlHelper extension methods to generate URLs for resource routes
    /// </summary>
    public static class UrlHelperExtensions
    {
        private static Func<RouteCollection, IEnumerable<ResourceActionRoute>> getResourceRoutes = GetResourceRoutesDefault;

        private static IEnumerable<ResourceActionRoute> GetResourceRoutesDefault(RouteCollection routes)
        {
            return routes.OfType<ResourceActionRoute>();
        }

        private static IEnumerable<ResourceActionRoute> GetResourceRoutes(UrlHelper helper)
        {
            return getResourceRoutes(helper.RouteCollection);
        }

        /// <summary>
        /// Initialises RezRouting's UrlHelperExtensions helper with a custom mechanism for getting the 
        /// available ResourceActionRoutes. Can be used to support use of extensions and profiling tools
        /// like Glimpse, that wrap routes within the application's RouteCollection with proxies.
        /// </summary>
        /// <param name="getRoutes"></param>
        public static void Init(Func<RouteCollection, IEnumerable<ResourceActionRoute>> getRoutes)
        {
            getResourceRoutes = getRoutes ?? GetResourceRoutesDefault;
        }

        /// <summary>
        /// 
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

        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, RouteValueDictionary routeValues)
        {
            var routes = GetResourceRoutes(helper);
            var route = routes.FirstOrDefault(x =>
                controllerType == x.Model.ControllerType
                           && action.EqualsIgnoreCase(x.Model.RouteType.ActionName));
            if(routeValues == null) routeValues = new RouteValueDictionary();
            if (route != null)
            {
                routeValues["httpMethod"] = "GET";
                return helper.RouteUrl(route.Name, routeValues);
            }
            return null;
        }

        public static string ResourceUrl<T>(this UrlHelper helper, Expression<Action<T>> action)
            where T : Controller
        {
            var values = ControllerActionExpressionHelper.GetRouteValuesFromExpression(action);
            string actionName = (string) values["action"];
            return helper.ResourceUrl(typeof (T), actionName, values);
        }
    }
}
