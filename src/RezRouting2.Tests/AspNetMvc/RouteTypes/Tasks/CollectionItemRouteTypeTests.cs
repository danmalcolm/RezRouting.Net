using System.Linq;
using RezRouting2.AspNetMvc.RouteTypes.Tasks;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Products.Product;
using RezRouting2.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class CollectionItemRouteTypeTests
    {
        private readonly Resource resource;

        public CollectionItemRouteTypeTests()
        {
            var scheme = new TaskRouteScheme();
            var mapper = new RouteMapper();
            mapper.RouteTypes(scheme.RouteTypes);
            mapper.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.HandledBy<DisplayProductController>();
                    product.HandledBy<DeleteProductController>();
                    product.HandledBy<EditProductController>();
                });
            });
            resource = mapper.Build().Single().Children.Single();
        }

        [Fact]
        public void should_map_singular_display_route()
        {
            resource.ShouldContainRoute("Show", typeof(DisplayProductController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            resource.ShouldContainRoute("EditProduct.Edit", typeof(EditProductController), "Edit", "GET", "Edit");
            resource.ShouldContainRoute("DeleteProduct.Edit", typeof(DeleteProductController), "Edit", "GET", "Delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            resource.ShouldContainRoute("EditProduct.Handle", typeof(EditProductController), "Handle", "POST", "Edit");
            resource.ShouldContainRoute("DeleteProduct.Handle", typeof(DeleteProductController), "Handle", "POST", "Delete");
        }
    }
}