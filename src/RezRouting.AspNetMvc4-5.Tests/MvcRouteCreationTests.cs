using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.AspNetMvc.Tests
{
    public class MvcRouteCreationTests
    {
        [Fact]
        public void should_create_routes_for_resources_at_all_levels_of_model()
        {
            var handler = MvcAction.For((Test1Controller c) => c.Action1());
            var builder = RootResourceBuilder.Create("");
            builder.Singular("Profile", profile =>
            {
                profile.Route("Route1", "GET", "action1", handler);
                profile.Singular("User", user => user.Route("Route1", "GET", "action1", handler));
            });
            builder.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", handler);
                products.Items(product =>
                {
                    product.Route("Route1", "GET", "action1", handler);
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Route("Route1", "GET", "action1", handler);
                        reviews.Items(review =>
                        {
                            review.Route("Route1", "GET", "action1", handler);
                        });
                    });
                });
            });
            var routes = new RouteCollection();
            Resource model = null;

            builder.MapMvcRoutes(routes, modelAction: x => model = x);
            
            var expectedRouteNames = model.Children.Expand().Select(resource => resource.FullName + ".Route1");
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .ShouldBeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void should_set_defaults_based_on_additional_route_values_configured_on_route()
        {
            var builder = RootResourceBuilder.Create("");
            var values = new CustomValueCollection
            {
                {"key1", "value1"}, {"key2", "value2"}
            };
            builder.Collection("Products",
                products => products.Route("Route1",
                    "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()), additionalRouteValues: values));

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.Defaults.Should().Contain("key1", "value1");
            route.Defaults.Should().Contain("key2", "value2");
        }

        [Fact]
        public void should_name_route_based_on_full_name_of_resource()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products",
                products => products.Route("Route1",
                    "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1())));
            
            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            routes["Products.Route1"].Should().BeSameAs(route);
        }

        [Fact]
        public void should_throw_if_route_names_are_not_unique()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
            });

            Action action = () => builder.MapMvcRoutes(new RouteCollection());
            const string expectedMessage =
                @"Unable to add routes to RouteCollection because the following route names are not unique:
Products.Route1 - (defined on resources Products and Products)
";
            action.ShouldThrow<RouteConfigurationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void should_throw_if_route_names_already_in_use()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => 
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1())));
            var routes = new RouteCollection();
            routes.MapRoute("Products.Route1", "url");
            
            Action action = () => builder.MapMvcRoutes(routes);

            const string expectedMessage = @"Unable to create routes because the following routes have names that already exist in the RouteCollection:
Products.Route1 - (defined on resource Products)
";
            action.ShouldThrow<RouteConfigurationException>().WithMessage(expectedMessage);
        }

        [Fact]
        public void should_add_route_model_to_route()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => 
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1())));
            var routes = new RouteCollection();
            Resource model = null;

            builder.MapMvcRoutes(routes, modelAction: x => model = x);
            
            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["RouteModel"].Should().Be(model.Children.First().Routes.First());
        }

        [Fact]
        public void should_map_collection_routes_before_item_routes()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                products.Route("Route2", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                products.Items(product =>
                {
                    product.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                    product.Route("Route2", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                        reviews.Route("Route2", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                        reviews.Items(review =>
                        {
                            review.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                            review.Route("Route2", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                        });
                    });
                });
            });
            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);
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
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                products.Items(product => product.HandledBy<Test1Controller>());
            });

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes, area: "Area1");

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["area"] as string)
                .Should().OnlyContain(x => Equals(x, "Area1"));
        }

        [Fact]
        public void should_initialise_index_used_by_url_generation()
        {
            var builder = RootResourceBuilder.Create();
            builder.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
                products.Route("Route2", "GET", "action2", MvcAction.For((Test1Controller c) => c.Action2()));
                products.Items(product => product.HandledBy<Test1Controller>());
            });

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            var indexed = UrlHelperExtensions.Index.GetRoutes(routes, typeof (Test1Controller), "Action1");
            indexed.Select(x => x.FullName).Should().Equal("Products.Route1");
        }

        [Fact]
        public void when_mapping_routes_for_separate_resources_hierarchies_should_index_all_for_url_generation()
        {
            var routes = new RouteCollection();
            var builder1 = RootResourceBuilder.Create("Area1");
            builder1.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", MvcAction.For((Test1Controller c) => c.Action1()));
            });
            builder1.MapMvcRoutes(routes);
            var builder2 = RootResourceBuilder.Create("Area2");
            builder2.Collection("Products", products =>
            {
                products.Route("Route1", "GET", "action1", MvcAction.For((Test2Controller c) => c.Action1()));
            });
            builder2.MapMvcRoutes(routes);

            UrlHelperExtensions.Index.GetRoutes(routes, typeof(Test1Controller), "Action1")
                .Select(x => x.FullName).Should().Equal("Area1.Products.Route1");
            UrlHelperExtensions.Index.GetRoutes(routes, typeof(Test2Controller), "Action1")
                .Select(x => x.FullName).Should().Equal("Area2.Products.Route1");
        }

        public class Test1Controller : Controller
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

        public class Test2Controller : Controller
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