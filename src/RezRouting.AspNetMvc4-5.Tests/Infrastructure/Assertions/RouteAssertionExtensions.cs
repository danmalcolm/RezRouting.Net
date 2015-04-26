using FluentAssertions;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.Tests.Infrastructure.Assertions
{
    public static class RouteAssertionExtensions
    {
        public static void ShouldBeConfiguredAs(this Route route, string name, IResourceRouteHandler handler, string httpMethod, string path)
        {
            route.Name.Should().Be(name);
            route.Handler.Should().Be(handler);
            route.HttpMethod.Should().Be(httpMethod);
            route.Path.Should().Be(path);
        }
    }
}