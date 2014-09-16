using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class QueryStringRouteMappingTests
    {
        [Theory, PropertyData("StandardCollectionRouteExpectations")]
        public void ShouldSelectRouteBasedOnQueryString(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionRouteExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Configure(configuration =>
                {
                    // 2 routes - have same URL path, with qs param to differentiate
                    var custom1 = new RouteType("Custom1", new[] { ResourceType.Collection }, "Custom1", StandardHttpMethod.Get, 9, 
                        customize: x =>
                        {
                            x.QueryStringValues(new {variation = "1"});
                            x.PathSegment = "custom";
                            x.CollectionLevel = CollectionLevel.Item;
                        });
                    var custom2 = new RouteType("Custom2", new[] {ResourceType.Collection}, "Custom2", StandardHttpMethod.Get, 9,
                        customize: x =>
                        {
                            x.QueryStringValues(new { variation = "2" });
                            x.PathSegment = "custom";
                            x.CollectionLevel = CollectionLevel.Item;
                        });
                    configuration.AddRouteType(custom1);
                    configuration.AddRouteType(custom2);
                });
                mapper.Collection(samples => samples.HandledBy<SamplesController>());

                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("GET /samples", "Samples.Index", "Samples#Index")
                    .ExpectMatch("GET /samples/123/custom?variation=1", "Samples.Custom1", "Samples#Custom1", new { id = "123" })
                    .ExpectMatch("GET /samples/123/custom?variation=2", "Samples.Custom2", "Samples#Custom2", new { id = "123" })
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("StandardCollectionUrlExpectations")]
        public void ShouldIncludeQueryStringInUrls(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> StandardCollectionUrlExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Configure(configuration =>
                {
                    // 2 routes - have same URL path, with qs param to differentiate
                    var custom1 = new RouteType("Custom1", new[] {ResourceType.Collection}, "Custom1", StandardHttpMethod.Get, 9,
                        customize: x =>
                        {
                            x.QueryStringValues(new { variation = "1" });
                            x.PathSegment = "custom";
                            x.CollectionLevel = CollectionLevel.Item;
                        });
                    var custom2 = new RouteType("Custom2", new[] { ResourceType.Collection }, "Custom2", StandardHttpMethod.Get, 9,
                        customize: x =>
                        {
                            x.QueryStringValues(new { variation = "2" });
                            x.PathSegment = "custom";
                            x.CollectionLevel = CollectionLevel.Item;
                        });

                    configuration.AddRouteTypes(custom1, custom2);
                });
                mapper.Collection(samples => samples.HandledBy<SamplesController>());
                mapper.Collection(users => users.HandledBy<UsersController>());

                return new UrlExpectations(mapper.MapRoutes())
                    .ForRoute("Samples.Index", new { httpMethod = "GET" }, "/samples")
// TODO - Is it possible to always include querystring values when generating outbound URLs - maybe constraint should always pass?
//                    .ForRoute("Samples.Custom1", new { httpMethod = "GET", id = "123" }, "/samples/123/custom?variation=1")
//                    .ForRoute("Samples.Custom2", new { httpMethod = "GET", id = "123" }, "/samples/123/custom?variation=2")
                    .ForAction("Samples#Custom1", new { httpMethod = "GET", id = "123", variation = "1" }, "/samples/123/custom?variation=1")
                    .ForRoute("Samples.Custom1", new { httpMethod = "GET", id = "123", variation = "1" }, "/samples/123/custom?variation=1")
                    .ForAction("Samples#Custom2", new { httpMethod = "GET", id = "123", variation = "2" }, "/samples/123/custom?variation=2")
                    .ForRoute("Samples.Custom2", new { httpMethod = "GET", id = "123", variation = "2" }, "/samples/123/custom?variation=2")
                    .ForAction("Samples#Custom1", new { httpMethod = "GET", id = "123", variation = "1", other = "2" }, "/samples/123/custom?variation=1&other=2")
                    .ForRoute("Samples.Custom1", new { httpMethod = "GET", id = "123", variation = "1", other = "2" }, "/samples/123/custom?variation=1&other=2")
                    .ForAction("Samples#Custom2", new { httpMethod = "GET", id = "123", variation = "2", other = "2" }, "/samples/123/custom?variation=2&other=2")
                    .ForRoute("Samples.Custom2", new { httpMethod = "GET", id = "123", variation = "2", other = "2" }, "/samples/123/custom?variation=2&other=2")
                    .ForAction("Users#Edit", new { httpMethod = "GET", id = "123" }, "/users/123/edit")
                    .ForRoute("Users.Edit", new { httpMethod = "GET", id = "123" }, "/users/123/edit")
                    .AsPropertyData();
            }
        }

        public class SamplesController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Custom1(string id)
            {
                return null;
            }

            public ActionResult Custom2(string id)
            {
                return null;
            }
        }
        
        
    }
}