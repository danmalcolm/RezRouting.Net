using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.Tests
{
    public class RouteBuilderTests
    {
        [Fact]
        public void should_build_route_with_core_properties_configured()
        {
            var route = RouteBuilder.Create("Route1", typeof(TestController), "Action1", "GET", "test");

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

        [Fact]
        public void custom_properties_should_be_empty_if_not_configured()
        {
            var route = RouteBuilder.Create("Route1", typeof(TestController), "Action1", "GET", "test");

            route.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_include_copy_of_items_in_custom_properties_if_specified()
        {
            var data = new Dictionary<string, object> { {"key 1", "value 1" }};
            var route = RouteBuilder.Create("Route1", typeof(TestController), "Action1", "GET", "test", data);

            route.CustomProperties.ShouldBeEquivalentTo(new Dictionary<string,object> { { "key 1", "value 1"}});
            route.CustomProperties.Should().NotBeSameAs(data);
        }
        
        [Theory,
        InlineData(null, "Action1", "GET", "test"),
        InlineData("Route1", null, "GET", "test"),
        InlineData("Route1", "Action1", null, "test"),
        InlineData("Route1", "Action1", "GET", null)
        ]
        public void should_throw_if_key_properties_not_configured(string name, string action, string httpMethod, string path)
        {
            Action a = () => RouteBuilder.Create(name, typeof(TestController), action, httpMethod, path);

            a.ShouldThrow<ArgumentNullException>();
        }

        private class TestController
        {
            
        }
    }
}