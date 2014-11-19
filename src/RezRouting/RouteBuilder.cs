using System;
using System.Collections.Generic;

namespace RezRouting
{
    public class RouteBuilder : IConfigureRoute
    {
        public static Route Create(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string,object> customProperties = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (action == null) throw new ArgumentNullException("action");
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");

            var properties = customProperties != null
                ? new Dictionary<string, object>(customProperties)
                : new Dictionary<string, object>();

            return new Route(name, controllerType, action, httpMethod, path, properties);
        }
    }
}