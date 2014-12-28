using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

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

        public IEnumerable<Route> Create(Resource resource, IEnumerable<IResourceHandler> handlers, UrlPathFormatter pathFormatter)
        {
            if (resource.Level == Level)
            {
                return from controller in handlers.OfType<MvcController>()
                       let controllerType = controller.ControllerType
                       where ActionMappingHelper.SupportsAction(controllerType, Action)
                       let handler = new MvcAction(controllerType, Action)
                       let path = pathFormatter.FormatDirectoryName(Path)
                       select new Route(Name, handler, HttpMethod, path, null);
            }
            return Enumerable.Empty<Route>();
        }
    }
}