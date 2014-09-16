using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Formats the name of a route
    /// </summary>
    public interface IRouteNameConvention
    {
        string GetRouteName(IEnumerable<string> resourceNames, string routeTypeName, Type controllerType, bool includeController);
    }
}
