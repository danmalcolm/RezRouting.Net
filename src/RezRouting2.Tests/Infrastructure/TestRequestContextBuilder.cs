using System.Web;
using System.Web.Routing;

namespace RezRouting2.Tests.Infrastructure
{
    public static class TestRequestContextBuilder
    {
        public static RequestContext Create(HttpContextBase httpContext = null)
        {
            httpContext = httpContext ?? TestHttpContextBuilder.Create("GET", "/");

            var routeData = new RouteData();
            routeData.Values.Add("controller", "test");
            routeData.Values.Add("action", "test");
            var requestContext = new RequestContext(httpContext, routeData);
            return requestContext;
        } 
    }
}