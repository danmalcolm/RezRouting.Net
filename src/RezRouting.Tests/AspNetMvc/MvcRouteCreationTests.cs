using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcRouteCreationTests
    {
        [Fact]
        public void should_create_routes_for_resources_at_all_levels_of_model()
        {
            var handler = MvcAction.For((TestController c) => c.Action1());
            var builder = new ResourceGraphBuilder("");
            builder.Singular("Profile", profile =>
            {
                profile.Route("Route1", handler, "GET", "action1");
                profile.Singular("User", user => user.Route("Route1", handler, "GET", "action1"));
            });
            builder.Collection("Products", products =>
            {
                products.Route("Route1", handler, "GET", "action1");
                products.Items(product =>
                {
                    product.Route("Route1", handler, "GET", "action1");
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Route("Route1", handler, "GET", "action1");
                        reviews.Items(review =>
                        {
                            review.Route("Route1", handler, "GET", "action1");
                        });
                    });
                });
            });
            var routes = new RouteCollection();
            Resource model = null;

            builder.MapMvcRoutes(new ResourceOptions(), routes, modelAction: x => model = x);
            
            var expectedRouteNames = model.Children.Expand().Select(resource => resource.FullName + ".Route1");
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .ShouldBeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void should_name_route_based_on_full_name_of_resource()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products",
                products => products.Route("Route1", MvcAction.For((TestController c) => c.Action1()),
                    "GET", "action1"));

            var routes = new RouteCollection();
            builder.MapMvcRoutes(new ResourceOptions(), routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            routes["Products.Route1"].Should().BeSameAs(route);
        }

        [Fact]
        public void should_throw_if_route_names_are_not_unique()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
            });

            Action action = () => builder.MapMvcRoutes(new ResourceOptions(), new RouteCollection());
            const string expectedMessage =
                @"Unable to add routes to RouteCollection because the following route names are not unique:
Products.Route1 - (defined on resources Products and Products)
";
            action.ShouldThrow<RouteConfigurationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void should_throw_if_route_names_already_in_use()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products => 
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1"));
            var routes = new RouteCollection();
            routes.MapRoute("Products.Route1", "url");
            
            Action action = () => builder.MapMvcRoutes(new ResourceOptions(), routes);

            const string expectedMessage = @"Unable to create routes because the following routes have names that already exist in the RouteCollection:
Products.Route1 - (defined on resource Products)
";
            action.ShouldThrow<RouteConfigurationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void should_add_route_model_to_route()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products => 
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1"));
            var routes = new RouteCollection();
            Resource model = null;

            builder.MapMvcRoutes(new ResourceOptions(), routes, modelAction: x => model = x);
            
            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["RouteModel"].Should().Be(model.Children.First().Routes.First());
        }

        [Fact]
        public void should_map_collection_routes_before_item_routes()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                products.Route("Route2", MvcAction.For((TestController c) => c.Action2()), "GET", "action2");
                products.Items(product =>
                {
                    product.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                    product.Route("Route2", MvcAction.For((TestController c) => c.Action2()), "GET", "action2");
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                        reviews.Route("Route2", MvcAction.For((TestController c) => c.Action2()), "GET", "action2");
                        reviews.Items(review =>
                        {
                            review.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                            review.Route("Route2", MvcAction.For((TestController c) => c.Action2()), "GET", "action2");
                        });
                    });
                });
            });
            var routes = new RouteCollection();
            builder.MapMvcRoutes(new ResourceOptions(), routes);
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
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", MvcAction.For((TestController c) => c.Action2()), "GET", "action2");
                products.Items(product => product.HandledBy<TestController>());
            });

            var routes = new RouteCollection();
            builder.MapMvcRoutes(new ResourceOptions(), routes, area: "Area1");

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["area"] as string)
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