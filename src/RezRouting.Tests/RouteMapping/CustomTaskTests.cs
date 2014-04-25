using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Tests.Infrastructure.Expectations;

namespace RezRouting.Tests.RouteMapping
{
    public class CustomTaskTests
    {
//        [Theory, PropertyData("CustomRoutesExpectations")]
        public void MappingCustomRoutes(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> CustomRoutesExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Collection(albums =>
                {
                    albums.HandledBy<AlbumsController>();
                });
               
                
                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("GET /albums", "Albums.Index", "Albums#Index")
                    .ExpectMatch("GET /albums/123/edit?task=rename", "Albums.Rename.Edit", "Rename#Edit", new { id = "123" })
                    .ExpectMatch("POST /albums/123/edit?task=rename", "Albums.Rename.Handle", "Rename#Handle", new { id = "123" })
                    .AsPropertyData();
            }
        }

        public class AlbumsController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }
        }

        public class RenameController : Controller
        {
            public ActionResult Edit(string id)
            {
                return null;
            }

            public ActionResult Handle(string id, object command)
            {
                return null;
            }
        }

        public class UpdateCostsController : Controller
        {
            public ActionResult Edit(string id)
            {
                return null;
            }

            public ActionResult Handle(string id, object command)
            {
                return null;
            }
        }
    }
}