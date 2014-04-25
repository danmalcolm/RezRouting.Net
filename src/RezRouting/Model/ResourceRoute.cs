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
    /// A route configured for a an action on a singular or collection resource
    /// </summary>
    internal class ResourceRoute
    {
        public RouteType RouteType { get; private set; }
        private readonly Type controllerType;
        
        public ResourceRoute(RouteType routeType, Type controllerType)
        {
            RouteType = routeType;
            this.controllerType = controllerType;
        }

        public void MapRoute(string resourceName, string resourceUrl, RouteCollection routes)
        {
            var properties = GetRouteInfo(resourceName, resourceUrl);

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
            var properties = GetRouteInfo(resourceName, resourceUrl);
            summary.Append(properties);
        }

        private RouteInfo GetRouteInfo(string resourceName, string resourceUrl)
        {
            // Name - based on nested resource path and action name
            string name = string.Format("{0}.{1}", resourceName, RouteType.Name);

            // URL - path to resource + additional path segment(s) for route
            string url = resourceUrl;
            if (!string.IsNullOrWhiteSpace(RouteType.UrlPath))
            {
                if (url.Length > 0) url += "/";
                url += RouteType.UrlPath;
            }
            
            string controller = ControllerNameFormatter.TrimControllerFromTypeName(controllerType);
            var defaults = new { controller = controller, action = RouteType.ControllerAction };
            var constraints = GetConstraints();
            var namespaces = new[] { controllerType.Namespace };

            return new RouteInfo(name, url, defaults, constraints, namespaces);
        }

        private RouteValueDictionary GetConstraints()
        {
            string httpMethod = RouteType.HttpMethod;
            var constraints = new RouteValueDictionary();
            constraints["httpMethod"] = new HttpMethodOrOverrideConstraint(httpMethod);
            foreach (string key in RouteType.RequestValues.Keys)
            {
                string value = RouteType.RequestValues[key].ToString();
                // Route values with key matching key of a constraint are 
                // excluded from the QueryString during URL generation. In this case, we want 
                // to include the value, as it needed part of the route URL. We work around
                // this by formatting an alternative key name
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

            public string Name { get; set; }
            public string Url { get; set; }
            public object Defaults { get; set; }
            public RouteValueDictionary Constraints { get; set; }
            public string[] Namespaces { get; set; }

            public override string ToString()
            {
                return string.Format("Name: {0}, Template: {1}, Defaults: {2}, Constraints: {3}, Namespaces: {4}", Name, Url, Defaults, Constraints, string.Join(", ", Namespaces));
            }
        }
    }
}