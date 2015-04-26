using System.Linq;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.Infrastructure.Assertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Tasks
{
    public class CollectionRouteConventionTests
    {
        private readonly Resource collection;

        public CollectionRouteConventionTests()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.HandledBy<EditProductsController>();
            });
            builder.ApplyRouteConventions(new TaskRouteConventions());
            var root = builder.Build();
            collection = root.Children.Single();
        }

        [Fact]
        public void should_map_collection_display_route()
        {
            collection.ShouldContainMvcRoute("Index", typeof(ListProductsController), "Index", "GET", "");
        }

        [Fact]
        public void should_map_collection_task_edit_routes()
        {
            collection.ShouldContainMvcRoute("CreateProduct.Edit", typeof(CreateProductController), "Edit", "GET", "create");
            collection.ShouldContainMvcRoute("EditProducts.Edit", typeof(EditProductsController), "Edit", "GET", "edit");
        }

        [Fact]
        public void should_map_collection_task_handle_routes()
        {
            collection.ShouldContainMvcRoute("CreateProduct.Handle", typeof(CreateProductController), "Handle", "POST", "create");
            collection.ShouldContainMvcRoute("EditProducts.Handle", typeof(EditProductsController), "Handle", "POST", "edit");
        }
    }
}