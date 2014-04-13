using System;

namespace RezRouting.Routing
{
    internal class ControllerNameFormatter
    {
        public static string TrimControllerFromTypeName(Type controllerType)
        {
            string value = controllerType.Name;
            int index = value.LastIndexOf("Controller", StringComparison.InvariantCultureIgnoreCase);
            if (index != -1)
            {
                value = value.Substring(0, index);
            }
            return value;
        }
    }
}