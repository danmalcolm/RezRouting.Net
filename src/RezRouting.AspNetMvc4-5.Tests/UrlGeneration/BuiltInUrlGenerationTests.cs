using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.Tests.Configuration;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.UrlGeneration
{
    public class BuiltInUrlGenerationTests : ConfigurationTestsBase
    {
        private readonly UrlHelper helper;

        public BuiltInUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var routes = new RouteCollection();
            BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Index", "GET", "", MvcAction.For((ProductsController c) => c.Index()));
                    products.Items(product =>
                    {
                        product.Route("Show", "GET", "", MvcAction.For((ProductsController c) => c.Show(0)));
                    });
                });
                root.MapMvcRoutes(routes);
            });
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
            string url = helper.Action("Show", "Products", new { id = 12345 });
            url.Should().Be("/products/12345");
        }

        [Fact]
        public void should_generate_full_url_with_protocol_and_host_name()
        {
            string url = helper.Action("index", "Products", null, "https", "www.example.org");
            url.Should().Be("https://www.example.org/products");
        }

        public class ProductsController : Controller
        {
            public ActionResult Index()
            {
                return Content("");
            }

            public ActionResult Show(int id)
            {
                return null;
            }
        }
    }
}