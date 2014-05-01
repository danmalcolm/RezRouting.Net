using System;
using System.Collections.Generic;
using System.Text;
using RezRouting.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Default implementation. Formats route name using pattern "Parent.Child.Route
    /// </summary>
    public class DefaultRouteNameConvention : IRouteNameConvention
    {
        public virtual string GetRouteName(IEnumerable<string> resourceNames, string routeTypeName, Type controllerType, bool includeController)
        {
            var name = new StringBuilder();
            name.Append(string.Join(".", resourceNames));
            if(includeController)
            {
                name.Append(".");
                var controllerName = RouteValueHelper.TrimControllerFromTypeName(controllerType);
                name.Append(controllerName);
            }
            name.Append(".");
            name.Append(routeTypeName);
            return name.ToString();
        }
    }
}