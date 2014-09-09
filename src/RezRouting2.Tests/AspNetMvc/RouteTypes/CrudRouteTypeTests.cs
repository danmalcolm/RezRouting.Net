using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting2.AspNetMvc.RouteTypes;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes
{
    public class CrudRouteTypeTests
    {
        [Fact]
        public void should_create_route_for_each_route_type_supported_by_controller()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
            mapper.Collection("Products", products => products.HandledBy<ProductsController>());

            var resources = mapper.Build().ToList();

            var collection = resources.Single();
            var expected = new[]
            {
                new {FullName = "Products.New"}
            };
            collection.Routes.ShouldAllBeEquivalentTo(expected);
        } 
    }

    public class ProductsController
    {
        public ActionResult Index()
        {
            return null;
        }

        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create()
        {
            return null;
        }
    }
}