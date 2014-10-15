using System.Linq;
using RezRouting.AspNetMvc.RouteTypes.Tasks;
using RezRouting.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Products.Product;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class CollectionItemRouteTypeTests
    {
        private readonly Resource resource;

        public CollectionItemRouteTypeTests()
        {
            var scheme = new TaskRouteTypeBuilder();
            var mapper = new RouteMapper();
            mapper.RouteTypes(scheme.Build());
            mapper.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.HandledBy<DisplayProductController>();
                    product.HandledBy<DeleteProductController>();
                    product.HandledBy<EditProductController>();
                });
            });
            var model = mapper.Build();
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