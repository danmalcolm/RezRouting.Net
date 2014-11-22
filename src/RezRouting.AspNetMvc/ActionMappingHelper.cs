using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Helper class for mapping controller actions
    /// </summary>
    public static class ActionMappingHelper
    {
        private static readonly Dictionary<Type, ControllerActions> ControllerActionsCache
            = new Dictionary<Type, ControllerActions>();

        /// <summary>
        /// Clears any data stored while inspecting controller types
        /// </summary>
        public static void ResetCache()
        {
            ControllerActionsCache.Clear();
        }

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
            return ControllerActionsCache.GetOrAdd(controllerType, 
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