using System.Collections.Generic;
using RezRouting.Tests.RouteMapping.TestControllers.Users;
using RezRouting.Tests.Shared.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests URL generation by collection resource routes
    /// </summary>
    public class CollectionRouteUrlTests
    {
        [Theory, PropertyData("StandardCollectionUrlsData")]
        public void StandardCollectionUrls(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionUrlsData
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
    }
}