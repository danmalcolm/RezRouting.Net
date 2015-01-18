using System.Linq;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.Tests.Infrastructure.Assertions.AspNetMvc;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class CollectionRouteConventionTests
    {
        private readonly Resource collection;

        public CollectionRouteConventionTests()
        {
            var taskConventions = new TaskRouteConventions();
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.HandledBy<EditProductsController>();
            });
            var options = new ResourceOptions();
            options.AddRouteConventions(new TaskRouteConventions());
            var root = builder.Build(options);
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