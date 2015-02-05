using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products.Product;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Profile;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class AreaUrlGenerationTests
    {
        private readonly UrlHelper helper;

        private void ConfigureTestResources(ISingularConfigurator root)
        {
            root.Collection("Products", products =>
            {
                products.Route("Index", MvcAction.For((ProductsController c) => c.Index()), "GET", "");
                products.Items(product =>
                {
                    product.Route("Show", MvcAction.For((ProductController c) => c.Show(null)), "GET", "");
                });
            });
            root.Singular("Profile", profile => profile.HandledBy<ProfileController>());
        }

        public AreaUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var routes = new RouteCollection();
            var area1Root = RootResourceBuilder.Create("Area1");
            area1Root.UrlPath("area1");
            ConfigureTestResources(area1Root);
            area1Root.MapMvcRoutes(routes, area: "Area1");

            var area2Root = RootResourceBuilder.Create("Area2");
            area2Root.UrlPath("area2");
            ConfigureTestResources(area2Root);
            area2Root.MapMvcRoutes(routes, area: "Area2");

            helper = new UrlHelper(context, routes);
        }

        [Fact]
        public void should_generate_collection_urls_within_area_specified()
        {
            string url1 = helper.Action("Index", "Products", new { area = "Area1" });
            string url2 = helper.Action("Index", "Products", new { area = "Area2" });

            url1.Should().Be("/area1/products");
            url2.Should().Be("/area2/products");
        }

        [Fact]
        public void should_generate_collection_item_urls_within_area_specified()
        {
            string url1 = helper.Action("Show", "Product", new { area = "Area1", id = 12345 });
            string url2 = helper.Action("Show", "Product", new { area = "Area2", id = 12345 });

            url1.Should().Be("/area1/products/12345");
            url2.Should().Be("/area2/products/12345");
        }
    }
}