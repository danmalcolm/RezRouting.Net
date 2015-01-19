using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class SingularConfigurationTests
    {
        private readonly ResourceOptions options = new ResourceOptions();

        [Fact]
        public void should_build_singular_resource()
        {
            var builder = new ResourceGraphBuilder();
            builder.Singular("Profile", x => { });

            var root = builder.Build(new ResourceOptions());
            root.Children.Should().HaveCount(1);
            var singular = root.Children.Single();
            singular.Type.Should().Be(ResourceType.Singular);
            singular.Name.Should().Be("Profile");
        }

        [Fact]
        public void url_path_should_be_based_on_resource_name_by_default()
        {
            var singular = BuildResource(root =>
            {
                root.Singular("Profile", profile => { });
            });

            singular.Url.Should().Be("profile");
        }

        [Fact]
        public void should_use_custom_url_path_when_specified()
        {
            var singular = BuildResource(root =>
            {
                root.Singular("profile", products =>
                {
                    products.UrlPath("myprofile");
                });
            });

            singular.Url.Should().Be("myprofile");
        }

        [Fact]
        public void should_throw_if_custom_url_path_invalid()
        {
            Action action = () => BuildResource(root =>
            {
                root.Singular("Profile", profile =>
                {
                    profile.UrlPath("my*()profile");
                });
            });
            action.ShouldThrow<ArgumentException>()
                .WithMessage("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.*")
                .Which.ParamName.Should().Be("path");
        }

        /// <summary>
        /// Configures resources using supplied action and returns the first child resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResource(Action<ResourceGraphBuilder> configure)
        {
            var builder = new ResourceGraphBuilder();
            configure(builder);
            var root = builder.Build(new ResourceOptions());
            return root.Children.Single();
        }
    }
}