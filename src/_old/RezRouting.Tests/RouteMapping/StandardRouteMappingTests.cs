using System.Collections.Generic;
using RezRouting.Tests.Infrastructure.Expectations;
using RezRouting.Tests.Infrastructure.TestControllers.Session;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class StandardRouteMappingTests
    {
        [Theory, PropertyData("StandardCollectionRouteExpectations")]
        public void ShouldMapStandardCollectionRoutes(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionRouteExpectations
        {
            get
            {
                var builder = new RouteMapper();
                builder.Collection(users => users.HandledBy<UsersController>());

                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET /users", "Users.Index", "Users#Index")
                    .ExpectMatch("GET /users/123", "Users.Show", "Users#Show", new { id = "123" })
                    .ExpectMatch("GET /users/new", "Users.New", "Users#New")
                    .ExpectMatch("POST /users", "Users.Create", "Users#Create")
                    .ExpectMatch("GET /users/123/edit", "Users.Edit", "Users#Edit", new { id = "123" })
                    .ExpectMatch("PUT /users/123", "Users.Update", "Users#Update", new { id = "123" })
                    .ExpectMatch("DELETE /users/123", "Users.Delete", "Users#Destroy", new { id = "123" })
                    .AsPropertyData();
            }
        }
        
        [Theory, PropertyData("StandardSingularRouteExpectations")]
        public void StandardSingularRoute(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardSingularRouteExpectations
        {
            get
            {
                var builder = new RouteMapper();
                builder.Singular(session => session.HandledBy<SessionController>());
                
                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET /session", "Session.Show", "Session#Show")
                    .ExpectMatch("GET /session/new", "Session.New", "Session#New")
                    .ExpectMatch("POST /session", "Session.Create", "Session#Create")
                    .ExpectMatch("GET /session/edit", "Session.Edit", "Session#Edit")
                    .ExpectMatch("PUT /session", "Session.Update", "Session#Update")
                    .ExpectMatch("DELETE /session", "Session.Delete", "Session#Destroy")
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("StandardCollectionRoutesAtRootLevelExpectations")]
        public void ShouldMapStandardCollectionRoutesAtRootLevel(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionRoutesAtRootLevelExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Collection(home =>
                {
                    home.HandledBy<UsersController>();
                    home.CustomUrlPath("");
                });
                return MappingExpectations.For(mapper)
                    .ExpectMatch("GET /", "Users.Index", "Users#Index")
                    .ExpectMatch("GET /123", "Users.Show", "Users#Show", new { id = "123" })
                    .ExpectMatch("GET /new", "Users.New", "Users#New")
                    .ExpectMatch("POST /", "Users.Create", "Users#Create")
                    .ExpectMatch("GET /123/edit", "Users.Edit", "Users#Edit", new { id = "123" })
                    .ExpectMatch("PUT /123", "Users.Update", "Users#Update", new { id = "123" })
                    .ExpectMatch("DELETE /123", "Users.Delete", "Users#Destroy", new { id = "123" })
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("StandardSingularRoutesAtRootLevelExpectations")]
        public void ShouldMapStandardSingularRoutesAtHomeLevel(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardSingularRoutesAtRootLevelExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Singular(home =>
                {
                    home.HandledBy<SessionController>();
                    home.CustomUrlPath("");
                });
                return MappingExpectations.For(mapper)
                    .ExpectMatch("GET /", "Session.Show", "Session#Show")
                    .ExpectMatch("GET /new", "Session.New", "Session#New")
                    .ExpectMatch("POST /", "Session.Create", "Session#Create")
                    .ExpectMatch("GET /edit", "Session.Edit", "Session#Edit")
                    .ExpectMatch("PUT /", "Session.Update", "Session#Update")
                    .ExpectMatch("DELETE /", "Session.Delete", "Session#Destroy")
                    .AsPropertyData();
            }
        }
    }
}
