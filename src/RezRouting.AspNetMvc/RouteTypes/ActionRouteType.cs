using System;
using RezRouting.Options;

namespace RezRouting.AspNetMvc.RouteTypes
{
    /// <summary>
    /// Creates a route for a specific action on the controller
    /// </summary>
    public class ActionRouteType : IRouteType
    {
        public ActionRouteType(string name, ResourceLevel level, string action, string httpMethod, string path)
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
        
        public Route BuildRoute(Resource resource, Type handlerType, UrlPathFormatter pathFormatter)
        {
            if (resource.Level == Level)
            {
                var supported = ActionMappingHelper.IncludesAction(handlerType, Action);
                if (supported)
                {
                    var path = pathFormatter.FormatDirectoryName(Path);
                    var builder = new RouteBuilder(handlerType);
                    builder.Configure(Name, Action, HttpMethod, path);
                    return builder.Build();
                }
            }
            return null;
        }
    }
}