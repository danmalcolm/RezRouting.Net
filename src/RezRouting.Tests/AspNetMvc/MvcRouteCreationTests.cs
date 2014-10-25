using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcRouteCreationTests
    {
        private readonly RouteType action1RouteType = new RouteType("RouteType1",
            (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

        private readonly RouteType routeType2 = new RouteType("RouteType2",
            (resource, type, route) => route.Configure("Route2", "Action2", "GET", "action2"));

        [Fact]
        public void should_create_routes_for_resources_at_all_levels_of_model()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(action1RouteType);
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
            ResourcesModel model = null;

            mapper.MapMvcRoutes(routes, modelAction: x => model = x);
            
            var expectedRouteNames = model.Resources.Expand().Select(resource => resource.FullName + ".Route1");
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .ShouldBeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void should_name_route_based_on_full_name_of_resource()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(action1RouteType);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            mapper.MapMvcRoutes(routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            routes["Products.Route1"].Should().BeSameAs(route);
        }

        [Fact]
        public void should_add_route_model_to_route()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(action1RouteType);
            mapper.Collection("Products", products => products.HandledBy<TestController>());
            var routes = new RouteCollection();
            ResourcesModel model = null;

            mapper.MapMvcRoutes(routes, modelAction: x => model = x);
            
            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["RouteModel"].Should().Be(model.Resources.First().Routes.First());
        }

        [Fact]
        public void should_map_collection_routes_before_item_routes()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(action1RouteType, routeType2);
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
            mapper.MapMvcRoutes(routes);
            var expectedRouteNames = new[]
            {
                "Products.Route1", "Products.Route2", "Products.Product.Route1", "Products.Product.Route2",
                "Products.Product.Reviews.Route1", "Products.Product.Reviews.Route2", "Products.Product.Reviews.Review.Route1", "Products.Product.Reviews.Review.Route2"
            };
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .Should().Equal(expectedRouteNames);
        }

        [Fact]
        public void should_include_area_when_mapping_area_routes()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(action1RouteType);
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController>();
                products.Items(product => product.HandledBy<TestController>());
            });

            var routes = new RouteCollection();
            mapper.MapMvcRoutes(routes, area: "Area1");

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens[RouteDataTokenKeys.Area] as string)
                .Should().OnlyContain(x => Equals(x, "Area1"));
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