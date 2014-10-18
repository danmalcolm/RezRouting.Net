using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcRouteMapperExtensionsTests
    {
        private readonly RouteType routeType1 = new RouteType("RouteType1",
            (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

        private readonly RouteType routeType2 = new RouteType("RouteType2",
            (resource, type, route) => route.Configure("Route2", "Action2", "GET", "action2"));

        [Fact]
        public void when_mapping_MVC_routes_should_map_routes_based_on_resources_model()
        {
            var mapper = CreateRouteMapper();
            mapper.Collection("Products", products => products.HandledBy<TestController>());
            
            var routes = new RouteCollection();
            mapper.MapMvcRoutes(routes);

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["RouteModel"] as Route)
                .Select(x => x.FullName)
                .ShouldBeEquivalentTo(new[] { "Products.Route1", "Products.Route2" });
        }

        [Fact]
        public void when_mapping_MVC_routes_should_execute_action_with_model_used_to_create_routes()
        {
            var mapper = CreateRouteMapper();
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            ResourcesModel model = null;
            mapper.MapMvcRoutes(new RouteCollection(), modelAction: x => model = x);

            model.Should().NotBeNull();
            model.Resources.Should().HaveCount(1);
            model.Resources.Should().ContainSingle(x => x.Name == "Products");

        }
        
        private RouteMapper CreateRouteMapper()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1, routeType2);
            return mapper;
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