using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace RezRouting2.AspNetMvc.UrlGeneration
{
    public class RouteIndex
    {
        private static Dictionary<ControllerActionKey, Route> byKey;
        
        public RouteIndex(RouteCollection routes)
        {
            const string modelKey = RouteDataTokenKeys.RouteModel;

            byKey = (from route in routes.OfType<System.Web.Routing.Route>()
                     let model = route.DataTokens[modelKey] as Route
                     where model != null
                     let key = new ControllerActionKey(model.ControllerType, model.Action)
                     group model by key
                         into grouped
                         select grouped)
                .ToDictionary(g => g.Key, g => g.First());
        }

        public Route Get(Type controllerType, string action)
        {
            var key = new ControllerActionKey(controllerType, action);
            Route route;
            byKey.TryGetValue(key, out route);
            return route;
        }

        private struct ControllerActionKey
        {
            private readonly Type controllerType;
            private readonly string action;

            public ControllerActionKey(Type controllerType, string action)
            {
                this.controllerType = controllerType;
                this.action = action;
            }

            public bool Equals(ControllerActionKey other)
            {
                return controllerType.Equals(other.controllerType) && string.Equals(action, other.action, StringComparison.InvariantCultureIgnoreCase);
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
        }
    }
}