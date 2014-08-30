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
        public void should_add_routes_for_each_route_added_by_route_type()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1, routeType2);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), routes);

            routes.Should().HaveCount(2);
            routes.Cast<ResourceRoute>().Select(r => r.Name).ShouldBeEquivalentTo(new [] { "Products.Route1", "Products.Route2" });
        }

        [Fact]
        public void should_add_routes_for_resources_at_all_levels()
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