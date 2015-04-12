using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using RezRouting.AspNetMvc.Utility;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc.UrlGeneration
{
    /// <summary>
    /// Indexes RezRouting routes by controller type and action. Used internally to optimise URL generation.
    /// </summary>
    public class RouteModelIndex
    {
        private readonly ConcurrentDictionary<RouteCollection, RouteCollectionIndex> indexes
            = new ConcurrentDictionary<RouteCollection, RouteCollectionIndex>();

        /// <summary>
        /// Indexes routes within the supplied RouteCollection. If the RouteCollection has
        /// already been indexed, the index will be replaced, meaning that this can be called
        /// multiple times.
        /// </summary>
        /// <param name="routes"></param>
        public void AddRoutes(RouteCollection routes)
        {
            if (routes == null) throw new ArgumentNullException("routes");

            var index = new RouteCollectionIndex(routes);
            indexes.AddOrUpdate(routes, r => index, (r, e) => index);
        }

        /// <summary>
        /// Removes any indexed routes
        /// </summary>
        public void Clear()
        {
            indexes.Clear();
        }
        
        /// <summary>
        /// Gets the RezRouting routes in a specific route collection matching the specified 
        /// controller type and action
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEnumerable<Route> GetRoutes(RouteCollection routes, Type controllerType, string action)
        {
            RouteCollectionIndex index;
            if (indexes.TryGetValue(routes, out index))
            {
                var key = new ControllerActionKey(controllerType, action);
                return index.GetRoutes(key);
            }
            return Enumerable.Empty<Route>();
        }

        private class RouteCollectionIndex
        {
            private readonly Dictionary<ControllerActionKey, List<Route>> routesByKey;

            public RouteCollectionIndex(RouteCollection routes)
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
                    .ToDictionary(g => g.Key, g => g.ToList());
            }

            public IEnumerable<Route> GetRoutes(ControllerActionKey key)
            {
                List<Route> routes;
                routesByKey.TryGetValue(key, out routes);
                return routes ?? Enumerable.Empty<Route>();
            }
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
                return obj is ControllerActionKey && Equals((ControllerActionKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (controllerType.GetHashCode() * 397) ^ action.GetHashCode();
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