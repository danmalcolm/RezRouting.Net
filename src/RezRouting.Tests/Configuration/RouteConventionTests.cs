using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using RezRouting.Configuration;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RouteConventionTests
    {
        [Fact]
        public void should_apply_each_convention_to_each_resource_in_hierarchy()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var root = BuildResourcesWithConventions(builder =>
            {
                builder.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => {});
                    });
                });
            }, convention1, convention2);

            convention1.Calls.Select(x => x.Resource).Should().BeEquivalentTo(root.Expand());
            convention2.Calls.Select(x => x.Resource).Should().BeEquivalentTo(root.Expand());
        }

        [Fact]
        public void should_apply_each_convention_using_each_resources_convention_data()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var root = BuildResourcesWithConventions(builder =>
            {
                builder.Collection("Products", products =>
                {
                    products.ConventionData(x => x["key1"] = "value1");
                    products.Items(product =>
                    {
                        product.ConventionData(x => x["key2"] = "value2");
                    });
                });
            }, convention1, convention2);

            var collection = root.Children.Single();
            var collectionItem = collection.Children.Single();
            var expectedCalls = new List<ConventionCreateCall>()
            {
                new ConventionCreateCall(root, new CustomValueCollection(), null),
                new ConventionCreateCall(collection, new CustomValueCollection{{"key1", "value1"}}, null),
                new ConventionCreateCall(collectionItem, new CustomValueCollection{{"key2", "value2"}}, null)
            };
            convention1.Calls.ShouldAllBeEquivalentTo(expectedCalls, options => options.ExcludingMissingProperties());
            convention2.Calls.ShouldAllBeEquivalentTo(expectedCalls, options => options.ExcludingMissingProperties());
        }

        [Fact]
        public void should_combine_modifications_to_convention_data_when_configuring_resource()
        {
            var convention = new TestRouteConvention("ConventionRoute1");
            var root = BuildResourcesWithConventions(builder =>
            {
                builder.ConventionData(data => data["key1"] = "value1");
                builder.ConventionData(data => data["key2"] = "value2");
            }, convention);

            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };

            convention.Calls.Single().Data.Should().Equal(expectedData);
        }

        [Fact]
        public void should_supply_empty_data_to_convention_when_none_configured()
        {
            var convention = new TestRouteConvention("ConventionRoute1");
            var root = BuildResourcesWithConventions(builder => { },
                convention);
            convention.Calls.Single().Data.Should().BeEmpty();
        }

        [Fact]
        public void should_add_routes_created_by_each_convention()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");
            var root = BuildResourcesWithConventions(builder =>
            {
                builder.Collection("Products", products => { });
            }, convention1, convention2);

            root.Routes.Select(x => x.Name).Should().Equal("ConventionRoute1", "ConventionRoute2");
            var collection = root.Children.Single();
            collection.Routes.Select(x => x.Name).Should().Equal("ConventionRoute1", "ConventionRoute2");
        }

        [Fact]
        public void should_include_routes_specified_on_resource_before_routes_created_by_conventions()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");
            var root = BuildResourcesWithConventions(builder =>
            {
                builder.Route("Route1", "GET", "", Mock.Of<IResourceRouteHandler>());
            }, convention1, convention2);
            
            root.Routes.Select(x => x.Name).Should().Equal("Route1", "ConventionRoute1", "ConventionRoute2");
        }


        /// <summary>
        /// Configures resources using supplied action and returns the root resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResourcesWithConventions(Action<IRootResourceBuilder> configure, params IRouteConvention[] conventions)
        {
            var builder = RootResourceBuilder.Create();
            configure(builder);
            var scheme = new TestRouteConventionScheme(conventions);
            builder.ApplyRouteConventions(scheme);
            var root = builder.Build();
            return root;
        }

        private class TestRouteConvention : IRouteConvention
        {
            private readonly string name;

            public TestRouteConvention(string name)
            {
                this.name = name;
            }

            public List<ConventionCreateCall> Calls = new List<ConventionCreateCall>();

            public IEnumerable<Route> Create(Resource resource, CustomValueCollection data, UrlPathSettings urlPathSettings, CustomValueCollection contextItems)
            {
                Calls.Add(new ConventionCreateCall(resource, data, urlPathSettings));
                yield return new Route(name, "GET", name.ToLower(), Mock.Of<IResourceRouteHandler>());
            }
        }

        private class ConventionCreateCall
        {
            public ConventionCreateCall(Resource resource, CustomValueCollection data, UrlPathSettings urlPathSettings)
            {
                Resource = resource;
                Data = data;
                UrlPathSettings = urlPathSettings;
            }

            public readonly Resource Resource;
            public readonly CustomValueCollection Data;
            public readonly UrlPathSettings UrlPathSettings;
        }
    }
}