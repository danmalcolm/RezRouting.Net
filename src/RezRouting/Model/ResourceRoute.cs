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
    /// A route configured for a specific singular or collection resource
    /// </summary>
    public class ResourceRoute
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
            var properties = GetRouteProperties(resourceName, resourceUrl);

            var route = new ResourceActionRoute(properties.Name, properties.Url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(properties.Defaults),
                Constraints = new RouteValueDictionary(properties.Constraints),
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
            var properties = GetRouteProperties(resourceName, resourceUrl);
            summary.Append(properties);
        }

        private RouteProperties GetRouteProperties(string resourceName, string resourceUrl)
        {
            // Name - based on nested resource path and action name
            string name = string.Format("{0}.{1}", resourceName, RouteType.Name);

            // URL - path to resource + any additional path
            string url = resourceUrl;
            if (!string.IsNullOrWhiteSpace(RouteType.UrlPath))
            {
                if (url.Length > 0) url += "/";
                url += RouteType.UrlPath;
            }
            
            string controller = ControllerNameFormatter.TrimControllerFromTypeName(controllerType);
            var defaults = new { controller = controller, action = RouteType.ControllerAction };
            string httpMethod = RouteType.HttpMethod;
            object constraints = new { httpMethod = new HttpMethodOrOverrideConstraint(httpMethod) };
            var namespaces = new[] { controllerType.Namespace };

            return new RouteProperties(name, url, defaults, constraints, namespaces);
        }

        private class RouteProperties
        {
            public RouteProperties(string name, string url, object defaults, object constraints, string[] namespaces)
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
            public object Constraints { get; set; }
            public string[] Namespaces { get; set; }

            public override string ToString()
            {
                return string.Format("Name: {0}, Template: {1}, Defaults: {2}, Constraints: {3}, Namespaces: {4}", Name, Url, Defaults, Constraints, string.Join(", ", Namespaces));
            }
        }
    }
}