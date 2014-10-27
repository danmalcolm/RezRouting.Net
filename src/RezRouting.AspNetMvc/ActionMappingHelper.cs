using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    internal static class ActionMappingHelper
    {
        private static readonly Dictionary<Type, ControllerActions> ControllerDescriptors
            = new Dictionary<Type, ControllerActions>();

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

        private static ControllerActions GetControllerDescriptor(Type controllerType)
        {
            return ControllerDescriptors.GetOrAdd(controllerType, 
                () => ControllerActions.Create(controllerType));
        }

        private class ControllerActions
        {
            public static ControllerActions Create(Type controllerType)
            {
                var descriptor = new ReflectedControllerDescriptor(controllerType);
                return new ControllerActions(descriptor);
            }

            private readonly HashSet<string> actionNames;

            private ControllerActions(ControllerDescriptor descriptor)
            {
                var names = from action in descriptor.GetCanonicalActions()
                            let actionNameAttribute = action.GetCustomAttributes(typeof(ActionNameAttribute), true)
                                .Cast<ActionNameAttribute>().FirstOrDefault()
                            select actionNameAttribute != null ? actionNameAttribute.Name : action.ActionName;
                actionNames = new HashSet<string>(names, StringComparer.OrdinalIgnoreCase);
            }

            public bool SupportsAction(string actionName)
            {
                return actionNames.Contains(actionName, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}