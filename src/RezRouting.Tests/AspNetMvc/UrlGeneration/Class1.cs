using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Products;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class RouteModelIndexTests
    {
        [Fact]
        public void should_index_routes()
        {
            var mapper = CrudResourceModel.Configure();
            var routeCollection = new RouteCollection();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), routeCollection);

            var index = new RouteModelIndex(routeCollection);

            var routeModel = index.Get(typeof (ProductsController), "Index");
            routeModel.FullName.Should().Be("Products.Index");

            routeModel = index.Get(typeof(ProductController), "Show");
            routeModel.FullName.Should().Be("Products.Product.Show");
        }

        [Fact]
        public void should_not_break_when_indexing_non_resource_routes()
        {
            var routes = new RouteCollection();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var index = new RouteModelIndex(routes);
        }
         
    }
}