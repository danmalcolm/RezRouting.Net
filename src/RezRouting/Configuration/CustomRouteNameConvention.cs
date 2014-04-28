using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of route name via a function
    /// </summary>
    public class CustomRouteNameConvention : IRouteNameConvention
    {
        private readonly Func<IEnumerable<string>, RouteType, Type, bool, string> create;

        public CustomRouteNameConvention(Func<IEnumerable<string>, RouteType, Type, bool, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            this.create = create;
        }

        public string GetRouteName(IEnumerable<string> resourceNames, RouteType routeType, Type controllerType, bool multiple)
        {
            return create(resourceNames, routeType, controllerType, multiple);
        }
    }
}