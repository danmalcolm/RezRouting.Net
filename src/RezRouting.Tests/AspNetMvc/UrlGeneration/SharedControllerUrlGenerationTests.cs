using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.UrlGeneration
{
    public class SharedControllerUrlGenerationTests
    {
        private readonly UrlHelper helper;
        private UrlHelper optimizedHelper;

        public SharedControllerUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var builder = RootResourceBuilder.Create();
            builder.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.IdNameAsAncestor("id");
                    product.CommentsCollection("Product");
                });
            });
            builder.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.IdNameAsAncestor("id");
                    manufacturer.CommentsCollection("Manufacturer");
                });
            });
            builder.Collection("Suppliers", suppliers =>
            {
                suppliers.Items(supplier =>
                {
                    supplier.IdNameAsAncestor("id");
                    supplier.CommentsCollection("Supplier");
                });
            });

            var collection1 = new RouteCollection();
            builder.MapMvcRoutes(collection1);
            helper = new UrlHelper(context, collection1);

            var collection2 = new RouteCollection();
            builder.MapMvcRoutes(collection2);
            optimizedHelper = new UrlHelper(context, collection2);

            UrlHelperExtensions.IndexRoutes(collection2);
        }

        [Fact]
        private void built_in_url_generation_should_get_route_identified_by_additional_route_values()
        {
            string url1 = helper.Action("Index", "Comments", 
                new { id = 12345, parentType = "Product" });
            url1.Should().Be("/products/12345/comments");

            string url2 = helper.Action("Index", "Comments", 
                new { id = 12345, parentType = "Manufacturer" });
            url2.Should().Be("/manufacturers/12345/comments");

            string url3 = helper.Action("Index", "Comments",
                new { id = 12345, parentType = "Supplier" });
            url3.Should().Be("/suppliers/12345/comments");
        }

        [Fact]
        private void custom_url_generation_should_get_route_identified_by_additional_route_values()
        {
            string url1 = helper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Product" });
            url1.Should().Be("/products/12345/comments");

            string url2 = helper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Manufacturer" });
            url2.Should().Be("/manufacturers/12345/comments");

            string url3 = helper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Supplier" });
            url3.Should().Be("/suppliers/12345/comments");
        }

        [Fact]
        private void optimized_custom_url_generation_should_get_route_identified_by_additional_route_values()
        {
            string url1 = optimizedHelper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Product" });
            url1.Should().Be("/products/12345/comments");

            string url2 = optimizedHelper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Manufacturer" });
            url2.Should().Be("/manufacturers/12345/comments");

            string url3 = optimizedHelper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Supplier" });
            url3.Should().Be("/suppliers/12345/comments");
        }
    }

    public class CommentsController : Controller
    {
        public ActionResult Index(string parentType, string id)
        {
            return null;
        }
    }

    public static class CommentResourceExtensions
    {
        public static void CommentsCollection(this ICollectionItemConfigurator item, string parentType)
        {
            item.Collection("Comments", comments =>
            {
                var routeValues = new CustomValueCollection { { "parentType", parentType } };
                var action = MvcAction.For((CommentsController x) => x.Index("", ""));
                comments.Route("Index", "GET", "", action, additionalRouteValues: routeValues);
            });
        }
    }

    
}