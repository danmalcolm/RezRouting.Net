using System.Web.Routing;

namespace RezRouting2.AspNetMvc
{
    /// <summary>
    /// RezRouting Route implementation 
    /// </summary>
    public class ResourceRoute : System.Web.Routing.Route
    {
        public ResourceRoute(string name, string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}