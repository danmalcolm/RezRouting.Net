using System.Linq;
using System.Web.Routing;
using FluentAssertions;

namespace RezRouting.Tests.Infrastructure.Assertions.AspNetMvc
{
    public static class RouteCollectionAssertionExtensions
    {
        public static void ShouldContainOnly(this RouteCollection routes, params string[] expectedNames)
        {
            routes.OfType<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["Name"])
                .OfType<string>()
                .Should().BeEquivalentTo(expectedNames);
        }
    }
}