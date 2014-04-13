using System.Linq;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.RouteMapping.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    public class RootResourceBuilderTests
    {
        [Fact]
        public void ShouldAddRoutesToRouteCollection()
        {
            var root = new RootResourceBuilder();
            root.Collection(users => users.HandledBy<UsersController>());
            var routes = root.MapRoutes();

            var expectedRouteNames = new[]
            {
                "Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete"
            };
            routes.Cast<ResourceActionRoute>().Select(x => x.Name).Should().Contain(expectedRouteNames);
        }

        [Fact]
        public void ShouldMapUsingSpecialTypeOfRoutes()
        {
            var root = new RootResourceBuilder();
            root.Collection(users => users.HandledBy<UsersController>());
            var routes = root.MapRoutes();

            routes.Should().ContainItemsAssignableTo<ResourceActionRoute>();
        }

        [Fact]
        public void ShouldOnlyMapRoutesSpecifiedViaIncludeFilter()
        {
            var root = new RootResourceBuilder();
            root.Collection(users =>
            {
                users.Include("Index", "Show");
                users.HandledBy<UsersController>();
            });
            var routes = root.MapRoutes();

            var expectedRouteNames = new[]
            {
                "Users.Index", "Users.Show"
            };
            routes.Cast<ResourceActionRoute>().Select(x => x.Name).Should().BeEquivalentTo(expectedRouteNames);
        }

        [Fact]
        public void ShouldNotMapRoutesSpecifiedViaExcludeFilter()
        {
            var root = new RootResourceBuilder();
            root.Collection(users =>
            {
                users.Exclude("Delete");
                users.HandledBy<UsersController>();
            });
            var routes = root.MapRoutes();

            var expectedRouteNames = new[]
            {
                "Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update"
            };
            routes.Cast<ResourceActionRoute>().Select(x => x.Name).Should().BeEquivalentTo(expectedRouteNames);
        }
    }
}