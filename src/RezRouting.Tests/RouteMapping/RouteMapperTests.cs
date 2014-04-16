using System.Linq;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    public class RouteMapperTests
    {
        [Fact]
        public void ShouldAddRoutesToRouteCollection()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users => users.HandledBy<UsersController>());
            var routes = mapper.MapRoutes();

            var expectedRouteNames = new[]
            {
                "Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete"
            };
            routes.Cast<ResourceActionRoute>().Select(x => x.Name).Should().Contain(expectedRouteNames);
        }

        [Fact]
        public void ShouldMapUsingSpecialTypeOfRoutes()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users => users.HandledBy<UsersController>());
            var routes = mapper.MapRoutes();

            routes.Should().ContainItemsAssignableTo<ResourceActionRoute>();
        }
    }
}