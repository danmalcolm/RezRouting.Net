using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.TestControllers.Orders;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class DefaultRouteNameConventionTests
    {
        private readonly DefaultRouteNameConvention convention = new DefaultRouteNameConvention();
        private readonly RouteType routeType;

        public DefaultRouteNameConventionTests()
        {
            routeType = new RouteType("Index", new[] {ResourceType.Collection}, CollectionLevel.Collection, "Index", "",
                    StandardHttpMethod.Get, 0);
        }

        [Fact]
        public void ShouldCreateNameForTopLevelResourceWithSingleController()
        {
            convention.GetRouteName(new[] { "Users" }, routeType, typeof(UsersController), false)
                .Should().Be("Users.Index");
        }

        [Fact]
        public void ShouldCreateNameForNestedLevelResourceWithSingleController()
        {
            convention.GetRouteName(new[] { "Orders", "Notes" }, routeType, typeof(NotesController), false)
                .Should().Be("Orders.Notes.Index");
        }

        [Fact]
        public void ShouldIncludeControllerNameWhenRouteMappedToMultipleControllers()
        {
            convention.GetRouteName(new[] { "Users" }, routeType, typeof(SomethingController), true)
                .Should().Be("Users.Something.Index");

            convention.GetRouteName(new[] { "Users" }, routeType, typeof(SomethingElseController), true)
                .Should().Be("Users.SomethingElse.Index");
        }

        public class SomethingController
        {
            
        }

        public class SomethingElseController
        {

        }
    }
}