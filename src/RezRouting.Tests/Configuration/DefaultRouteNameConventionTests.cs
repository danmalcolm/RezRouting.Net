using System.Linq;
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
        private readonly RouteType index;

        public DefaultRouteNameConventionTests()
        {
            index = StandardRouteTypes.Build().Single(x => x.Name == "Index");
        }

        [Fact]
        public void ShouldCreateNameForTopLevelResourceWithSingleController()
        {
            convention.GetRouteName(new[] { "Users" }, index, typeof(UsersController), false)
                .Should().Be("Users.Index");
        }

        [Fact]
        public void ShouldCreateNameForNestedLevelResourceWithSingleController()
        {
            convention.GetRouteName(new[] { "Orders", "Notes" }, index, typeof(NotesController), false)
                .Should().Be("Orders.Notes.Index");
        }

        [Fact]
        public void ShouldIncludeControllerNameWhenRouteMappedToMultipleControllers()
        {
            convention.GetRouteName(new[] { "Users" }, index, typeof(SomethingController), true)
                .Should().Be("Users.Something.Index");

            convention.GetRouteName(new[] { "Users" }, index, typeof(SomethingElseController), true)
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