using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using RezRouting.Configuration.Builders;
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

            var allNames = resources[""].Expand().Select(x => x.FullName).ToList();
            convention1.Calls.Select(x => x.ResourceFullName).Should().BeEquivalentTo(allNames);
            convention2.Calls.Select(x => x.ResourceFullName).Should().BeEquivalentTo(allNames);
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

            var allNames = resources[""].Expand().Select(x => x.FullName).ToList();
            convention1.Calls.Select(x => x.ResourceFullName).Should().BeEquivalentTo(allNames);
            convention2.Calls.Select(x => x.ResourceFullName).Should().BeEquivalentTo(allNames);
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
                    products.ExtensionData(x => x["key1"] = "value1");
                    products.Items(product =>
                    {
                        product.ExtensionData(x => x["key2"] = "value2");
                    });
                });
            });

            var root1 = resources[""];
            var collection = resources["Products"];
            var collectionItem = resources["Products.Product"];
            var expectedCalls = new List<ConventionCreateCall>()
            {
                new ConventionCreateCall(root1.FullName, new CustomValueCollection(), new CustomValueCollection(), null),
                new ConventionCreateCall(collection.FullName, new CustomValueCollection(), new CustomValueCollection{{"key1", "value1"}}, null),
                new ConventionCreateCall(collectionItem.FullName, new CustomValueCollection(), new CustomValueCollection{{"key2", "value2"}}, null)
            };
            convention1.Calls.ShouldAllBeEquivalentTo(expectedCalls, options => options.ExcludingMissingProperties());
            convention2.Calls.ShouldAllBeEquivalentTo(expectedCalls, options => options.ExcludingMissingProperties());
        }

        [Fact]
        public void should_apply_each_convention_using_shared_convention_data()
        {
            var convention1 = new TestRouteConvention("ConventionRoute1");
            var convention2 = new TestRouteConvention("ConventionRoute2");

            var resources = BuildResources(root =>
            {
                root.SharedExtensionData(x => x["key1"] = "value1");
                root.ApplyRouteConventions(convention1, convention2);
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                    });
                });
            });

            var root1 = resources[""];
            var collection = resources["Products"];
            var collectionItem = resources["Products.Product"];
            var expectedCalls = new List<ConventionCreateCall>()
            {
                new ConventionCreateCall(root1.FullName, new CustomValueCollection(), new CustomValueCollection(), null),
                new ConventionCreateCall(collection.FullName, new CustomValueCollection(), new CustomValueCollection{{"key1", "value1"}}, null),
                new ConventionCreateCall(collectionItem.FullName, new CustomValueCollection(), new CustomValueCollection{{"key2", "value2"}}, null)
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
                root.ExtensionData(data => data["key1"] = "value1");
                root.ExtensionData(data => data["key2"] = "value2");
                root.ApplyRouteConventions(convention);
            });

            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };

            convention.Calls.Single().ConventionData.Should().Equal(expectedData);
        }

        [Fact]
        public void should_supply_empty_data_to_convention_when_none_configured()
        {
            var convention = new TestRouteConvention("ConventionRoute1");
            BuildResources(root =>
            {
                root.ApplyRouteConventions(convention);
            });
            convention.Calls.Single().ConventionData.Should().BeEmpty();
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

            public IEnumerable<Route> Create(ResourceData resource, CustomValueCollection sharedConventionData, CustomValueCollection conventionData, UrlPathSettings urlPathSettings, CustomValueCollection contextItems)
            {
                Calls.Add(new ConventionCreateCall(resource.FullName, sharedConventionData, conventionData, urlPathSettings));
                yield return new Route(name, "GET", name.ToLower(), Mock.Of<IResourceRouteHandler>());
            }
        }

        private class ConventionCreateCall
        {
            public ConventionCreateCall(string resourceFullName, CustomValueCollection sharedConventionData, CustomValueCollection conventionData, UrlPathSettings urlPathSettings)
            {
                ResourceFullName = resourceFullName;
                SharedConventionData = sharedConventionData;
                ConventionData = conventionData;
                UrlPathSettings = urlPathSettings;
            }

            public readonly string ResourceFullName;
            public readonly CustomValueCollection SharedConventionData;
            public readonly CustomValueCollection ConventionData;
            public readonly UrlPathSettings UrlPathSettings;
        }
    }
}