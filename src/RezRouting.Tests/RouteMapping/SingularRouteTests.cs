using System.Collections.Generic;
using RezRouting.Tests.RouteMapping.TestControllers.Session;
using RezRouting.Tests.Shared.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class SingularRouteTests
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
                builder.Singular(session => session.HandledBy<SessionController>());
                
                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET session", "Session.Show", "Session#Show")
                    .ExpectMatch("GET session/new", "Session.New", "Session#New")
                    .ExpectMatch("POST session", "Session.Create", "Session#Create")
                    .ExpectMatch("GET session/edit", "Session.Edit", "Session#Edit")
                    .ExpectMatch("PUT session", "Session.Update", "Session#Update")
                    .ExpectMatch("DELETE session", "Session.Delete", "Session#Destroy")
                    .AsPropertyData();
            }
        }
    }
}
