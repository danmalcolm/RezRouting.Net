using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting2.Utility;

namespace RezRouting2.AspNetMvc.UrlGeneration
{
    public static class UrlHelperExtensions
    {
        private static RouteIndex index;
        
        public static void EnableOptimisations(this UrlHelper helper)
        {
            index = new RouteIndex(helper.RouteCollection);
        }

        public static string ResourceUrl(this UrlHelper helper, Type controllerType, string action, object routeValues = null)
        {
            const string modelKey = RouteDataTokenKeys.RouteModel;

            Route route;
            if (index != null)
            {
                route = index.Get(controllerType, action);
            }
            else
            {
                route = helper.RouteCollection.OfType<System.Web.Routing.Route>()
                    .Where(r => r.DataTokens.ContainsKey(modelKey))
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