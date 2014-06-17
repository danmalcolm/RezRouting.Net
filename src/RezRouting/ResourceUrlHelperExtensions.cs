using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
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
    public static class ResourceUrlHelperExtensions
    {
        public static string ResourceUrl(this UrlHelper helper)
        {
            return null;
        }

        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, object routeValues)
        {
            return helper.ResourceUrl(controllerType, action, new RouteValueDictionary(routeValues));
        }

        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, RouteValueDictionary routeValues)
        {
            var routes = GetResourceRoutes(helper);
            var route = routes.FirstOrDefault(x =>
                controllerType == x.Model.ControllerType
                           && action.EqualsIgnoreCase(x.Model.RouteType.ActionName));
            if (route != null)
            {
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
        
        private static IEnumerable<ResourceActionRoute> GetResourceRoutes(UrlHelper helper)
        {
            return helper.RouteCollection.OfType<ResourceActionRoute>();
        }
    }
}