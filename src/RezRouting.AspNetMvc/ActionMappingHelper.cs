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
        private const string ActionsCacheKey = "RezRouting.AspNetMvc.ActionMappingHelper.ControllerActionsCache";

        /// <summary>
        /// Indicates whether an ASP.Net MVC controller supports the specified
        /// action
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="actionName"></param>
        /// <param name="contextItems"></param>
        /// <returns></returns>
        public static bool SupportsAction(Type controllerType, string actionName, Dictionary<string, object> contextItems)
        {
            var actionInfo = GetControllerDescriptor(controllerType, contextItems);
            return actionInfo.SupportsAction(actionName);
        }

        private static ControllerActionInfo GetControllerDescriptor(Type controllerType, Dictionary<string, object> contextItems)
        {
            var cache = contextItems.GetOrAdd(ActionsCacheKey,
                () => new Dictionary<Type, ControllerActionInfo>());
            return cache.GetOrAdd(controllerType, 
                () => ControllerActionInfo.Create(controllerType));
        }

        private class ControllerActionInfo
        {
            public static ControllerActionInfo Create(Type controllerType)
            {
                var descriptor = new ReflectedControllerDescriptor(controllerType);
                return new ControllerActionInfo(descriptor);
            }

            private readonly HashSet<string> actionNames;

            private ControllerActionInfo(ControllerDescriptor descriptor)
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