using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc.Utility;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc.Tests
{
    public class RouteMappingExtensionsTests
    {
        [Fact]
        public void when_mapping_MVC_routes_should_map_routes_based_on_resources_model()
        {
            var builder = CreateBuilder();

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens[RouteDataTokenKeys.RouteModel] as Route)
                .Select(x => x.FullName)
                .ShouldBeEquivalentTo(new[] { "Products.Route1", "Products.Route2" });
        }

        [Fact]
        public void when_mapping_MVC_routes_should_map_routes_for_area_specified()
        {
            var builder = CreateBuilder();

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes, area: "Area1");

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["area"] as string)
                .ShouldBeEquivalentTo(new[] { "Area1", "Area1" });
        }

        [Fact]
        public void when_mapping_MVC_routes_should_execute_action_with_model_used_to_create_routes()
        {
            var builder = CreateBuilder();

            Resource model = null;
            builder.MapMvcRoutes(new RouteCollection(), modelAction: x => model = x);

            model.Should().NotBeNull();
            model.Children.Should().HaveCount(1);
            model.Children.Should().ContainSingle(x => x.Name == "Products");
        }
        
        private IRootResourceBuilder CreateBuilder()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", new MvcAction(typeof(TestController), "Action1"));
                products.Route("Route2", "GET", "action2", new MvcAction(typeof(TestController), "Action2"));
            });
            return root;
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