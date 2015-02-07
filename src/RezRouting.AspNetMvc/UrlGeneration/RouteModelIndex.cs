using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using RezRouting.AspNetMvc.Utility;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc.UrlGeneration
{
    /// <summary>
    /// Stores a collection of routes, indexed by controller type and action for
    /// rapid retrieval during URL generation. Only routes created by RezRouting
    /// are indexed.
    /// </summary>
    public class RouteModelIndex
    {
        private readonly Dictionary<ControllerActionKey, Route> routesByKey;
        
        /// <summary>
        /// Creates a new RouteModelIndex
        /// </summary>
        /// <param name="routes"></param>
        public RouteModelIndex(RouteCollection routes)
        {
            const string modelKey = RouteDataTokenKeys.RouteModel;

            routesByKey = (from route in routes.OfType<System.Web.Routing.Route>()
                     let model = route.DataTokens != null ? route.DataTokens[modelKey] as Route : null
                     where model != null
                     let handler = model.Handler as MvcAction
                     where handler != null
                     let key = new ControllerActionKey(handler.ControllerType, handler.ActionName)
                     group model by key
                         into grouped
                         select grouped)
                .ToDictionary(g => g.Key, g => g.First());
        }

        /// <summary>
        /// Gets the RezRouting route for the specified controller type and action
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Route Get(Type controllerType, string action)
        {
            var key = new ControllerActionKey(controllerType, action);
            Route route;
            routesByKey.TryGetValue(key, out route);
            return route;
        }

        private struct ControllerActionKey
        {
            private readonly Type controllerType;
            private readonly string action;

            public ControllerActionKey(Type controllerType, string action)
            {
                this.controllerType = controllerType;
                this.action = action.ToLowerInvariant();
            }

            private bool Equals(ControllerActionKey other)
            {
                return controllerType == other.controllerType
                    && string.Equals(action, other.action);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is ControllerActionKey && Equals((ControllerActionKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (controllerType.GetHashCode()*397) ^ action.GetHashCode();
                }
            }

            public static bool operator ==(ControllerActionKey left, ControllerActionKey right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(ControllerActionKey left, ControllerActionKey right)
            {
                return !left.Equals(right);
            }

            public override string ToString()
            {
                return string.Format("ControllerType: {0}, Action: {1}", controllerType, action);
            }
        }
    }
}