using System.Linq;
using System.Web.Mvc;

namespace RezRouting2.AspNetMvc.RouteTypes
{
    internal static class ActionDescriptorExtensions
    {
        /// <summary>
        /// Gets the name from an ActionNameAttribute on a controller action or null
        /// if the attribute is not present
        /// </summary>
        /// <param name="action"></param>
        /// <returns>The name </returns>
        public static string GetNameOverride(this ActionDescriptor action)
        {
            string name = action.GetCustomAttributes(typeof (ActionNameAttribute), true)
                .OfType<ActionNameAttribute>().Select(x => x.Name).FirstOrDefault();
            return name;
        } 
    }
}