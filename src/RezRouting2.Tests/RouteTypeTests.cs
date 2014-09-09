using System;
using System.Linq;
using FluentAssertions;
using RezRouting2.Options;
using Xunit;

namespace RezRouting2.Tests
{
    public class RouteTypeTests
    {
        
        [Fact]
        public void should_create_route_based_on_resource_and_handler_type()
        {
            Resource mappedResource;
            Type mappedHandlerType;
            var routeType = new RouteType("RouteType1", (resource, type, route) =>
            {
                mappedResource = resource;
                mappedHandlerType = type;
                route.Configure("name1", "action1", "GET", "path1");
            });
            var context = new RouteMappingContext(new[] { routeType }, new OptionsBuilder().Build());
            var builder = new CollectionBuilder("Products");
            builder.HandledBy<TestController>();
            var resource1 = builder.Build(context);

            var route1 = resource1.Routes.Single();
            route1.Resource.Should().Be(resource1);
            route1.Name.Should().Be("name1");
            route1.Action.Should().Be("action1");
            route1.HttpMethod.Should().Be("GET");
            route1.Path.Should().Be("path1");
            route1.ControllerType.Should().Be(typeof(TestController));
        }

        private class TestController
        {
        }
    }
}