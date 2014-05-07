using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
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
                        CollectionLevel.Collection, "Search", StandardHttpMethod.Get, 9,
                        customize: settings => settings.PathSegment = "search");
                    var kick = new RouteType("Kick", new[] {ResourceType.Collection, ResourceType.Singular},
                        CollectionLevel.Item, "Kick", StandardHttpMethod.Post, 9,
                        customize: settings => settings.PathSegment = "kick");
                    var bust = new RouteType("Bust", new[] { ResourceType.Collection, ResourceType.Singular },
                        CollectionLevel.Item, "Bust", StandardHttpMethod.Delete, 9,
                        customize: settings => settings.PathSegment = "bust");
                    
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
                        CollectionLevel.Collection, "Search", StandardHttpMethod.Get, 9,
                        customize: settings => settings.PathSegment = "search");
                var kick = new RouteType("Kick", new[] { ResourceType.Collection, ResourceType.Singular },
                    CollectionLevel.Item, "Kick", StandardHttpMethod.Post, 9,
                        customize: settings => settings.PathSegment = "kick");
                var bust = new RouteType("Bust", new[] { ResourceType.Collection, ResourceType.Singular },
                    CollectionLevel.Item, "Bust", StandardHttpMethod.Delete, 9,
                        customize: settings => settings.PathSegment = "bust");

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