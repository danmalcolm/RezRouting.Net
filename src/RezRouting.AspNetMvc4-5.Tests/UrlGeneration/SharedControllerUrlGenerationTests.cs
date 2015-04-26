using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.AspNetMvc5.Tests.UrlGeneration
{
    public class SharedControllerUrlGenerationTests
    {
        private readonly UrlHelper urlHelper;

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

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);
            urlHelper = new UrlHelper(context, routes);
        }

        [Fact]
        private void built_in_url_generation_should_get_route_identified_by_additional_route_values()
        {
            string url1 = urlHelper.Action("Index", "Comments", 
                new { id = 12345, parentType = "Product" });
            url1.Should().Be("/products/12345/comments");

            string url2 = urlHelper.Action("Index", "Comments", 
                new { id = 12345, parentType = "Manufacturer" });
            url2.Should().Be("/manufacturers/12345/comments");

            string url3 = urlHelper.Action("Index", "Comments",
                new { id = 12345, parentType = "Supplier" });
            url3.Should().Be("/suppliers/12345/comments");
        }

        [Fact]
        private void custom_url_generation_should_get_route_identified_by_additional_route_values()
        {
            string url1 = urlHelper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Product" });
            url1.Should().Be("/products/12345/comments");

            string url2 = urlHelper.ResourceUrl<CommentsController>("Index",
                new { id = 12345, parentType = "Manufacturer" });
            url2.Should().Be("/manufacturers/12345/comments");

            string url3 = urlHelper.ResourceUrl<CommentsController>("Index",
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