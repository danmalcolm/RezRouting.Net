using System;
using System.Text;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.Utility;
using RezRouting.Routing;

namespace RezRouting.Tests.Shared.Expectations
{
    /// <summary>
    /// Defines expected outcome when matching route to an incoming request
    /// </summary>
    public class MappingExpectation
    {
        public static MappingExpectation Match(RouteCollection routes, RouteTestingRequest request, string routeName, string controllerAction, object otherRouteValues, string desc)
        {
            var actionParts = controllerAction.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            string controller = actionParts[0];
            string action = actionParts[1];
            return new MappingExpectation
            {
                Routes = routes,
                Request = request,
                Description = desc ?? string.Format("{0} request", routeName),
                RouteName = routeName,
                Controller = controller,
                Action = action,
                OtherRouteValues = otherRouteValues
            };
        }

        public static MappingExpectation NoMatch(RouteCollection routes, RouteTestingRequest request, string desc)
        {
            return new MappingExpectation
            {
                Routes = routes,
                Request = request,
                Description = desc,
                RouteName = null
            };
        }

        public RouteCollection Routes { get; set; }

        public RouteTestingRequest Request { get; set; }

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
            description.AppendFormat("{0} {1} ({2})", Request.HttpMethod, Request.Path, Description);

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
            var routeData = GetRoute(Routes, Request);

            if (ShouldMatchRoute)
            {
                routeData.Should().NotBeNull("a route should map to request");
                routeData.Route.Should().BeOfType<ResourceActionRoute>("a library-specific route should be mapped");
                var route = routeData.Route as ResourceActionRoute;
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

        protected RouteData GetRoute(RouteCollection routes, RouteTestingRequest request)
        {
            var context = TestHttpContextBuilder.Create(request.Path, request.HttpMethod, request.Headers, request.Form);
            var routeData = routes.GetRouteData(context);
            return routeData;
        }
    }
}