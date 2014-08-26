using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class RouteMapperTests
    {
        [Fact]
        public void should_create_resources_configured_via_builders()
        {
            var mapper = new RouteMapper();
            
            mapper.Collection("Products", products => {});
            mapper.Singular("Profile", profile => {});
            var resources = mapper.Build().ToList();

            resources.Should().HaveCount(2);
            resources.Should().Contain(x => x.Name == "Products" && x.Level == ResourceLevel.Collection);
            resources.Should().Contain(x => x.Name == "Profile" && x.Level == ResourceLevel.Singular);
        }

        [Fact]
        public void should_attempt_to_create_route_for_each_resource_and_controller_and_route_type()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.HandledBy<TestController2>();
                products.Items(product =>
                {
                    product.HandledBy<TestController1>();
                    product.HandledBy<TestController2>();
                });
            });
            var actualAttempts = new List<Tuple<Resource, Type,string>>();
            
            var routeType1 = new RouteType("RouteType1", 
                (resource, type, route) => actualAttempts.Add(Tuple.Create(resource,type,"RouteType1")));
            var routeType2 = new RouteType("RouteType2",
                (resource, type, route) => actualAttempts.Add(Tuple.Create(resource,type,"RouteType2")));
            mapper.RouteTypes(routeType1, routeType2);
            var resources = mapper.Build().ToList();
            
            var resource1 = resources.Single();
            var resource2 = resource1.Children.Single();
            var expectedAttempts = new List<Tuple<Resource, Type, string>>()
            {
                Tuple.Create(resource1, typeof (TestController1), "RouteType1"),
                Tuple.Create(resource1, typeof (TestController2), "RouteType1"),
                Tuple.Create(resource1, typeof (TestController1), "RouteType2"),
                Tuple.Create(resource1, typeof (TestController2), "RouteType2"),
                Tuple.Create(resource2, typeof (TestController1), "RouteType1"),
                Tuple.Create(resource2, typeof (TestController2), "RouteType1"),
                Tuple.Create(resource2, typeof (TestController1), "RouteType2"),
                Tuple.Create(resource2, typeof (TestController2), "RouteType2")
            };
            actualAttempts.ShouldAllBeEquivalentTo(expectedAttempts);
        }

        [Fact]
        public void should_create_routes_specified_by_each_route_type()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Items(product => product.HandledBy<TestController2>());
            });

            var routeType1 = new RouteType("RouteType1",
                (resource, type, route) =>
                {
                    if (type != typeof (TestController1))
                        route.Skip();
                    else
                        route.Name("Route1");
                });
            var routeType2 = new RouteType("RouteType2",
                (resource, type, route) =>
                {
                    if (type != typeof(TestController2))
                        route.Skip();
                    else
                        route.Name("Route2");
                });
            mapper.RouteTypes(routeType1, routeType2);
            var resources = mapper.Build().ToList();

            var resource1 = resources.Single();
            var resource2 = resource1.Children.Single();
            resource1.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route1"});
            resource2.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route2" });
        }

        public class TestController1
        {
            
        }

        public class TestController2
        {

        }


    }

}