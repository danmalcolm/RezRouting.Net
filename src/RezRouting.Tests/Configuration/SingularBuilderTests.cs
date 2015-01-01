using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class SingularBuilderTests
    {
        private readonly RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<IRouteConvention>(), new OptionsBuilder().Build());

        [Fact]
        public void should_build_singular_resource()
        {
            var builder = new SingularBuilder("Profile");

            var resource = builder.Build(context);

            resource.Should().NotBeNull();
            resource.Type.Should().Be(ResourceType.Singular);
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
        public void should_throw_if_custom_url_path_invalid()
        {
            var builder = new SingularBuilder("Profile");

            Action action = () => builder.UrlPath("mypr*()ofile");
            action.ShouldThrow<ArgumentException>()
                .WithMessage("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.*")
                .Which.ParamName.Should().Be("path");
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
            child.Type.Should().Be(ResourceType.Singular);
            child.Name.Should().Be("User");
            child.Parent.Should().Be(resource);
        }

        [Fact]
        public void when_configuring_nested_collection_should_add_child_resource()
        {
            var builder = new SingularBuilder("Profile");
            builder.Collection("Logins", logins => { });

            var resource = builder.Build(context);

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Type.Should().Be(ResourceType.Collection);
            child.Name.Should().Be("Logins");
            child.Parent.Should().Be(resource);
        }
    }
}