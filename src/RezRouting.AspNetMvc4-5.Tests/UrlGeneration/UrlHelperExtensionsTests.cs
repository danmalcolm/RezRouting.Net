﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Tests.Configuration;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.UrlGeneration
{
    public class UrlHelperExtensionsTests : ConfigurationTestsBase
    {
        private readonly UrlHelper helper;

        public UrlHelperExtensionsTests()
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
                        product.Route("Update", "POST", "", MvcAction.For((ProductsController c) => c.Update(null)));
                    });
                });
                root.MapMvcRoutes(routes);
            });
            helper = new UrlHelper(context, routes);
        }

        [Fact]
        public void should_generate_url_specified_by_controller_type_and_action()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index", null);
            url.Should().Be("/products");
        }

        [Fact]
        public void should_ignore_case_of_action_name()
        {
            helper.ResourceUrl(typeof(ProductsController), "index", null).Should().Be("/products");
            helper.ResourceUrl(typeof(ProductsController), "Index", null).Should().Be("/products");
            helper.ResourceUrl(typeof(ProductsController), "INDEX", null).Should().Be("/products");
        }

        [Fact]
        public void should_generate_url_specified_by_controller_type_as_type_parameter()
        {
            string url = helper.ResourceUrl<ProductsController>("Index", null);
            url.Should().Be("/products");
        }

        [Fact]
        public void should_generate_full_url_with_host_name()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index", null, hostName: "www.example.org");
            url.Should().Be("http://www.example.org/products");
        }

        [Fact]
        public void should_generate_full_url_with_host_name_and_protocol()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index", null, "https", "www.example.org");
            url.Should().Be("https://www.example.org/products");
        }

        [Fact]
        public void should_generate_url_specified_by_controller_type_action_and_values()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "Show", new { id = "123" });
            url.Should().Be("/products/123");
        }

        [Fact]
        public void should_generate_url_for_POST_route_specified_by_controller_type_action_and_values()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "Update", new { id = "123" });
            url.Should().Be("/products/123");
        }

        [Fact]
        public void should_not_generate_url_for_unrecognised_action()
        {
            string url = helper.ResourceUrl(typeof(ProductsController), "index2", null);
            url.Should().BeNull();
        }

        [Fact]
        public void should_not_break_due_to_ignore_routes_without_data_tokens()
        {
            var routes = helper.RouteCollection;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var route = routes.Last();
            routes.Remove(route);
            routes.Insert(0, route);

            string url = helper.ResourceUrl(typeof(ProductsController), "index", null);
            url.Should().Be("/products");
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

            public ActionResult Update(object value)
            {
                return null;
            }
        }
    }
}