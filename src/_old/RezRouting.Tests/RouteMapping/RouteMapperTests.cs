using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Session;
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
            
            routes.ShouldContainRoutesWithNames("Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete");
        }

        [Fact]
        public void ShouldBaseRouteNamesForCollectionResourceOnPluralName()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users => users.HandledBy<UsersController>());
            var routes = mapper.MapRoutes();

            routes.ShouldContainRoutesWithNames("Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete");
        }

        [Fact]
        public void ShouldBaseRouteNamesForSingularResourceOnSingularName()
        {
            var mapper = new RouteMapper();
            mapper.Singular(users => users.HandledBy<SessionController>());
            var routes = mapper.MapRoutes();

            routes.ShouldContainRoutesWithNames("Session.Show", "Session.New", "Session.Create", "Session.Edit", "Session.Update", "Session.Delete");
        }

        [Fact]
        public void ShouldMapUsingSpecialRouteType()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users => users.HandledBy<UsersController>());
            var routes = mapper.MapRoutes();

            routes.Should().ContainItemsAssignableTo<ResourceActionRoute>();
        }
    }
}