using System.Linq;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.Routing;

namespace RezRouting.Tests.Infrastructure.Assertions
{
    public static class RouteCollectionAssertionExtensions
    {
        public static void ShouldContainRoutesWithControllerActions(this RouteCollection routes, params string[] expectedControllerActions)
        {
            var expected = expectedControllerActions.Select(ControllerActionInfo.Parse).ToArray();
            routes.OfType<ResourceActionRoute>().Select(r => new ControllerActionInfo(r.Defaults))
                .Should().BeEquivalentTo(expected);
        }

        public static void ShouldContainRoutesWithNames(this RouteCollection routes, params string[] expectedNames)
        {
            routes.OfType<ResourceActionRoute>().Select(r => r.Name).Should().BeEquivalentTo(expectedNames);
        }

        public static void ShouldContainRoutesWithUrls(this RouteCollection routes, params string[] expectedUrls)
        {
            routes.OfType<ResourceActionRoute>().Select(r => r.Url).Should().BeEquivalentTo(expectedUrls);
        }
    }
}