using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Routing;

namespace RezRouting.Model
{
    /// <summary>
    /// A route configured for an action on a resource. An intermediate model created by 
    /// ResourceBuilder classes during route mapping.
    /// </summary>
    internal class ResourceRoute
    {
        public string RouteName { get; private set; }
        public RouteType RouteType { get; private set; }
        private readonly Type controllerType;
        private readonly CustomRouteSettings settings;

        public ResourceRoute(string routeName, RouteType routeType, Type controllerType, CustomRouteSettings settings)
        {
            RouteName = routeName;
            RouteType = routeType;
            this.controllerType = controllerType;
            this.settings = settings;
        }

        public void MapRoute(string resourceName, string resourceUrl, RouteCollection routes)
        {
            var properties = GetRouteInfo(resourceUrl);

            var route = new ResourceActionRoute(properties.Name, properties.Url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(properties.Defaults),
                Constraints = properties.Constraints,
                DataTokens = new RouteValueDictionary(),
            };

            if (properties.Namespaces.Any())
            {
                route.DataTokens["Namespaces"] = properties.Namespaces;
                route.DataTokens["UseNamespaceFallback"] = false;
            }

            routes.Add(properties.Name, route);
        }

        public void DebugSummary(string resourceName, string resourceUrl, StringBuilder summary)
        {
            var properties = GetRouteInfo(resourceUrl);
            summary.Append(properties);
        }

        private RouteInfo GetRouteInfo(string resourceUrl)
        {
            // URL - path to resource + additional path segment(s) for route
            string url = resourceUrl;

            if (!string.IsNullOrWhiteSpace(settings.PathSegment))
            {
                if (url.Length > 0) url += "/";
                url += settings.PathSegment;
            }
            
            string controller = RouteValueHelper.TrimControllerFromTypeName(controllerType);
            var defaults = new { controller = controller, action = RouteType.ActionName };
            var constraints = GetConstraints();
            var namespaces = new[] { controllerType.Namespace };

            return new RouteInfo(RouteName, url, defaults, constraints, namespaces);
        }

        private RouteValueDictionary GetConstraints()
        {
            string httpMethod = RouteType.HttpMethod;
            var constraints = new RouteValueDictionary();
            constraints["httpMethod"] = new HttpMethodOrOverrideConstraint(httpMethod);
            var queryStringValues = settings.QueryStringValues;
            foreach (string key in queryStringValues.Keys)
            {
                string value = queryStringValues[key].ToString();
                // Route values whose key matches the key of a constraint are excluded 
                // from the query string during URL generation. Here, we specifically want
                // to include the value in the query string, so we use an alternative
                // key in the constraint dictionary. The constraint itself has the real key.
                string constraintKey = "__requestvalueconstraint__" + key;
                constraints[constraintKey] = new QueryStringValueConstraint(key, value);
            }
            return constraints;
        }

        private class RouteInfo
        {
            public RouteInfo(string name, string url, object defaults, RouteValueDictionary constraints, string[] namespaces)
            {
                Name = name;
                Url = url;
                Defaults = defaults;
                Constraints = constraints;
                Namespaces = namespaces;
            }

            public string Name { get; private set; }
            public string Url { get; private set; }
            public object Defaults { get; private set; }
            public RouteValueDictionary Constraints { get; private set; }
            public string[] Namespaces { get; private set; }

            public override string ToString()
            {
                return string.Format("Name: {0}, Template: {1}, Defaults: {2}, Constraints: {3}, Namespaces: {4}", Name, Url, Defaults, Constraints, string.Join(", ", Namespaces));
            }
        }
    }
}