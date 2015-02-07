using System.Web;
using System.Web.Routing;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    // Based on v. useful tips here: http://samsaffron.com/archive/2011/10/13/optimising-asp-net-mvc3-routing

    /// <summary>
    /// Route implementation used for ASP.Net MVC routes configured by RezRouting, includes 
    /// in-bound route identification optimizations.
    /// </summary>
    public class ResourceRoute : System.Web.Routing.Route
    {
        private readonly string start;

        /// <summary>
        /// Creates a ResourceRoute
        /// </summary>
        /// <param name="url"></param>
        /// <param name="handler"></param>
        public ResourceRoute(string url, IRouteHandler handler) : base(url, handler)
        {
            int index = url.IndexOf('{');
            start = index >= 0 
                ? "~/" + url.Substring(0, index).TrimEnd('/') 
                : null;
        }

        /// <summary>
        /// Optimised implementation that skips the parsing and tokenisation for URLs
        /// that do not begin with the start of the route URL (initial directory segment(s)
        /// before the first ID parameter)
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (start != null && !MatchesStart(httpContext))
            {
                return null;
            }
            return base.GetRouteData(httpContext);
        }

        private bool MatchesStart(HttpContextBase httpContext)
        {
            string path = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            return path.StartsWithIgnoreCase(start);
        }
    }
}