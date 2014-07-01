using System.Web.Routing;
using RezRouting.Model;

namespace RezRouting.Routing
{
    /// <summary>
    /// Route implementation for routes mapped by RezRouting. Includes properties to assist with route
    /// identification and URL generation.
    /// </summary>
    public class ResourceActionRoute : Route
    {
        public ResourceActionRoute(Model.ResourceRoute model, string name, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            Model = model;
            Name = name;
        }

        /// <summary>
        /// The name of this route
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A model containing information about the resource action on which this route is based
        /// </summary>
        public ResourceRoute Model { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Controller: {1}, Action: {2}", Name, Defaults["controller"], Defaults["action"]);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var virtualPathData = base.GetVirtualPath(requestContext, values);
            return virtualPathData;
        }
    }
}
