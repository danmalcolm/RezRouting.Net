using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Creates a route for resource based on a specific action on a controller
    /// </summary>
    public class ActionRouteConvention : IRouteConvention
    {
        public ActionRouteConvention(string name, ResourceLevel level, string action, string httpMethod, string path)
        {
            Name = name;
            Level = level;
            Action = action;
            HttpMethod = httpMethod;
            Path = path;
        }

        public string Name { get; set; }

        public ResourceLevel Level { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }

        public string Path { get; set; }
        
        public IEnumerable<Route> Create(Resource resource, IEnumerable<Type> controllerTypes, UrlPathFormatter pathFormatter)
        {
            if (resource.Level == Level)
            {
                return from controllerType in controllerTypes
                    where ActionMappingHelper.SupportsAction(controllerType, Action)
                    let path = pathFormatter.FormatDirectoryName(Path)
                    select new Route(Name, controllerType, Action, HttpMethod, path, null);
            }
            return Enumerable.Empty<Route>();
        }
    }
}