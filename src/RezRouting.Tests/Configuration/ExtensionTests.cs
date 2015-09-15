using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using RezRouting.Configuration;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Extensions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ExtensionTests : ConfigurationTestsBase
    {
        [Fact]
        public void should_apply_each_extension_to_root_resource_in_hierarchy()
        {
            var extension1 = new TestExtension("Extension1");
            var extension2 = new TestExtension("Extension2");

            var resources = BuildResources(root =>
            {
                root.Extension(extension1, extension2);
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                    });
                });
            });

            extension1.Calls.Should().HaveCount(1, "it should call each extension");
            extension2.Calls.Should().HaveCount(1, "it should call each extension");
        }
        

        [Fact]
        public void should_combine_modifications_to_extension_data_when_configuring_resource()
        {
            var extension = new TestExtension("Extension1");
            BuildResources(root =>
            {
                root.ExtensionData(data => data["key1"] = "value1");
                root.ExtensionData(data => data["key2"] = "value2");
                root.Extension(extension);
            });

            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };

            extension.Calls.Single().ResourceData.ExtensionData.Should().Equal(expectedData);
        }

        [Fact]
        public void should_extend_copy_of_manually_configured_resource_data()
        {
            var extension = new TestExtension("Extension1");
            var root = RootResourceBuilder.Create();
            root.Extension(extension);
            root.Build();
            root.Build();
            
            extension.Calls.Should().HaveCount(2);
            var data1 = extension.Calls[0].ResourceData;
            var data2 = extension.Calls[1].ResourceData;

            data1.Should().NotBeSameAs(data2);
            data1.ShouldBeEquivalentTo(data2);
        }

        [Fact]
        public void should_supply_empty_data_to_extension_when_none_configured()
        {
            var extension = new TestExtension("Extension1");
            BuildResources(root =>
            {
                root.Extension(extension);
            });
            extension.Calls.Single().ResourceData.ExtensionData.Should().BeEmpty();
        }

        [Fact]
        public void should_add_modifications_from_each_extension_to_resources()
        {
            var extension1 = new TestExtension("Extension1");
            var extension2 = new TestExtension("Extension2");
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products => { });
                root.Extension(extension1, extension2);
            });

            resources[""].Routes.Select(x => x.Name).Should().Equal("Extension1Route", "Extension2Route");
        }

        private class TestExtension : IExtension
        {
            private readonly string name;

            public TestExtension(string name)
            {
                this.name = name;
            }

            public List<ExtensionCall> Calls = new List<ExtensionCall>();

            public void Extend(ResourceData root, ConfigurationContext context, ConfigurationOptions options)
            {
                string routeName = name + "Route";
                var route = new RouteData(routeName, "GET", name.ToLower(), Mock.Of<IResourceRouteHandler>());
                root.AddRoute(route);
                Calls.Add(new ExtensionCall(root, context, options));
            }
        }

        private class ExtensionCall
        {
            public ExtensionCall(ResourceData ResourceData, ConfigurationContext context, ConfigurationOptions options)
            {
                this.ResourceData = ResourceData;
                Context = context;
                Options = options;
            }

            public ResourceData ResourceData { get; set; }
            public ConfigurationContext Context { get; set; }
            public ConfigurationOptions Options { get; set; }
        }
    }
}