using FluentAssertions;
using RezRouting.Configuration.Options;
using Xunit;

namespace RezRouting.Tests.Configuration.Options
{
    public class DefaultIdNameConventionTests
    {
        private string resourceName = "Thing";

        [Fact]
        public void ShouldUseIdWithoutResourceName()
        {
            var convention = new DefaultIdNameFormatter();
            string name = convention.GetIdName(resourceName);
            name.Should().Be("id");
        }

        [Fact]
        public void ShouldUseIncludeResourceNameIfSpecified()
        {
            var convention = new DefaultIdNameFormatter(fullNameForCurrent: true);
            string name = convention.GetIdName(resourceName);
            name.Should().Be("thingId");
        }

        [Fact]
        public void ShouldUseCustomNameIfSpecified()
        {
            var convention = new DefaultIdNameFormatter(idName: "code");
            string name = convention.GetIdName(resourceName);
            name.Should().Be("code");
        }

        [Fact]
        public void ShouldUseCustomNameForIdAsAncestorIfSpecified()
        {
            var convention = new DefaultIdNameFormatter(idName: "code");
            string name = convention.GetIdNameAsAncestor(resourceName);
            name.Should().Be("thingCode");
        }
        
        [Fact]
        public void ShouldUseFullCamelizedNameForIdAsAncestor()
        {
            var convention = new DefaultIdNameFormatter();
            string name = convention.GetIdNameAsAncestor(resourceName);
            name.Should().Be("thingId");
        }
    }
}