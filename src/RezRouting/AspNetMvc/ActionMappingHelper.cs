using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    public static class ActionMappingHelper
    {
        public static bool IncludesAction(Type controllerType, string action)
        {
            var controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
            var actions = controllerDescriptor.GetCanonicalActions();
            var supportsAction = actions.Any(x => StringExtensions.EqualsIgnoreCase(x.ActionName, action));
            return supportsAction;
        }
    }
}