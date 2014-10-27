using System.Globalization;
using System.Web.Routing;

namespace RezRouting.AspNetMvc
{
    // Based on v. useful tips here: http://samsaffron.com/archive/2011/10/13/optimising-asp-net-mvc3-routing

    /// <summary>
    /// Route implementation for resource routes, containing optimisation based on
    /// resource route structure - this prevents the parsing and tokenisation of routes
    /// </summary>
    public class ResourceRoute : System.Web.Routing.Route
    {
        private readonly string neededOnTheLeft;

        public ResourceRoute(string url, IRouteHandler handler) : base(url, handler)
        {
            int index = url.IndexOf('{');
            neededOnTheLeft = "~/" + (index >= 0 ? url.Substring(0, index) : url).TrimEnd('/');
        }

        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            if (!httpContext.Request.AppRelativeCurrentExecutionFilePath.StartsWith(neededOnTheLeft, true, CultureInfo.InvariantCulture)) return null;
            return base.GetRouteData(httpContext);
        }
    }
}