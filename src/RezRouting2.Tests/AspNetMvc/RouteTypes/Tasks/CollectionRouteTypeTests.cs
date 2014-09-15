using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting2.AspNetMvc.RouteTypes.Tasks;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Products;
using RezRouting2.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class CollectionRouteTypeTests
    {
        private readonly RouteMapper mapper;
        private Resource collection;

        public CollectionRouteTypeTests()
        {
            var scheme = new TaskRouteScheme();
            mapper = new RouteMapper();
            mapper.RouteTypes(scheme.RouteTypes);
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.HandledBy<EditProductsController>();
            });
            collection = mapper.Build().Single();
        }

        [Fact]
        public void should_map_collection_display_route()
        {
            collection.ShouldContainRoute("Index", typeof(ListProductsController), "Index", "GET", "");
        }

        [Fact]
        public void should_map_collection_task_edit_routes()
        {
            collection.ShouldContainRoute("CreateProduct.Edit", typeof(CreateProductController), "Edit", "GET", "Create");
            collection.ShouldContainRoute("EditProducts.Edit", typeof(EditProductsController), "Edit", "GET", "Edit");
        }

        [Fact]
        public void should_map_collection_task_handle_routes()
        {
            collection.ShouldContainRoute("CreateProduct.Handle", typeof(CreateProductController), "Handle", "POST", "Create");
            collection.ShouldContainRoute("EditProducts.Handle", typeof(EditProductsController), "Handle", "POST", "Edit");
        }
    }
}