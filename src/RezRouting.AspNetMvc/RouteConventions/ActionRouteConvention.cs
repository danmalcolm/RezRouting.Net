using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions
{
    /// <summary>
    /// Creates a route for resource based on a specific action on a controller
    /// </summary>
    public class ActionRouteConvention : IRouteConvention
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

        public IEnumerable<Route> Create(Resource resource, Dictionary<string, object> data, UrlPathSettings urlPathSettings)
        {
            if (resource.Type == Type)
            {
                var controllerTypes = data.GetControllerTypes();
                return from controllerType in controllerTypes
                       where ActionMappingHelper.SupportsAction(controllerType, Action)
                       let handler = new MvcAction(controllerType, Action)
                       let path = urlPathSettings.FormatDirectoryName(Path)
                       select new Route(Name, handler, HttpMethod, path, null);
            }
            return Enumerable.Empty<Route>();
        }
    }
}