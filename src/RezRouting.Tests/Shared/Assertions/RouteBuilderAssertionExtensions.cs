using System.Linq;
using FluentAssertions;
using RezRouting.Routing;

namespace RezRouting.Tests.Shared.Assertions
{
    public static class ResourceBuilderAssertionExtensions
    {
        public static void ShouldMapRoutesWithControllerActions(this RootResourceBuilder builder, params string[] expectedControllerActions)
        {
            var routes = builder.MapRoutes();
            var expected = expectedControllerActions.Select(ControllerActionInfo.Parse).ToArray();
            routes.OfType<ResourceActionRoute>().Select(r => new ControllerActionInfo(r.Defaults))
                .Should().BeEquivalentTo(expected);
        }

        public static void ShouldMapRoutesWithNames(this RootResourceBuilder builder, params string[] expectedNames)
        {
            var routes = builder.MapRoutes();
            routes.OfType<ResourceActionRoute>().Select(r => r.Name).Should().BeEquivalentTo(expectedNames);
        }

        public static void ShouldMapRoutesWithUrls(this RootResourceBuilder builder, params string[] expectedUrls)
        {
            var routes = builder.MapRoutes();
            routes.OfType<ResourceActionRoute>().Select(r => r.Url).Should().BeEquivalentTo(expectedUrls);
        }
    }
}