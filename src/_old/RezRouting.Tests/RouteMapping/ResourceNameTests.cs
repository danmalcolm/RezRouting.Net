using System;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class ResourceNameTests
    {
        [Fact]
        public void ShouldInitialiseWithSpecifiedSingularAndPluralValues()
        {
            var name = new ResourceName("thing", "thingys");
            name.Singular.Should().Be("thing");
            name.Plural.Should().Be("thingys");
        }

        [Fact]
        public void ShouldInitialisePluralBasedOnSingular()
        {
            var name = new ResourceName(singular: "Product");
            name.Singular.Should().Be("Product");
            name.Plural.Should().Be("Products");
        }

        [Fact]
        public void ShouldInitialiseSingularBasedOnPlural()
        {
            var name = new ResourceName(plural: "Products");
            name.Singular.Should().Be("Product");
            name.Plural.Should().Be("Products");
        }

        [Theory,
        InlineData("", ""),
        InlineData(null, null),
        InlineData(" ", " ")]
        public void ShouldThrowWithEmptyValues(string singular, string plural)
        {
            Action act = () => new ResourceName(singular, plural);
            act.ShouldThrow<ArgumentException>();
       }
    }
}