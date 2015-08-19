using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.Tests.Infrastructure.Assertions;
using RezRouting.AspNetMvc.Tests.RouteConventions.Crud.TestControllers;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Configuration;
using RezRouting.Tests.Infrastructure;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Crud
{
    public class CrudRouteConventionsTests : ConfigurationTestsBase
    {
        private static readonly Dictionary<string, Resource> Resources;

        static CrudRouteConventionsTests()
        {
            Resources = BuildResources(root =>
            {
                root.ApplyRouteConventions(new CrudRouteConventions());
                root.Collection("Products", products =>
                {
                    products.Controller<ProductsController>();
                    products.Items(product => product.Controller<ProductController>());
                });
                root.Singular("Profile", profile => profile.Controller<ProfileController>());
            });
        }

        [Theory]
        [InlineData("Index", typeof(ProductsController), "Index", "GET", "")]
        [InlineData("New", typeof(ProductsController), "New", "GET", "new")]
        [InlineData("Create", typeof(ProductsController), "Create", "POST", "")]
        public void should_create_collection_level_routes(string fullName, Type controllerType, 
            string action, string httpMethod, string path)
        {
            var collection = Resources["Products"];
            collection.ShouldContainMvcRoute(fullName, controllerType, action, httpMethod, path);
        }

        [Theory]
        [InlineData("Show", typeof(ProductController), "Show", "GET", "")]
        [InlineData("Edit", typeof(ProductController), "Edit", "GET", "edit")]
        [InlineData("Update", typeof(ProductController), "Update", "PUT", "")]
        [InlineData("Delete", typeof(ProductController), "Delete", "DELETE", "")]
        public void should_create_collection_item_level_routes(string fullName, Type controllerType,
            string action, string httpMethod, string path)
        {
            var item = Resources["Products.Product"];
            item.ShouldContainMvcRoute(fullName, controllerType, action, httpMethod, path);
        }

        [Theory]
        [InlineData("New", typeof(ProfileController), "New", "GET", "new")]
        [InlineData("Create", typeof(ProfileController), "Create", "POST", "")]
        [InlineData("Show", typeof(ProfileController), "Show", "GET", "")]
        [InlineData("Edit", typeof(ProfileController), "Edit", "GET", "edit")]
        [InlineData("Update", typeof(ProfileController), "Update", "PUT", "")]
        [InlineData("Delete", typeof(ProfileController), "Delete", "DELETE", "")]
        public void should_create_singular_level_routes(string fullName, Type controllerType,
            string action, string httpMethod, string path)
        {
            var collection = Resources["Profile"];
            collection.ShouldContainMvcRoute(fullName, controllerType, action, httpMethod, path);
        }

        [Fact]
        public void should_not_map_resource_level_routes_on_different_level_resources()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products =>
            {
                products.Items(product => product.Controller<ProductsController>());
            });
            builder.ApplyRouteConventions(new CrudRouteConventions());
            var root = builder.Build();
            var routes = root.Children.Expand().SelectMany(x => x.Routes);
            routes.Should().BeEmpty();
        }
    }
}