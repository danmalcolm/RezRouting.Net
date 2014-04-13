using System.Linq;
using System.Web.UI;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
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
    }
}