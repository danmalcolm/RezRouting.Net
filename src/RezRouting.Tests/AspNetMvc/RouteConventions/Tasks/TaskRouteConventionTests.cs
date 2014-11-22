using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Products;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class TaskRouteConventionTests
    {
        private readonly Resource collection;
        private readonly Resource singular;
        private readonly UrlPathFormatter pathFormatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.None));

        public TaskRouteConventionTests()
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
            var convention = new TaskRouteConvention("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var route = convention
                .Create(collection, new [] { typeof (EditProductsController) }, pathFormatter)
                .Single();
            
            route.Path.Should().Be("Edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");
            
            var route = convention
                .Create(collection, new [] { typeof(CreateProductController) }, pathFormatter)
                .Single();

            route.Path.Should().Be("Create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");
            var formatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.Lower));

            var route = convention
                .Create(collection, new [] { typeof(CreateProductController) }, formatter)
                .Single();

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceLevel.Collection, "Edit", "GET");

            var routes = convention
                .Create(singular, new [] { typeof(EditProductsController) }, pathFormatter);

            routes.Should().BeEmpty();
        }
    }
}