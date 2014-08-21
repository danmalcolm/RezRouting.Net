using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class ResourceBuilderChildBuilderTests
    {
        [Fact]
        public void should_not_build_children_if_none_configured()
        {
            var builder = new SingularResourceBuilder("Profile");
            var resource = builder.Build();
            resource.Children.Should().BeEmpty();
        }

        [Fact]
        public void should_build_nested_singular_resource_within_singular()
        {
            var builder = new SingularResourceBuilder("Profile");
            builder.Singular("User", user => { });
            
            var resource = builder.Build();

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Level.Should().Be(ResourceLevel.Singular);
            child.Name.Should().Be("User");
        }

        [Fact]
        public void should_build_nested_collection_resource_within_singular()
        {
            var builder = new SingularResourceBuilder("Profile");
            builder.Collection("Logins", logins => { });

            var resource = builder.Build();

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Level.Should().Be(ResourceLevel.Collection);
            child.Name.Should().Be("Logins");
        }


        [Fact]
        public void should_build_child_resources_with_reference_to_parent()
        {
            var builder = new SingularResourceBuilder("Profile");
            builder.Singular("User", singular => { });
            
            var resource = builder.Build();
            
            var child = resource.Children.Single();
            child.Parent.Should().Be(resource);
        }
    }
}