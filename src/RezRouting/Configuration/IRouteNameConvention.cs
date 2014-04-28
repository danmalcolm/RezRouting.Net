using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Formats the name of a route
    /// </summary>
    public interface IRouteNameConvention
    {
        string GetRouteName(IEnumerable<string> resourceNames, RouteType routeType, Type controllerType, bool multiple);
    }
}
