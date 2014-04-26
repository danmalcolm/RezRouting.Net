using System.Collections.Generic;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class MultipleControllerRouteMappingTests
    {

        [Theory, PropertyData("MultipleControllerRouteExpectations")]
        public void ShouldCustomizeRoutesForEachController(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> MultipleControllerRouteExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Configure(c => c.ClearRouteTypes());
                var commandEditRouteType = new RouteType("EditCommand", 
                    new[] {ResourceType.Collection}, CollectionLevel.Item, "Edit",
                    "edit", "GET", 1, 
                    includeController: (type,index) => typeof(ICommandController).IsAssignableFrom(type),
                    customize: settings =>
                    {
                        string command = settings.ControllerType.Name.Replace("Controller", "").ToLowerInvariant();
                        settings.QueryStringValues(new { command = command });
                    });
                var commandHandleRouteType = new RouteType("HandleCommand",
                    new[] { ResourceType.Collection }, CollectionLevel.Item, "Handle",
                    "edit", "POST", 1,
                    includeController: (type, index) => typeof(ICommandController).IsAssignableFrom(type),
                    customize: settings =>
                    {
                        string command = settings.ControllerType.Name.Replace("Controller", "").ToLowerInvariant();
                        settings.QueryStringValues(new { command = command });
                    });
                mapper.Configure(config => config.AddRoutes(commandEditRouteType, commandHandleRouteType));
                mapper.Collection(albums => albums.HandledBy<ProductsController, RenameController,UpdateCostsController>());
                
                return new MappingExpectations(mapper.MapRoutes())
                    .ExpectMatch("GET /products/123/edit?command=rename", "Products.EditCommand.Rename", "Rename#Edit", new { id = "123" })
                    .ExpectMatch("POST /products/123/edit?command=rename", "Products.HandleCommand.Rename", "Rename#Handle", new { id = "123" })
                    .AsPropertyData();
            }
        }

        public class ProductsController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }
        }

        public interface ICommandController
        {
            
        }

        public class RenameController : Controller, ICommandController
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

        public class UpdateCostsController : Controller, ICommandController
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