using System.Collections.Generic;
using RezRouting.Tests.RouteMapping.TestControllers.Users;
using RezRouting.Tests.Shared.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class CollectionRouteTests
    {
        [Theory, PropertyData("StandardRouteExpectations")]
        public void ShouldMapStandardRoutes(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardRouteExpectations
        {
            get
            {
                var builder = new RootResourceBuilder();
                builder.Collection(users => users.HandledBy<UsersController>());
                
                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET users", "Users.Index", "Users#Index")
                    .ExpectMatch("GET users/123", "Users.Show", "Users#Show", new { id = "123" })
                    .ExpectMatch("GET users/new", "Users.New", "Users#New")
                    .ExpectMatch("POST users", "Users.Create", "Users#Create")
                    .ExpectMatch("GET users/123/edit", "Users.Edit", "Users#Edit", new { id = "123" })
                    .ExpectMatch("PUT users/123", "Users.Update", "Users#Update", new { id = "123" })
                    .ExpectMatch("DELETE users/123", "Users.Delete", "Users#Destroy", new { id = "123" })
                    .AsPropertyData();
            }
        }

        public void ShouldNotMapExcludedActions()
        {
            var builder = new RootResourceBuilder();
            builder.Collection(users =>
            {
                users.HandledBy<UsersController>();
                users.Include();
            });

        }
    }
}