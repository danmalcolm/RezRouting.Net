using System;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration.Options;
using RezRouting.Tests.AspNetMvc.TestModels.Crud;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products.Product;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class UrlHelperExtensionsOptimizationTests
    {
        private readonly UrlHelper helper;
        private readonly UrlHelper helperUsingIndexedRoutes;
        
        public UrlHelperExtensionsOptimizationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var builder = TestCrudResourceModel.Configure();
            var options = new ResourceOptions();
            options.AddRouteConventions(new CrudRouteConventions());
            
            var collection1 = new RouteCollection();
            builder.MapMvcRoutes(options, collection1);
            helper = new UrlHelper(context, collection1);

            var collection2 = new RouteCollection();
            builder.MapMvcRoutes(options, collection2);
            helperUsingIndexedRoutes = new UrlHelper(context, collection2);

            UrlHelperExtensions.IndexRoutes(collection2);
        }

        [Fact]
        public void should_use_indexed_routes()
        {
            string url1 = helperUsingIndexedRoutes.ResourceUrl(typeof(ProductsController), "Index", null);
            url1.Should().Be("/products");
        }

        [Fact]
        public void should_use_indexed_route_ignoring_case_used_for_action()
        {
            string url1 = helperUsingIndexedRoutes.ResourceUrl(typeof(ProductsController), "index", null);
            string url2 = helperUsingIndexedRoutes.ResourceUrl(typeof(ProductsController), "Index", null);

            url1.Should().Be(url2);
        }

        [Fact]
        public void helper_using_index_should_generate_same_urls_as_helper_without_index()
        {
            EnsureUrlsMatch(typeof(ProductsController), "Index");
            EnsureUrlsMatch(typeof(ProductController), "Show", new { id = "123" });
            EnsureUrlsMatch(typeof(ProductController), "Update", new { id = "123" });
        }
        
        private void EnsureUrlsMatch(Type controllerType, string action, object routeValues = null)
        {
            string url1 = helper.ResourceUrl(controllerType, action, routeValues);
            string url2 = helperUsingIndexedRoutes.ResourceUrl(controllerType, action, routeValues);
            url2.Should().Be(url1);
        }
        
        [Fact]
        public void should_not_generate_url_for_unrecognised_action()
        {
            string url = helperUsingIndexedRoutes.ResourceUrl(typeof(ProductsController), "index2", null);
            url.Should().BeNull();
        }
    }
}