﻿using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests configuration of additional RouteTypes at mapper level - shared
    /// by all resources
    /// </summary>
    public class SharedCustomRouteTests
    {
        [Theory, PropertyData("CustomRoutesExpectations")]
        public void ShouldMapCustomRoutesToAvailableActions(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> CustomRoutesExpectations
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
                    
                    configuration.AddRoute(search);
                    configuration.AddRoute(kick);
                    configuration.AddRoute(bust);
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