using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RouteConventionTests : ConfigurationTestsBase
    {
        [Fact]
        public void should_apply_each_convention_in_scheme_to_each_resource_in_hierarchy()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                    });
                });
                var conventionScheme = new TestRouteConventionScheme(convention1, convention2);
                root.ApplyRouteConventions(conventionScheme);
            });

            var all = resources[""].Expand().ToList();
            convention1.Calls.Select(x => x.Resource).Should().BeEquivalentTo(all);
            convention2.Calls.Select(x => x.Resource).Should().BeEquivalentTo(all);
        }

        [Fact]
        public void should_apply_each_convention_to_each_resource_in_hierarchy()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                    });
                });
                root.ApplyRouteConventions(convention1, convention2);
            });

            var all = resources[""].Expand().ToList();
            convention1.Calls.Select(x => x.Resource).Should().BeEquivalentTo(all);
            convention2.Calls.Select(x => x.Resource).Should().BeEquivalentTo(all);
        }
        
        [Fact]
        public void should_apply_each_convention_using_each_resources_convention_data()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var resources = BuildResources(root =>
            {
                root.ApplyRouteConventions(convention1, convention2);
                root.Collection("Products", products =>
                {
                    products.ConventionData(x => x["key1"] = "value1");
                    products.Items(product =>
                    {
                        product.ConventionData(x => x["key2"] = "value2");
                    });
                });
            });

            var root1 = resources[""];
            var collection = resources["Products"];
            var collectionItem = resources["Products.Product"];
            var expectedCalls = new List<ConventionCreateCall>()
            {
                new ConventionCreateCall(root1, new CustomValueCollection(), null),
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
            BuildResources(root =>
            {
                root.ConventionData(data => data["key1"] = "value1");
                root.ConventionData(data => data["key2"] = "value2");
                root.ApplyRouteConventions(convention);
            });

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
            BuildResources(root =>
            {
                root.ApplyRouteConventions(convention);
            });
            convention.Calls.Single().Data.Should().BeEmpty();
        }

        [Fact]
        public void should_add_routes_created_by_each_convention()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products => { });
                root.ApplyRouteConventions(convention1, convention2);
            });

            resources[""].Routes.Select(x => x.Name).Should().Equal("ConventionRoute1", "ConventionRoute2");
            var collection = resources["Products"];
            collection.Routes.Select(x => x.Name).Should().Equal("ConventionRoute1", "ConventionRoute2");
        }

        [Fact]
        public void should_include_routes_specified_on_resource_before_routes_created_by_conventions()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");
            var resources = BuildResources(builder =>
            {
                builder.Route("Route1", "GET", "", Mock.Of<IResourceRouteHandler>());
                builder.ApplyRouteConventions(convention1, convention2);
            });
            resources[""].Routes.Select(x => x.Name).Should().Equal("Route1", "ConventionRoute1", "ConventionRoute2");
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