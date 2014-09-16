using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Products;
using RezRouting.Tests.Infrastructure;
using RezRouting.AspNetMvc.UrlGeneration;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class UrlHelperExtensionsTests
    {
        private readonly UrlHelper helper;

        public UrlHelperExtensionsTests()
        {
            var context = TestRequestContextBuilder.Create();
            var mapper = CrudResourceModel.Configure();
            var routes = new RouteCollection();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), routes);
            helper = new UrlHelper(context, routes);
        }

        [Fact]
        public void should_generate_url_specified_by_controller_type_and_action()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index");
            url.Should().Be("/products");
        }

        [Fact]
        public void should_generate_url_specified_by_controller_type_action_and_values()
        {
            string url = helper.ResourceUrl(typeof(ProductController), "Show", new { id = "123" });
            url.Should().Be("/products/123");
        }

        [Fact]
        public void should_generate_url_for_POST_route_specified_by_controller_type_action_and_values()
        {
            string url = helper.ResourceUrl(typeof(ProductController), "Update", new { id = "123" });
            url.Should().Be("/products/123");
        }

        [Fact]
        public void should_not_generate_url_for_unrecognised_action()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index2");
            url.Should().BeNull();
        }
    }
}