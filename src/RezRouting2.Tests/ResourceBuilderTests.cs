using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class ResourceBuilderTests
    {
        [Fact]
        public void should_build_singular_resource_with_root_name()
        {
            var builder = new SingularResourceBuilder("Profile");
            
            var resource = builder.Build();
            
            resource.Should().NotBeNull();
            resource.Level.Should().Be(ResourceLevel.Singular);
            resource.Name.Should().Be("Profile");
        }

        [Fact]
        public void should_set_url_path_based_on_resource_name()
        {
            var builder = new SingularResourceBuilder("Profile");
            
            var resource = builder.Build();

            resource.UrlPath.Should().Be("Profile");
        }

    }
}