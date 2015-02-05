using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Tests.AspNetMvc.TestModels.Crud;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products.Product;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class RouteModelIndexTests
    {
        [Fact]
        public void should_index_routes()
        {
            var builder = TestCrudResourceModel.Configure();
            var routes = new RouteCollection();
            builder.ApplyRouteConventions(new CrudRouteConventions());
            builder.MapMvcRoutes(routes);

            var index = new RouteModelIndex(routes);

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