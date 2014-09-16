using System.Linq;
using FluentAssertions;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests
{
    public class SingularBuilderTests
    {
        private readonly RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<RouteType>(), new OptionsBuilder().Build());

        [Fact]
        public void should_build_singular_resource()
        {
            var builder = new SingularBuilder("Profile");
            
            var resource = builder.Build(context);
            
            resource.Should().NotBeNull();
            resource.Level.Should().Be(ResourceLevel.Singular);
            resource.Name.Should().Be("Profile");
        }

        [Fact]
        public void should_default_to_url_path_based_on_resource_name()
        {
            var builder = new SingularBuilder("Profile");
            
            var resource = builder.Build(context);

            resource.Url.Should().Be("profile");
        }

        [Fact]
        public void should_use_customised_url_path()
        {
            var builder = new SingularBuilder("Profile");

            builder.UrlPath("myprofile");
            var resource = builder.Build(context);

            resource.Url.Should().Be("myprofile");
        }
        
        [Fact]
        public void should_not_build_any_children_if_none_configured()
        {
            var builder = new SingularBuilder("Profile");
            var resource = builder.Build(context);
            resource.Children.Should().BeEmpty();
        }

        [Fact]
        public void when_nested_singular_configured_should_add_child_resource()
        {
            var builder = new SingularBuilder("Profile");
            builder.Singular("User", user => { });

            var resource = builder.Build(context);

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Level.Should().Be(ResourceLevel.Singular);
            child.Name.Should().Be("User");
            child.Parent.Should().Be(resource);
        }

        [Fact]
        public void when_nested_collection_configured_should_add_child_resource()
        {
            var builder = new SingularBuilder("Profile");
            builder.Collection("Logins", logins => { });

            var resource = builder.Build(context);

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Level.Should().Be(ResourceLevel.Collection);
            child.Name.Should().Be("Logins");
            child.Parent.Should().Be(resource);
        }
    }
}