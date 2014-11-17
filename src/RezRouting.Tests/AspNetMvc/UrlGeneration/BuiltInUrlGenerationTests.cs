using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class BuiltInUrlGenerationTests
    {
        private readonly UrlHelper helper;

        public BuiltInUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var mapper = CrudResourceModel.Configure();
            var routes = new RouteCollection();
            mapper.MapMvcRoutes(routes);
            helper = new UrlHelper(context, routes);
        }

        [Fact]
        public void should_generate_collection_url_specified_by_controller_and_action()
        {
            string url = helper.Action("index", "Products", null);
            url.Should().Be("/products");
        }

        [Fact]
        public void should_generate_collection_item_url_using_id_route_value()
        {
            string url = helper.Action("Show", "Product", new { id = 12345 });
            url.Should().Be("/products/12345");
        }

        [Fact]
        public void should_generate_full_url_with_protocol_and_host_name()
        {
            string url = helper.Action("index", "Products", null, "https", "www.example.org");
            url.Should().Be("https://www.example.org/products");
        }
    }
}