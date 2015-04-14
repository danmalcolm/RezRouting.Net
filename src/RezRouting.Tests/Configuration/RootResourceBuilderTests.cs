using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RootResourceBuilderTests
    {
        [Fact]
        public void should_build_resources_configured_via_child_resource_builders()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("Products", products => {});
            root.Singular("Profile", profile => {});
            var resource = root.Build();

            resource.Children.Should().HaveCount(2);
            resource.Children.Should().Contain(x => x.Name == "Products" && x.Type == ResourceType.Collection);
            resource.Children.Should().Contain(x => x.Name == "Profile" && x.Type == ResourceType.Singular);
        }
        
        [Fact]
        public void should_include_root_path_in_all_resource_urls()
        {
            var root = RootResourceBuilder.Create("Api");
            root.UrlPath("api");
            root.Collection("Products", products => { });
            var resource = root.Build();

            var urls = resource.Children.Expand().Select(x => x.Url);
            urls.Should().BeEquivalentTo("api/products", "api/products/{id}");
        }

        [Fact]
        public void should_include_base_name_in_full_resource_names()
        {
            var root = RootResourceBuilder.Create("Api");
            root.Collection("Products", products => { });
            var resource = root.Build();

            var fullNames = resource.Children.Expand().Select(x => x.FullName);
            fullNames.Should().BeEquivalentTo("Api.Products", "Api.Products.Product");
        }
    }
}