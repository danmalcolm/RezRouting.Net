using System;
using System.Web.Routing;

namespace RezRouting.Utility
{
    public class ControllerActionInfo
    {
        public ControllerActionInfo(Type controllerType, string action, RouteValueDictionary additionalRouteValues)
        {
            ControllerType = controllerType;
            Action = action;
            AdditionalRouteValues = additionalRouteValues;
        }

        public Type ControllerType { get; private set; }

        public string Action { get; private set; }

        public RouteValueDictionary AdditionalRouteValues { get; private set; }
    }
}