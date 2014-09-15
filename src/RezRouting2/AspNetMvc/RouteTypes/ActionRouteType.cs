using System;

namespace RezRouting2.AspNetMvc.RouteTypes
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
        
        public Route BuildRoute(Resource resource, Type handlerType)
        {
            if (resource.Level == Level)
            {
                var supported = ActionMappingHelper.IncludesAction(handlerType, Action);
                if (supported)
                {
                    var builder = new RouteBuilder(handlerType);
                    builder.Configure(Name, Action, HttpMethod, Path);
                    return builder.Build();
                }
            }
            return null;
        }
    }
}