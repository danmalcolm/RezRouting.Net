using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;

namespace RezRouting.Tests.Shared
{
    public abstract class RouteBuilderTestContext
    {
        protected RouteBuilderTestContext()
        {
            Routes = new RouteCollection();
            var builder = new RootResourceBuilder();
            Console.Write((string) builder.DebugSummary());
            builder.MapRoutes(Routes);

            Url = new UrlHelper(TestRequestContextBuilder.Create(), Routes);
        }
        

        /// <summary>
        /// The routes under test
        /// </summary>
        protected RouteCollection Routes { get; private set; }

        /// <summary>
        /// Instance of UrlHelper that has been initialised with the routes
        /// under test
        /// </summary>
        protected UrlHelper Url { get; set; }

        protected RouteData RouteMappedTo(string path, string httpMethod, NameValueCollection form = null,
                                         NameValueCollection headers = null)
        {
            var context = TestHttpContextBuilder.Create(path, httpMethod, headers, form);
            var routeData = Routes.GetRouteData(context);
            return routeData;
        }

      

    }
}