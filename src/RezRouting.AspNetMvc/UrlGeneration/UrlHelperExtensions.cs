﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.Utility;
using RezRouting.Utility;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc.UrlGeneration
{
    /// <summary>
    /// Contains methods to build URLs based on routes created by RezRouting.ASPNetMvc
    /// </summary>
    public static class UrlHelperExtensions
    {
        private static readonly ConcurrentDictionary<RouteCollection, RouteModelIndex> Indexes = new ConcurrentDictionary<RouteCollection, RouteModelIndex>();

        /// <summary>
        /// Stores an index based on the supplied RouteCollection that can be used by 
        /// UrlHelperExtensions for faster route URL generation. This method is designed 
        /// to be called after all routes used by the application have been added to the
        /// collection. This optimisation can be used when there is a guarantee that the
        /// routes in the collection will not change after they have first been set up 
        /// (in practice, there are few scenarios where a RouteCollection is modified
        /// following initialisation at start-up). 
        /// </summary>
        /// <param name="routes"></param>
        public static void IndexRoutes(RouteCollection routes)
        {
            if (routes == null) throw new ArgumentNullException("routes");

            var index = new RouteModelIndex(routes);
            Indexes.TryAdd(routes, index);
        }

        /// <summary>
        /// Resets indexes stored for RouteCollections indexed via IndexRoutes method
        /// </summary>
        public static void ClearIndexedRoutes()
        {
            Indexes.Clear();
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
            IEnumerable<Route> routeModels;
            RouteModelIndex index;
            if (Indexes.TryGetValue(helper.RouteCollection, out index))
            {
                routeModels = index.GetRoutes(controllerType, action);
            }
            else
            {
                const string key = RouteDataTokenKeys.RouteModel;
                routeModels = helper.RouteCollection.OfType<System.Web.Routing.Route>()
                    .Where(route => route.DataTokens != null && route.DataTokens.ContainsKey(key))
                    .Select(r => r.DataTokens[key] as Route)
                    .Where(h => h != null
                        && h.Handler is MvcAction
                        && ((MvcAction)h.Handler).ControllerType == controllerType
                        && ((MvcAction)h.Handler).ActionName.EqualsIgnoreCase(action));
            }

            var routeUrl = routeModels
                .Select(route => helper.RouteUrl(route.FullName, routeValues, protocol, hostName))
                .FirstOrDefault(url => url != null);
            return routeUrl;
        }
    }
}