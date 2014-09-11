using System.Linq;
using System.Web.Routing;
using FluentAssertions;

namespace RezRouting2.Tests.Infrastructure.Assertions
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

//        public static void ShouldNotContainRoutesWithNames(this RouteCollection routes, params string[] expectedNames)
//        {
//            routes.OfType<ResourceActionRoute>().Select(r => r.Name).Should().NotBeEquivalentTo(expectedNames);
//        }
//
//        public static void ShouldContainRoutesWithUrls(this RouteCollection routes, params string[] expectedUrls)
//        {
//            routes.OfType<ResourceActionRoute>().Select(r => r.Url).Should().BeEquivalentTo(expectedUrls);
//        }
    }
}