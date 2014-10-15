﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcRouteMapperTests
    {
        private readonly RouteType routeType1 = new RouteType("RouteType1",
            (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

        private readonly RouteType routeType2 = new RouteType("RouteType2",
            (resource, type, route) => route.Configure("Route2", "Action2", "GET", "action2"));

        [Fact]
        public void should_add_name_to_route_based_on_full_name_of_resource()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["Name"].Should().Be("Products.Route1");
        }

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
            var model = mapper.Build();
            new MvcRouteMapper().CreateRoutes(model, routes);
            var expectedRouteNames = model.Resources.Expand().Select(resource => resource.FullName + ".Route1");
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .ShouldBeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void should_add_route_model_to_route()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Collection("Products", products => products.HandledBy<TestController>());

            var routes = new RouteCollection();
            var model = mapper.Build();
            new MvcRouteMapper().CreateRoutes(model, routes);

            var route = routes.Cast<System.Web.Routing.Route>().Single();
            route.DataTokens["RouteModel"].Should().Be(model.Resources.First().Routes.First());
        }

        [Fact]
        public void should_map_collection_routes_before_item_routes()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1, routeType2);
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
            var model = mapper.Build();
            new MvcRouteMapper().CreateRoutes(model, routes);
            var expectedRouteNames = new[]
            {
                "Products.Route1", "Products.Route2", "Products.Product.Route1", "Products.Product.Route2",
                "Products.Product.Reviews.Route1", "Products.Product.Reviews.Route2", "Products.Product.Reviews.Review.Route1", "Products.Product.Reviews.Review.Route2"
            };
            routes.Cast<System.Web.Routing.Route>().Select(x => x.DataTokens["Name"])
                .Should().Equal(expectedRouteNames);
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