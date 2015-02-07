using System;

namespace RezRouting.AspNetMvc.Utility
{
    internal static class RouteValueHelper
    {
        public static string TrimControllerFromTypeName(Type controllerType)
        {
            string value = controllerType.Name;
            if (value.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.Substring(0, value.Length - 10);
            }
            return value;
        }
    }
}