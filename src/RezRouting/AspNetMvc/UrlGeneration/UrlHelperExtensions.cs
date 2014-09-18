using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc.UrlGeneration
{
    public static class UrlHelperExtensions
    {
        private static readonly ConcurrentDictionary<RouteCollection, RouteModelIndex> Indexes = new ConcurrentDictionary<RouteCollection, RouteModelIndex>();
        
        /// <summary>
        /// Stores an index based on the supplied RouteCollection to enable quicker route URL generation. 
        /// This method is designed to be called during application startup after all routes used by 
        /// the application have been added to the collection. This optimisation can be used when there
        /// is a guarantee that the routes in the collection will not change. 
        /// </summary>
        /// <param name="routes"></param>
        public static void IndexRoutes(RouteCollection routes)
        {
            var index = new RouteModelIndex(routes);
            Indexes.TryAdd(routes, index);
        }
        
        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, object routeValues)
        {
            var rvd = routeValues != null ? new RouteValueDictionary(routeValues) : null;
            return helper.ResourceUrl(controllerType, action, rvd);
        }

        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, RouteValueDictionary routeValues)
        {
            const string modelKey = RouteDataTokenKeys.RouteModel;

            Route route;
            RouteModelIndex index;
            if (Indexes.TryGetValue(helper.RouteCollection, out index))
            {
                route = index.Get(controllerType, action);
            }
            else
            {
                route = helper.RouteCollection.OfType<System.Web.Routing.Route>()
                    .Where(r => r.DataTokens != null && r.DataTokens.ContainsKey(modelKey))
                    .Select(r => r.DataTokens[modelKey] as Route)
                    .FirstOrDefault(r => r != null
                        && r.ControllerType == controllerType
                        && r.Action.EqualsIgnoreCase(action));
            }

            if (route != null)
            {
                return helper.RouteUrl(route.FullName, routeValues);
            }
            return null;
        }
    }
}