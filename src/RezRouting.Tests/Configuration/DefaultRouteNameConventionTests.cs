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
            convention.GetRouteName(new[] { "Users" }, "Index", typeof(UsersController), false)
                .Should().Be("Users.Index");
        }

        [Fact]
        public void ShouldCreateNameForNestedLevelResourceWithSingleController()
        {
            convention.GetRouteName(new[] { "Orders", "Notes" }, "Index", typeof(NotesController), false)
                .Should().Be("Orders.Notes.Index");
        }
        
        [Fact]
        public void ShouldIncludeControllerNameWhenSpecified()
        {
            convention.GetRouteName(new[] { "Users" }, "Index", typeof(SomethingController), true)
                .Should().Be("Users.Something.Index");
        }

        public class SomethingController
        {
            
        }
    }
}
