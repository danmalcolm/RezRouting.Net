using System.Linq;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Products.Product;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.Assertions.AspNetMvc;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class CollectionItemRouteConventionTests
    {
        private readonly Resource resource;

        public CollectionItemRouteConventionTests()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.HandledBy<DisplayProductController>();
                    product.HandledBy<DeleteProductController>();
                    product.HandledBy<EditProductController>();
                });
            });
            var options = new ResourceOptions();
            options.AddRouteConventions(new TaskRouteConventions());
            var root = builder.Build(options);
            resource = root.Children.Single().Children.Single();
        }

        [Fact]
        public void should_map_singular_display_route()
        {
            resource.ShouldContainMvcRoute("Show", typeof(DisplayProductController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            resource.ShouldContainMvcRoute("EditProduct.Edit", typeof(EditProductController), "Edit", "GET", "edit");
            resource.ShouldContainMvcRoute("DeleteProduct.Edit", typeof(DeleteProductController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            resource.ShouldContainMvcRoute("EditProduct.Handle", typeof(EditProductController), "Handle", "POST", "edit");
            resource.ShouldContainMvcRoute("DeleteProduct.Handle", typeof(DeleteProductController), "Handle", "POST", "delete");
        }
    }
}