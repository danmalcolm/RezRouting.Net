﻿using System.Linq;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class CollectionRouteConventionTests
    {
        private readonly Resource collection;

        public CollectionRouteConventionTests()
        {
            var conventionBuilder = new TaskRouteConventionBuilder();
            var builder = new ResourcesBuilder();
            builder.RouteConventions(conventionBuilder.Build());
            builder.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.HandledBy<EditProductsController>();
            });
            var model = builder.Build();
            collection = model.Resources.Single();
        }

        [Fact]
        public void should_map_collection_display_route()
        {
            collection.ShouldContainRoute("Index", typeof(ListProductsController), "Index", "GET", "");
        }

        [Fact]
        public void should_map_collection_task_edit_routes()
        {
            collection.ShouldContainRoute("CreateProduct.Edit", typeof(CreateProductController), "Edit", "GET", "create");
            collection.ShouldContainRoute("EditProducts.Edit", typeof(EditProductsController), "Edit", "GET", "edit");
        }

        [Fact]
        public void should_map_collection_task_handle_routes()
        {
            collection.ShouldContainRoute("CreateProduct.Handle", typeof(CreateProductController), "Handle", "POST", "create");
            collection.ShouldContainRoute("EditProducts.Handle", typeof(EditProductsController), "Handle", "POST", "edit");
        }
    }
}