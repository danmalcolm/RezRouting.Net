using System;
using System.Collections.Generic;
using System.Text;
using RezRouting.Routing;

namespace RezRouting.Configuration
{
    public class DefaultRouteNameConvention : IRouteNameConvention
    {
        public virtual string GetRouteName(IEnumerable<string> resourceNames, RouteType routeType, Type controllerType, bool multiple)
        {
            var name = new StringBuilder();
            name.Append(string.Join(".", resourceNames));
            if(multiple)
            {
                name.Append(".");
                var controllerName = ControllerNameFormatter.TrimControllerFromTypeName(controllerType);
                name.Append(controllerName);
            }
            name.Append(".");
            name.Append(routeType.Name);
            return name.ToString();
        }
    }
}