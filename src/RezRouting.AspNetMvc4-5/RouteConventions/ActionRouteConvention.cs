using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Creates a route for resource based on a specific action on a controller
    /// </summary>
    public class ActionRouteConvention : MvcRouteConvention
    {
        public ActionRouteConvention(string name, ResourceType type, string action, string httpMethod, string path)
        {
            Name = name;
            Type = type;
            Action = action;
            HttpMethod = httpMethod;
            Path = path;
        }

        public string Name { get; set; }

        public ResourceType Type { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }

        protected override IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options,
            IEnumerable<Type> controllerTypes)
        {
            if (resource.Type == Type)
            {
                var urlPathSettings = options.UrlPathSettings;
                return from controllerType in controllerTypes
                       where ActionMappingHelper.SupportsAction(controllerType, Action, context.Cache)
                       let handler = new MvcAction(controllerType, Action)
                       let path = urlPathSettings.FormatDirectoryName(Path)
                       select new RouteData(Name, HttpMethod, path, handler, null);
            }
            return Enumerable.Empty<RouteData>();
        }
    }
}