using FluentAssertions;
using RezRouting.Configuration;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class DefaultIdNameConventionTests
    {
        private ResourceName resourceName = new ResourceName("Thing");

        [Fact]
        public void ShouldUseIdWithoutResourceName()
        {
            var convention = new DefaultIdNameConvention();
            string name = convention.GetIdName(resourceName);
            name.Should().Be("id");
        }

        [Fact]
        public void ShouldUseIncludeResourceNameIfSpecified()
        {
            var convention = new DefaultIdNameConvention(fullNameForCurrent: true);
            string name = convention.GetIdName(resourceName);
            name.Should().Be("thingId");
        }

        [Fact]
        public void ShouldUseCustomNameIfSpecified()
        {
            var convention = new DefaultIdNameConvention(idName: "code");
            string name = convention.GetIdName(resourceName);
            name.Should().Be("code");
            name = convention.GetIdNameAsAncestor(resourceName);
            name.Should().Be("thingCode");
        }

        [Fact]
        public void ShouldUseFullCamelizedNameForIdAsAncestor()
        {
            var convention = new DefaultIdNameConvention();
            string name = convention.GetIdNameAsAncestor(resourceName);
            name.Should().Be("thingId");
        }
    }
}