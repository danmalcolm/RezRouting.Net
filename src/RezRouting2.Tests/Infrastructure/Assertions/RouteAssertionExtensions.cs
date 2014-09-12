using System.Web.Routing;
using FluentAssertions;

namespace RezRouting2.Tests.Infrastructure.Assertions
{
    public static class RouteAssertionExtensions
    {
        public static void ShouldBeBasedOnRoute(this RouteData routeData, string name)
        {
            routeData.Should().NotBeNull();
            routeData.Route.Should().BeOfType<System.Web.Routing.Route>("route should be of type System.Web.Routing.Route");
            var route = routeData.Route as System.Web.Routing.Route;
            route.DataTokens["Name"].Should().Be(name);
        }
    }
}