using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RouteMapperTests
    {
        [Fact]
        public void should_create_resources_configured_via_builders()
        {
            var mapper = new RouteMapper();
            
            mapper.Collection("Products", products => {});
            mapper.Singular("Profile", profile => {});
            var model = mapper.Build();

            model.Resources.Should().HaveCount(2);
            model.Resources.Should().Contain(x => x.Name == "Products" && x.Level == ResourceLevel.Collection);
            model.Resources.Should().Contain(x => x.Name == "Profile" && x.Level == ResourceLevel.Singular);
        }
        
        [Fact]
        public void should_include_base_path_in_all_resource_urls()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products => { });

            mapper.BasePath("api");
            var model = mapper.Build();

            var urls = model.AllResources().Select(x => x.Url);
            urls.Should().BeEquivalentTo("api/products", "api/products/{id}");
        }

        [Fact]
        public void should_include_base_name_in_full_resource_names()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products => { });

            mapper.BaseName("Api");
            var model = mapper.Build();

            var fullNames = model.AllResources().Select(x => x.FullName);
            fullNames.Should().BeEquivalentTo("Api.Products", "Api.Products.Product");
        }

        [Fact]
        public void should_attempt_to_create_route_for_each_resource_and_convention()
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
            var actualAttempts = new List<Tuple<RecordingRouteConvention, Resource, string>>();

            var convention1 = new RecordingRouteConvention(actualAttempts);
            var convention2 = new RecordingRouteConvention(actualAttempts);
            mapper.RouteConventions(convention1, convention2);

            var model = mapper.Build();

            var collection = model.Resources.Single();
            var collectionItem = collection.Children.Single();
            var expectedAttempts = new List<Tuple<RecordingRouteConvention, Resource, string>>()
            {
                Tuple.Create(convention1, collectionItem, "TestController1,TestController2"),
                Tuple.Create(convention2, collectionItem, "TestController1,TestController2"),
                Tuple.Create(convention1, collection, "TestController1,TestController2"),
                Tuple.Create(convention2, collection, "TestController1,TestController2")
            };
            actualAttempts.ShouldAllBeEquivalentTo(expectedAttempts);
        }

        public class RecordingRouteConvention : IRouteConvention
        {
            private readonly List<Tuple<RecordingRouteConvention, Resource, string>> actualAttempts;

            public RecordingRouteConvention(List<Tuple<RecordingRouteConvention, Resource, string>> actualAttempts)
            {
                this.actualAttempts = actualAttempts;
            }

            public virtual IEnumerable<Route> Create(Resource resource, IEnumerable<Type> controllerTypes, UrlPathFormatter pathFormatter)
            {
                if (controllerTypes.Any())
                {
                    string typeNames = string.Join(",", controllerTypes.Select(x => x.Name));
                    actualAttempts.Add(Tuple.Create(this, resource, typeNames));
                }
                yield break;
            }
        }

        [Fact]
        public void should_create_routes_specified_by_each_convention()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Items(product => product.HandledBy<TestController2>());
            });

            var convention1 = new TestRouteConvention("Route1", "Action1", "GET", "action1",
                (r,t) => t == typeof (TestController1));
            var convention2 = new TestRouteConvention("Route2", "Action2", "GET", "action2",
                (r,t) => t == typeof (TestController2));
            mapper.RouteConventions(convention1, convention2);
            var model = mapper.Build();

            var resource1 = model.Resources.Single();
            var resource2 = resource1.Children.Single();
            resource1.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route1"});
            resource2.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route2" });
        }

        [Fact]
        public void should_include_routes_specified_on_resource_before_routes_created_by_conventions()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Route("Route2", typeof(TestController2), "Action2", "GET", "action2");
            });

            var convention1 = new TestRouteConvention("Route1", "Action1", "GET", "action1");
            mapper.RouteConventions(convention1);
            var model = mapper.Build();

            var resource1 = model.Resources.Single();
            resource1.Routes.Select(x => x.Name).Should().Equal("Route2", "Route1");
        }
        
        public class TestController1
        {
            
        }

        public class TestController2
        {

        }
    }
}