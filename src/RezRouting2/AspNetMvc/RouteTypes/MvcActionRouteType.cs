using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting2.Utility;

namespace RezRouting2.AspNetMvc.RouteTypes
{
    public class MvcActionRouteType : IRouteType
    {
        public MvcActionRouteType(string name, ResourceLevel level, string action, string httpMethod, string path)
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
                var supported = SupportsAction(handlerType);
                if (supported)
                {
                    var builder = new RouteBuilder(handlerType);
                    builder.Configure(Name, Action, HttpMethod, Path);
                    return builder.Build();
                }
            }
            return null;
        }

        private bool SupportsAction(Type handlerType)
        {
            var controllerDescriptor = new ReflectedControllerDescriptor(handlerType);
            var actions = controllerDescriptor.GetCanonicalActions();
            var supportsAction = actions.Any(x => StringExtensions.EqualsIgnoreCase(x.ActionName, Action));
            return supportsAction;
        }
    }
}