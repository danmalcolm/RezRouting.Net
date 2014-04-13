using System.Linq;
using System.Web.Mvc;

namespace RezRouting.Utility
{
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        /// Gets the name specified on a controller action if defined via the 
        /// ActionNameAttribute attribute 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string GetActionNameOverride(this ActionDescriptor action)
        {
            string name = action.GetCustomAttributes(typeof (ActionNameAttribute), true)
                .Cast<ActionNameAttribute>().Select(x => x.Name).FirstOrDefault();
            return name;
        } 
    }
}