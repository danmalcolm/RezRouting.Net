using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class RequestValueConstraintRouteMappingTests
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
                    var custom1 = new RouteType("Custom1", new[] { ResourceType.Collection },
                        CollectionLevel.Item, "Custom1", "custom", StandardHttpMethod.Get, 9, new { variation = "1" });
                    var custom2 = new RouteType("Custom2", new[] { ResourceType.Collection },
                        CollectionLevel.Item, "Custom2", "custom", StandardHttpMethod.Get, 9, new { variation = "2" });
                    
                    configuration.AddRoute(custom1);
                    configuration.AddRoute(custom2);
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
        public void ShouldIncludeQueryStringParamsWhenCreatingUrls(UrlExpectation expectation)
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
                    var custom1 = new RouteType("Custom1", new[] { ResourceType.Collection },
                        CollectionLevel.Item, "Custom1", "custom", StandardHttpMethod.Get, 9, new { variation = "1" });
                    var custom2 = new RouteType("Custom2", new[] { ResourceType.Collection },
                        CollectionLevel.Item, "Custom2", "custom", StandardHttpMethod.Get, 9, new { variation = "2" });

                    configuration.AddRoute(custom1);
                    configuration.AddRoute(custom2);
                });
                mapper.Collection(samples => samples.HandledBy<SamplesController>());

                return new UrlExpectations(mapper.MapRoutes())
                    .ForRoute("Samples.Index", new { httpMethod = "GET" }, "/samples")
                    .ForAction("Samples#Custom1", new { httpMethod = "GET", id = "123", variation = "1" }, "/samples/123/custom?variation=1")
                    .ForRoute("Samples.Custom1", new { httpMethod = "GET", id = "123", variation = "1" }, "/samples/123/custom?variation=1")
                    .ForAction("Samples#Custom2", new { httpMethod = "GET", id = "123", variation = "2" }, "/samples/123/custom?variation=2")
                    .ForRoute("Samples.Custom2", new { httpMethod = "GET", id = "123", variation = "2" }, "/samples/123/custom?variation=2")
                    .ForAction("Samples#Custom1", new { httpMethod = "GET", id = "123", variation = "1", other = "2" }, "/samples/123/custom?variation=1&other=2")
                    .ForRoute("Samples.Custom1", new { httpMethod = "GET", id = "123", variation = "1", other = "2" }, "/samples/123/custom?variation=1&other=2")
                    .ForAction("Samples#Custom2", new { httpMethod = "GET", id = "123", variation = "2", other = "2" }, "/samples/123/custom?variation=2&other=2")
                    .ForRoute("Samples.Custom2", new { httpMethod = "GET", id = "123", variation = "2", other = "2" }, "/samples/123/custom?variation=2&other=2")
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
