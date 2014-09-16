using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests
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
                (resource, type, route) =>
                {
                    actualAttempts.Add(Tuple.Create(resource, type, "RouteType1"));
                });
            var routeType2 = new RouteType("RouteType2",
                (resource, type, route) =>
                {
                    actualAttempts.Add(Tuple.Create(resource, type, "RouteType2"));
                });
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
                    if (type == typeof (TestController1))
                    {
                        route.Configure("Route1", "Action1", "GET", "action1");
                    }
                });
            var routeType2 = new RouteType("RouteType2",
                (resource, type, route) =>
                {
                    if (type == typeof(TestController2))
                    {
                        route.Configure("Route2", "Action2", "GET", "action2");
                    }
                });
            mapper.RouteTypes(routeType1, routeType2);
            var resources = mapper.Build().ToList();

            var resource1 = resources.Single();
            var resource2 = resource1.Children.Single();
            resource1.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route1"});
            resource2.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route2" });
        }

        [Fact]
        public void should_customise_url_formatting_using_options()
        {
            var routeType1 = new RouteType("RouteType1",
                (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Collection("FineProducts", products => products.HandledBy<TestController1>());
            mapper.Options(options => options.FormatUrlPaths(new UrlPathSettings(caseStyle:CaseStyle.Upper, wordSeparator: "_")));

            var routeUrl = mapper.Build().Single().Routes.Single().Url;
            routeUrl.Should().Be("FINE_PRODUCTS/action1");
        }

        [Fact]
        public void should_customise_id_names_using_options()
        {
            var routeType1 = new RouteType("RouteType1",
                (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            mapper.Collection("Products", products => products.Items(product => product.HandledBy<TestController1>()));
            mapper.Options(options => options.CustomiseIdNames(new DefaultIdNameConvention("code", true)));

            var resourceUrl = mapper.Build().Single().Children.Single(x => x.Level == ResourceLevel.CollectionItem).Url;
            resourceUrl.Should().Be("products/{productCode}");
        }


        public class TestController1
        {
            
        }

        public class TestController2
        {

        }


    }

}