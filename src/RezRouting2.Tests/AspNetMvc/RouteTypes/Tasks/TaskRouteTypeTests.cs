﻿using System.Linq;
using FluentAssertions;
using RezRouting2.AspNetMvc.RouteTypes.Tasks;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Products;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class TaskRouteTypeTests
    {
        private readonly Resource collection;
        private Resource singular;

        public TaskRouteTypeTests()
        {
            var routeMapper = new RouteMapper();
            routeMapper.Collection("Products", products => {});
            routeMapper.Singular("Profile", profile => {});
            var resources = routeMapper.Build();
            collection = resources.Single(x => x.Name == "Products");
            singular = resources.Single(x => x.Name == "Profile");
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var route = routeType.BuildRoute(collection, typeof (EditProductsController));
            
            route.Path.Should().Be("Edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");
            
            var route = routeType.BuildRoute(collection, typeof(CreateProductController));

            route.Path.Should().Be("Create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var route = routeType.BuildRoute(singular, typeof(EditProductsController));

            route.Should().BeNull();
        }
    }
}