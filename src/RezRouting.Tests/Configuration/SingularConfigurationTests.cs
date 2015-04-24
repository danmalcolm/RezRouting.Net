using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class SingularConfigurationTests
    {
        [Fact]
        public void should_build_singular_resource()
        {
            var builder = RootResourceBuilder.Create();
            builder.Singular("Profile", x => { });

            var root = builder.Build();
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
        public void should_use_custom_url_path_containing_directory_separator()
        {
            var singular = BuildResource(root =>
            {
                root.Singular("profile", products =>
                {
                    products.UrlPath("mystuff/profile");
                });
            });

            singular.Url.Should().Be("mystuff/profile");
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
        private Resource BuildResource(Action<IRootResourceBuilder> configure)
        {
            var builder = RootResourceBuilder.Create();
            configure(builder);
            var root = builder.Build();
            return root.Children.Single();
        }
    }
}