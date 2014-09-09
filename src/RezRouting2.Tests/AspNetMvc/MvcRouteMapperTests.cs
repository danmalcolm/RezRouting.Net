using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting2.AspNetMvc;
using RezRouting2.Tests.Utility;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc
{
    public class MvcRouteMapperTests
    {
        private readonly RouteType routeType1 = new RouteType("RouteType1",
            (resource, type, route) => route.Configure(resource.Name + ".Route1", "Action1", "GET", "action1"));

        private readonly RouteType routeType2 = new RouteType("RouteType2",
            (resource, type, route) => route.Configure(resource.Name + ".Route2", "Action2", "GET", "action2"));

        [Fact]
        public void should_create_routes_for_resources_at_all_levels_of_model()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Singular("Profile", profile =>
            {
                profile.HandledBy<TestController>();
                profile.Singular("User", user => user.HandledBy<TestController>());
            });
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController>();
                products.Items(product =>
                {
                    product.HandledBy<TestController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<TestController>();
                        reviews.Items(review => review.HandledBy<TestController>());
                    });
                });
            });
            var routes = new RouteCollection();
            var resources = mapper.Build().ToList();
            new MvcRouteMapper().CreateRoutes(resources, routes);
            var expectedRouteNames = resources.Expand().Select(resource => resource.Name + ".Route1");
            routes.Cast<ResourceRoute>().Select(x => x.Name).ShouldBeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void should_add_route_model_to_route()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            var resources = mapper.Build().ToList();
            new MvcRouteMapper().CreateRoutes(resources, routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["RouteModel"].Should().Be(resources.First().Routes.First());
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