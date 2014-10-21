using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class AreaUrlGenerationTests
    {
        private readonly UrlHelper helper;

        public AreaUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var routes = new RouteCollection();
            var area1RouteMapper = CrudResourceModel.Configure(a =>
            {
                a.BaseName("Area1");
                a.BasePath("area1");
            });
            area1RouteMapper.MapMvcRoutes(routes, area: "Area1");

            var area2RouteMapper = CrudResourceModel.Configure(a =>
            {
                a.BaseName("Area2");
                a.BasePath("area2");
            });
            area2RouteMapper.MapMvcRoutes(routes, area: "Area2");

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