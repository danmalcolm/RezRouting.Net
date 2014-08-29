using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting2.AspNetMvc;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc
{
    public class RouteMapperTests
    {
        private readonly RouteType routeType1 = new RouteType("RouteType1",
                (resource, type, route) =>
                {
                    route.Name(resource.Name + "Route1");
                    route.Action("Action1");
                    route.HttpMethod("GET");
                    route.Path("action1");
                });

        private readonly RouteType routeType2 = new RouteType("RouteType2",
                (resource, type, route) =>
                {
                    route.Name(resource.Name + "Route2");
                    route.Action("Action2");
                    route.HttpMethod("GET");
                    route.Path("action1");
                });

        [Fact]
        public void should_add_routes_to_route_table_for_each_route_added_by_route_type()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1, routeType2);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            new AspNetMvcRouteMapper().CreateRoutes(mapper, routes);

            routes.Should().HaveCount(2);
        }

        public class TestController : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }

            public ActionResult Action2()
            {
                return null;
            }

        }
    }
}