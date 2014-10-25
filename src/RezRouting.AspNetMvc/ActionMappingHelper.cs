using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    internal static class ActionMappingHelper
    {
        private static readonly Dictionary<Type, ControllerInfo> ControllerDescriptors
            = new Dictionary<Type, ControllerInfo>();

        /// <summary>
        /// Indicates whether an ASP.Net MVC controller supports the specified
        /// action
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool SupportsAction(Type controllerType, string actionName)
        {
            var controllerDescriptor = GetControllerDescriptor(controllerType);
            return controllerDescriptor.SupportsAction(actionName);
        }

        private static ControllerInfo GetControllerDescriptor(Type controllerType)
        {
            return ControllerDescriptors.GetOrAdd(controllerType, () =>
                new ControllerInfo(new ReflectedControllerDescriptor(controllerType)));
        }

        /// <summary>
        /// Gets the name from an ActionNameAttribute on a controller action or null
        /// if the attribute is not present
        /// </summary>
        /// <param name="action"></param>
        /// <returns>The name </returns>
        private static string GetActionNameOverride(ActionDescriptor action)
        {
            string name = action.GetCustomAttributes(typeof (ActionNameAttribute), true)
                .OfType<ActionNameAttribute>().Select(x => x.Name).FirstOrDefault();
            return name;
        }

        private class ControllerInfo
        {
            private readonly HashSet<string> actionNames; 

            public ControllerInfo(ControllerDescriptor descriptor)
            {
                var names = descriptor.GetCanonicalActions().Select(
                    action => GetActionNameOverride(action) ?? action.ActionName);
                this.actionNames = new HashSet<string>(names,StringComparer.OrdinalIgnoreCase);
            }

            public bool SupportsAction(string actionName)
            {
                return actionNames.Contains(actionName);
            }
        }
    }
}