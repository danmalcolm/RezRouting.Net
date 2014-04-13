using System;
using System.Text;
using System.Web;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.Utility;
using RezRouting.Routing;

namespace RezRouting.Tests.Infrastructure.Expectations
{
    /// <summary>
    /// Defines expected outcome when matching route to an incoming request
    /// </summary>
    public class MappingExpectation
    {
        public static MappingExpectation Match(RouteCollection routes, HttpContextBase httpContext, string routeName, string controllerAction, object otherRouteValues, string desc)
        {
            var actionParts = controllerAction.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            string controller = actionParts[0];
            string action = actionParts[1];
            return new MappingExpectation
            {
                Routes = routes,
                HttpContext = httpContext,
                Description = desc ?? string.Format("{0} request", routeName),
                RouteName = routeName,
                Controller = controller,
                Action = action,
                OtherRouteValues = otherRouteValues
            };
        }

        public static MappingExpectation NoMatch(RouteCollection routes, HttpContextBase httpContext, string desc)
        {
            return new MappingExpectation
            {
                Routes = routes,
                HttpContext = httpContext,
                Description = desc,
                RouteName = null
            };
        }

        public RouteCollection Routes { get; set; }

        public HttpContextBase HttpContext { get; set; }

        public string RouteName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public object OtherRouteValues { get; set; }

        public string Description { get; set; }

        public bool ShouldMatchRoute
        {
            get
            {
                return RouteName != null;
            }
        }

        public override string ToString()
        {
            var description = new StringBuilder();
            description.AppendFormat("{0} {1} ({2})", HttpContext.Request.HttpMethod, HttpContext.Request.Url.PathAndQuery, Description);

            if (ShouldMatchRoute)
            {
                description.AppendFormat(" should match route {0}", RouteName);
            }
            else
            {
                description.Append(" should not match any routes");
            }
            return description.ToString();
        }

        public void Verify()
        {
            var routeData = GetRoute();

            if (ShouldMatchRoute)
            {
                routeData.Should().NotBeNull("a route should map to request");
                routeData.Route.Should().BeOfType<ResourceActionRoute>("a library-specific route should be mapped");
                var route = routeData.Route as ResourceActionRoute;
                // ReSharper disable once PossibleNullReferenceException
                route.Name.Should().Be(RouteName, "route {0} should map to request", RouteName);
                var values = routeData.Values;
                values.GetController().ShouldBeEquivalentTo(Controller, "route should map to controller");
                values.GetAction().ShouldBeEquivalentTo(Action, "route should map to action");

                if (OtherRouteValues != null)
                {
                    var actualValues = new RouteValueDictionary(values);
                    actualValues.Remove("controller");
                    actualValues.Remove("action");
                    var expectedValues = new RouteValueDictionary(OtherRouteValues);
                    actualValues.ShouldBeEquivalentTo(expectedValues, "route data should contain expected additional route values");
                }
            }
            else
            {
                routeData.Should().BeNull("no route should map to request");
            }
        }

        private RouteData GetRoute()
        {
            var routeData = Routes.GetRouteData(HttpContext);
            return routeData;
        }
    }
}