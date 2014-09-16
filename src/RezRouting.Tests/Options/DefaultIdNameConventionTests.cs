using FluentAssertions;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests.Options
{
    public class DefaultIdNameConventionTests
    {
        private string resourceName = "Thing";

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
        }

        [Fact]
        public void ShouldUseCustomNameForIdAsAncestorIfSpecified()
        {
            var convention = new DefaultIdNameConvention(idName: "code");
            string name = convention.GetIdNameAsAncestor(resourceName);
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