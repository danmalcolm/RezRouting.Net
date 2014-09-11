using System.Linq;
using System.Web.Mvc;
using RezRouting2.Utility;

namespace RezRouting2.AspNetMvc.RouteTypes
{
    public class MvcRouteTypeHelper 
    {
        public static RouteType ActionRouteType(string name, ResourceLevel level, string action, string httpMethod, string path)
        {
            return new RouteType(name, (resource, handlerType, route) =>
            {
                if (resource.Level == level)
                {
                    var controllerDescriptor = new ReflectedControllerDescriptor(handlerType);
                    var actions = controllerDescriptor.GetCanonicalActions();
                    if (actions.Any(x => x.ActionName.EqualsIgnoreCase(action)))
                    {
                        route.Configure(name, action, httpMethod, path);
                    }
                }
            });
        }
    }
}