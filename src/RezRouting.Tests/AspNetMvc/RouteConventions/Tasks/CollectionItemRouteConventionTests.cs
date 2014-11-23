using System.Linq;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Products.Product;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class CollectionItemRouteConventionTests
    {
        private readonly Resource resource;

        public CollectionItemRouteConventionTests()
        {
            var taskConventions = new TaskRouteConventionBuilder();
            var builder = new ResourcesBuilder();
            builder.RouteConventions(taskConventions.Build());
            builder.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.HandledBy<DisplayProductController>();
                    product.HandledBy<DeleteProductController>();
                    product.HandledBy<EditProductController>();
                });
            });
            var model = builder.Build();
            resource = model.Resources.Single().Children.Single();
        }

        [Fact]
        public void should_map_singular_display_route()
        {
            resource.ShouldContainRoute("Show", typeof(DisplayProductController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            resource.ShouldContainRoute("EditProduct.Edit", typeof(EditProductController), "Edit", "GET", "edit");
            resource.ShouldContainRoute("DeleteProduct.Edit", typeof(DeleteProductController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            resource.ShouldContainRoute("EditProduct.Handle", typeof(EditProductController), "Handle", "POST", "edit");
            resource.ShouldContainRoute("DeleteProduct.Handle", typeof(DeleteProductController), "Handle", "POST", "delete");
        }
    }
}