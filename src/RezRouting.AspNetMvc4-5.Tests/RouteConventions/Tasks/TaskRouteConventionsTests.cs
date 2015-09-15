using System.Collections.Generic;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.AspNetMvc.Tests.Infrastructure.Assertions;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Products.Product;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Profile;
using RezRouting.Resources;
using RezRouting.Tests.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Tasks
{
    public class TaskRouteConventionsTests : ConfigurationTestsBase
    {
        private static readonly Dictionary<string, Resource> Resources;

        static TaskRouteConventionsTests()
        {
            Resources = BuildResources(root =>
            {
                root.Extension(new TaskRouteConventions());
                root.Collection("Products", products =>
                {
                    products.Controller<CreateProductController>();
                    products.Controller<EditProductsController>();
                    products.Items(product =>
                    {
                        product.Controller<DeleteProductController>();
                        product.Controller<EditProductController>();
                    });
                });
                root.Singular("Profile", profile =>
                {
                    profile.Controller<DeleteProfileController>();
                    profile.Controller<EditProfileController>();
                });
            });
        }

        [Fact]
        public void should_map_collection_task_edit_routes()
        {
            var products = Resources["Products"];
            products.ShouldContainMvcRoute("CreateProduct.Edit", typeof(CreateProductController), "Edit", "GET", "create");
            products.ShouldContainMvcRoute("EditProducts.Edit", typeof(EditProductsController), "Edit", "GET", "edit");
        }

        [Fact]
        public void should_map_collection_task_handle_routes()
        {
            var products = Resources["Products"];
            products.ShouldContainMvcRoute("CreateProduct.Handle", typeof(CreateProductController), "Handle", "POST", "create");
            products.ShouldContainMvcRoute("EditProducts.Handle", typeof(EditProductsController), "Handle", "POST", "edit");
        }

        [Fact]
        public void should_map_collection_item_task_edit_routes()
        {
            var product = Resources["Products.Product"];
            product.ShouldContainMvcRoute("EditProduct.Edit", typeof(EditProductController), "Edit", "GET", "edit");
            product.ShouldContainMvcRoute("DeleteProduct.Edit", typeof(DeleteProductController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_collection_item_task_handle_routes()
        {
            var product = Resources["Products.Product"];
            product.ShouldContainMvcRoute("EditProduct.Handle", typeof(EditProductController), "Handle", "POST", "edit");
            product.ShouldContainMvcRoute("DeleteProduct.Handle", typeof(DeleteProductController), "Handle", "POST", "delete");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            var profile = Resources["Profile"];
            profile.ShouldContainMvcRoute("EditProfile.Edit", typeof(EditProfileController), "Edit", "GET", "edit");
            profile.ShouldContainMvcRoute("DeleteProfile.Edit", typeof(DeleteProfileController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            var profile = Resources["Profile"];
            profile.ShouldContainMvcRoute("EditProfile.Handle", typeof(EditProfileController), "Handle", "POST", "edit");
            profile.ShouldContainMvcRoute("DeleteProfile.Handle", typeof(DeleteProfileController), "Handle", "POST", "delete");
        }
    }
}