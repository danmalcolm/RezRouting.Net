using System;
using FluentAssertions;

namespace RezRouting2.Tests.Infrastructure.Assertions
{
    public static class RouteAssertionExtensions
    {
        public static void ShouldBeConfiguredAs(this Route route, string name, Type controllerType, string action, string httpMethod, string path)
        {
            route.Name.Should().Be(name);
            route.ControllerType.Should().Be(controllerType);
            route.Action.Should().Be(action);
            route.HttpMethod.Should().Be(httpMethod);
            route.Path.Should().Be(path);
        }
    }
}