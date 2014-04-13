using System.Web.Routing;

namespace RezRouting.Utility
{
    internal static class RouteValueDictionaryExtensions
    {
        public static string GetController(this RouteValueDictionary values)
        {
            return values["controller"] as string;
        }

        public static string GetAction(this RouteValueDictionary values)
        {
            return values["action"] as string;
        }
    }
}