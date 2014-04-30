using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests configuration of additional RouteTypes configured at mapper level
    /// </summary>
    public class CustomRouteMappingTests
    {
        [Theory, PropertyData("SharedCustomRoutesExpectations")]
        public void ShouldMapSharedCustomRoutesForAllResources(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> SharedCustomRoutesExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Configure(configuration =>
                {
                    var search = new RouteType("Search", new[] { ResourceType.Collection },
                        CollectionLevel.Collection, "Search", "search", StandardHttpMethod.Get, 9);
                    var kick = new RouteType("Kick", new[] {ResourceType.Collection, ResourceType.Singular},
                        CollectionLevel.Item, "Kick", "kick", StandardHttpMethod.Post, 9);
                    var bust = new RouteType("Bust", new[] { ResourceType.Collection, ResourceType.Singular },
                        CollectionLevel.Item, "Bust", "bust", StandardHttpMethod.Delete, 9);
                    
                    configuration.AddRouteType(search);
                    configuration.AddRouteType(kick);
                    configuration.AddRouteType(bust);
                });
                mapper.Collection(asses => asses.HandledBy<AssesController>());
                mapper.Collection(donkeys => donkeys.HandledBy<DonkeysController>());
                
                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("GET /asses", "Asses.Index", "Asses#Index")
                    .ExpectMatch("GET /asses/search", "Asses.Search", "Asses#Search")
                    .ExpectMatch("POST /asses/123/kick", "Asses.Kick", "Asses#Kick", new { id = "123" })
                    .ExpectMatch("DELETE /asses/123/bust", "Asses.Bust", "Asses#Bust", new { id = "123" })
                    .ExpectMatch("GET /donkeys/search", "Donkeys.Search", "Donkeys#Search")
                    .ExpectNoMatch("GET /donkeys/123/kick")
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("CustomRoutesExpectations")]
        public void ShouldMapResourceLevelCustomRoutesOnResourceOnly(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> CustomRoutesExpectations
        {
            get
            {
                var mapper = new RouteMapper();

                var search = new RouteType("Search", new[] { ResourceType.Collection },
                        CollectionLevel.Collection, "Search", "search", StandardHttpMethod.Get, 9);
                var kick = new RouteType("Kick", new[] { ResourceType.Collection, ResourceType.Singular },
                    CollectionLevel.Item, "Kick", "kick", StandardHttpMethod.Post, 9);
                var bust = new RouteType("Bust", new[] { ResourceType.Collection, ResourceType.Singular },
                    CollectionLevel.Item, "Bust", "bust", StandardHttpMethod.Delete, 9);

                mapper.Collection(asses =>
                {
                    asses.HandledBy<AssesController>();
                    asses.Configure(config =>
                    {
                        config.AddRouteType(search);
                        config.AddRouteType(kick);
                        config.AddRouteType(bust);
                    });
                });
                mapper.Collection(donkeys => donkeys.HandledBy<DonkeysController>());

                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("GET /asses", "Asses.Index", "Asses#Index")
                    .ExpectMatch("GET /asses/search", "Asses.Search", "Asses#Search")
                    .ExpectMatch("POST /asses/123/kick", "Asses.Kick", "Asses#Kick", new { id = "123" })
                    .ExpectMatch("DELETE /asses/123/bust", "Asses.Bust", "Asses#Bust", new { id = "123" })
                    .ExpectNoMatch("GET /donkeys/search")
                    .AsPropertyData();
            }
        }

       
        public class AssesController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Search()
            {
                return null;
            }

            public ActionResult Kick()
            {
                return null;
            }

            public ActionResult Bust()
            {
                return null;
            }
        }

        public class DonkeysController : Controller
        {
            public ActionResult Search()
            {
                return null;
            }
        }

    }
}