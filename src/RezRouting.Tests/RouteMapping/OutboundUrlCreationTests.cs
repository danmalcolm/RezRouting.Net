using System.Collections.Generic;
using RezRouting.Tests.Infrastructure.Expectations;
using RezRouting.Tests.Infrastructure.TestControllers.Session;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests resource route URL creation
    /// </summary>
    public class OutboundUrlCreationTests
    {
        [Theory, PropertyData("StandardCollectionUrlExpectations")]
        public void StandardCollectionUrls(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionUrlExpectations
        {
            get
            {
                var root = new RootResourceBuilder();
                root.Collection(users => users.HandledBy<UsersController>());

                return new UrlExpectations(root.MapRoutes())
                    .ForRoute("Users.Index", new {httpMethod = "GET"}, "/users")
                    .ForAction("users#index", new {httpMethod = "GET"}, "/users")
                    .ForRoute("Users.Show", new {httpMethod = "GET", id = "123"}, "/users/123")
                    .ForAction("users#show", new {httpMethod = "GET", id = "123"}, "/users/123")
                    .ForRoute("Users.New", new {httpMethod = "GET"}, "/users/new")
                    .ForAction("users#new", new {httpMethod = "GET"}, "/users/new")
                    .ForRoute("Users.Create", new {httpMethod = "POST"}, "/users")
                    .ForAction("users#create", new {httpMethod = "POST"}, "/users")
                    .ForRoute("Users.Edit", new {httpMethod = "GET", id = "123"}, "/users/123/edit")
                    .ForAction("users#edit", new {httpMethod = "GET", id = "123"}, "/users/123/edit")
                    .ForRoute("Users.Update", new {httpMethod = "PUT", id = "123"}, "/users/123")
                    .ForAction("users#update", new {httpMethod = "PUT", id = "123"}, "/users/123")
                    .ForRoute("Users.Delete", new {httpMethod = "DELETE", id = "123"}, "/users/123")
                    .ForAction("users#destroy", new {httpMethod = "DELETE", id = "123"}, "/users/123")
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("StandardSingularUrlExpectations")]
        public void StandardSingularUrls(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardSingularUrlExpectations
        {
            get
            {
                var root = new RootResourceBuilder();
                root.Singular(session => session.HandledBy<SessionController>());

                return new UrlExpectations(root.MapRoutes())
                    .ForRoute("Session.Show", new { httpMethod = "GET" }, "/session")
                    .ForAction("session#show", new { httpMethod = "GET" }, "/session")
                    .ForRoute("Session.New", new { httpMethod = "GET" }, "/session/new")
                    .ForAction("session#new", new { httpMethod = "GET" }, "/session/new")
                    .ForRoute("Session.Create", new { httpMethod = "POST" }, "/session")
                    .ForAction("session#create", new { httpMethod = "POST" }, "/session")
                    .ForRoute("Session.Edit", new { httpMethod = "GET" }, "/session/edit")
                    .ForAction("session#edit", new { httpMethod = "GET" }, "/session/edit")
                    .ForRoute("Session.Update", new { httpMethod = "PUT" }, "/session")
                    .ForAction("session#update", new { httpMethod = "PUT" }, "/session")
                    .ForRoute("Session.Delete", new { httpMethod = "DELETE" }, "/session")
                    .ForAction("session#destroy", new { httpMethod = "DELETE" }, "/session")
                    .AsPropertyData();
            }
        }
    }
}