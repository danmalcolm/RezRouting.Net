using System.Collections.Generic;
using RezRouting.Tests.RouteMapping.TestControllers.Session;
using RezRouting.Tests.Shared.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class SingularRouteUrlTests
    {
        [Theory, PropertyData("StandardRouteUrlExpectations")]
        public void ShouldGetStandardRouteUrls(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardRouteUrlExpectations
        {
            get
            {
                var root = new RootResourceBuilder();
                root.Singular(session => session.HandledBy<SessionController>());

                return new UrlExpectations(root.MapRoutes())
                    .ForRoute("Session.Show", new {httpMethod = "GET"}, "/session")
                    .ForAction("session#show", new {httpMethod = "GET"}, "/session")
                    .ForRoute("Session.New", new {httpMethod = "GET"}, "/session/new")
                    .ForAction("session#new", new {httpMethod = "GET"}, "/session/new")
                    .ForRoute("Session.Create", new {httpMethod = "POST"}, "/session")
                    .ForAction("session#create", new {httpMethod = "POST"}, "/session")
                    .ForRoute("Session.Edit", new {httpMethod = "GET"}, "/session/edit")
                    .ForAction("session#edit", new {httpMethod = "GET"}, "/session/edit")
                    .ForRoute("Session.Update", new {httpMethod = "PUT"}, "/session")
                    .ForAction("session#update", new {httpMethod = "PUT"}, "/session")
                    .ForRoute("Session.Delete", new {httpMethod = "DELETE"}, "/session")
                    .ForAction("session#destroy", new {httpMethod = "DELETE"}, "/session")
                    .AsPropertyData();
            }
        }
    }
}
