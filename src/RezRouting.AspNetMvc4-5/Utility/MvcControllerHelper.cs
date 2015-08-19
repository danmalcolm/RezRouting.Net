using System;
using System.Web.Mvc;

namespace RezRouting.AspNetMvc.Utility
{
    public static class MvcControllerHelper
    {
        /// <summary>
        /// Indicates whether the type is a concrete class inheriting
        /// from Controller class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsController(Type type)
        {
            return typeof (Controller).IsAssignableFrom(type)
                && type.IsClass && !type.IsAbstract;
        }
    }
}