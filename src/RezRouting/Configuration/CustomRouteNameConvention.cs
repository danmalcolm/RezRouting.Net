using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of route name via a function
    /// </summary>
    internal class CustomRouteNameConvention : IRouteNameConvention
    {
        private readonly Func<IEnumerable<string>, string, Type, bool, string> create;

        public CustomRouteNameConvention(Func<IEnumerable<string>, string, Type, bool, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            this.create = create;
        }

        public string GetRouteName(IEnumerable<string> resourceNames, string routeTypeName, Type controllerType, bool includeController)
        {
            return create(resourceNames, routeTypeName, controllerType, includeController);
        }
    }
}