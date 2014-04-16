using System.Collections.Generic;
using System.Collections.Specialized;
using RezRouting.Tests.Infrastructure.Expectations;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class HttpMethodOverrideTests
    {
        [Theory, PropertyData("HttpMethodOverrideExpectations")]
        public void ShouldSupportHttpMethodOverride(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> HttpMethodOverrideExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Collection(users => users.HandledBy<UsersController>());

                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("POST users/123", "Users.Delete", "Users#Destroy", new {id = "123"},
                        form: new NameValueCollection {{"X-HTTP-Method-Override", "DELETE"}},
                        desc: "DELETE via form method override")
                    .ExpectMatch("POST users/123", "Users.Delete", "Users#Destroy", new {id = "123"},
                        headers: new NameValueCollection {{"X-HTTP-Method-Override", "DELETE"}},
                        desc: "DELETE via HTTP header method override")
                    .ExpectMatch("POST users/123", "Users.Delete", "Users#Destroy", new {id = "123"},
                        form: new NameValueCollection {{"_method", "DELETE"}},
                        desc: "DELETE via _method form value")
                    .ExpectMatch("POST users/123", "Users.Update", "Users#Update", new {id = "123"},
                        form: new NameValueCollection {{"X-HTTP-Method-Override", "PUT"}},
                        desc: "PUT via form method override")
                    .ExpectMatch("POST users/123", "Users.Update", "Users#Update", new {id = "123"},
                        headers: new NameValueCollection {{"X-HTTP-Method-Override", "PUT"}},
                        desc: "PUT via HTTP header method override")
                    .ExpectMatch("POST users/123", "Users.Update", "Users#Update", new {id = "123"},
                        form: new NameValueCollection {{"_method", "PUT"}},
                        desc: "PUT via _method form value")
                    .AsPropertyData();
            }
        }
    }
}