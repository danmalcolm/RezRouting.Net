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
    public class ResourceBuilderTests
    {
        [Fact]
        public void should_create_resources_configured_via_builders()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => {});
            builder.Singular("Profile", profile => {});
            var model = builder.Build();

            model.Resources.Should().HaveCount(2);
            model.Resources.Should().Contain(x => x.Name == "Products" && x.Level == ResourceLevel.Collection);
            model.Resources.Should().Contain(x => x.Name == "Profile" && x.Level == ResourceLevel.Singular);
        }
        
        [Fact]
        public void should_include_base_path_in_all_resource_urls()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => { });

            builder.BasePath("api");
            var model = builder.Build();

            var urls = model.AllResources().Select(x => x.Url);
            urls.Should().BeEquivalentTo("api/products", "api/products/{id}");
        }

        [Fact]
        public void should_include_base_name_in_full_resource_names()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => { });

            builder.BaseName("Api");
            var model = builder.Build();

            var fullNames = model.AllResources().Select(x => x.FullName);
            fullNames.Should().BeEquivalentTo("Api.Products", "Api.Products.Product");
        }

        [Fact]
        public void should_attempt_to_create_route_for_each_resource_and_convention()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
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
            builder.RouteConventions(convention1, convention2);

            var model = builder.Build();

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
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Items(product => product.HandledBy<TestController2>());
            });

            var convention1 = new TestRouteConvention("Route1", "Action1", "GET", "action1",
                (r,t) => t == typeof (TestController1));
            var convention2 = new TestRouteConvention("Route2", "Action2", "GET", "action2",
                (r,t) => t == typeof (TestController2));
            builder.RouteConventions(convention1, convention2);
            var model = builder.Build();

            var resource1 = model.Resources.Single();
            var resource2 = resource1.Children.Single();
            resource1.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route1"});
            resource2.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route2" });
        }

        [Fact]
        public void should_include_routes_specified_on_resource_before_routes_created_by_conventions()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Route("Route2", typeof(TestController2), "Action2", "GET", "action2");
            });

            var convention1 = new TestRouteConvention("Route1", "Action1", "GET", "action1");
            builder.RouteConventions(convention1);
            var model = builder.Build();

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