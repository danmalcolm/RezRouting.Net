using System.Web;
using System.Web.Routing;

namespace RezRouting.Routing
{
    /// <summary>
    /// Route implementation for routes mapped by RezRouting. Includes properties to assist with route
    /// identification.
    /// </summary>
    public class ResourceActionRoute : Route
    {
        public ResourceActionRoute(string name, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            Name = name;
        }

        public ResourceActionRoute(string name, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Controller: {1}, Action: {2}", Name, Defaults["controller"], Defaults["action"]);
        }
    }
}
