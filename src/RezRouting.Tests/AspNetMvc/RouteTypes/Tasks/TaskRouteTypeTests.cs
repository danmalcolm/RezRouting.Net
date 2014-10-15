using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteTypes.Tasks;
using RezRouting.Options;
using RezRouting.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Products;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class TaskRouteTypeTests
    {
        private readonly Resource collection;
        private readonly Resource singular;
        private readonly UrlPathFormatter pathFormatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.None));

        public TaskRouteTypeTests()
        {
            var routeMapper = new RouteMapper();
            routeMapper.Collection("Products", products => {});
            routeMapper.Singular("Profile", profile => {});
            var model = routeMapper.Build();
            collection = model.Resources.Single(x => x.Name == "Products");
            singular = model.Resources.Single(x => x.Name == "Profile");
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var route = routeType.BuildRoute(collection, typeof (EditProductsController), pathFormatter);
            
            route.Path.Should().Be("Edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");
            
            var route = routeType.BuildRoute(collection, typeof(CreateProductController), pathFormatter);

            route.Path.Should().Be("Create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");
            var formatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.Lower));

            var route = routeType.BuildRoute(collection, typeof(CreateProductController), formatter);

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var routeType = new TaskRouteType("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var route = routeType.BuildRoute(singular, typeof(EditProductsController), pathFormatter);

            route.Should().BeNull();
        }
    }
}