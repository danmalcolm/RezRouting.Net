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
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => {});
            builder.Singular("Profile", profile => {});
            var model = builder.Build();

            model.Children.Should().HaveCount(2);
            model.Children.Should().Contain(x => x.Name == "Products" && x.Type == ResourceType.Collection);
            model.Children.Should().Contain(x => x.Name == "Profile" && x.Type == ResourceType.Singular);
        }
        
        [Fact]
        public void should_include_root_path_in_all_resource_urls()
        {
            var builder = RootResourceBuilder.Create("Api");
            builder.UrlPath("api");
            builder.Collection("Products", products => { });

            var root = builder.Build();

            var urls = root.Children.Expand().Select(x => x.Url);
            urls.Should().BeEquivalentTo("api/products", "api/products/{id}");
        }

        [Fact]
        public void should_include_base_name_in_full_resource_names()
        {
            var builder = RootResourceBuilder.Create("Api");
            builder.Collection("Products", products => { });

            var root = builder.Build();

            var fullNames = root.Children.Expand().Select(x => x.FullName);
            fullNames.Should().BeEquivalentTo("Api.Products", "Api.Products.Product");
        }
    }
}