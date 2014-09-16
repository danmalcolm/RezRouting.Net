using System;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.Tests
{
    public class RouteBuilderTests
    {
        [Fact]
        public void should_not_create_route_if_not_configured()
        {
            var builder = new RouteBuilder(typeof(TestController));

            builder.Build().Should().BeNull();
        }

        [Fact]
        public void should_build_route_with_properties_configured()
        {
            var builder = new RouteBuilder(typeof(TestController));
            builder.Configure("Route1", "Action1", "GET", "test");
            var route = builder.Build();

            route.Should().NotBeNull();
            route.ShouldBeEquivalentTo(new
            {
                Name = "Route1", 
                ControllerType = typeof(TestController),
                Action = "Action1",
                HttpMethod = "GET",
                Path = "test"
            }, options => options.ExcludingMissingProperties());
        }

        [Theory,
        InlineData(null, "Action1", "GET", "test"),
        InlineData("Route1", null, "GET", "test"),
        InlineData("Route1", "Action1", null, "test"),
        InlineData("Route1", "Action1", "GET", null)
        ]
        public void should_throw_if_key_properties_not_configured(string name, string action, string httpMethod, string path)
        {
            var builder = new RouteBuilder(typeof(TestController));
            Action a = () => builder.Configure(name, action, httpMethod, path);

            a.ShouldThrow<ArgumentNullException>();
        }

        private class TestController
        {
            
        }
    }
}