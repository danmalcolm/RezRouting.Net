using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    internal static class ActionMappingHelper
    {
        /// <summary>
        /// Indicates whether an ASP.Net MVC controller supports the specified
        /// action
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool SupportsAction(Type controllerType, string actionName)
        {
            var controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
            var actions = controllerDescriptor.GetCanonicalActions();
            var supportsAction = actions.Any(action =>
            {
                string name = GetActionNameOverride(action) ?? action.ActionName;
                return name.EqualsIgnoreCase(actionName);
            });
            return supportsAction;
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
    }
}