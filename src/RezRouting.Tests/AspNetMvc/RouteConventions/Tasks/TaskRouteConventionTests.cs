using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc;
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
        private readonly UrlPathSettings pathSettings = new UrlPathSettings(CaseStyle.None);

        public TaskRouteConventionTests()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products => {});
            builder.Singular("Profile", profile => {});
            var root = builder.Build(new ResourceOptions());
            collection = root.Children.Single(x => x.Name == "Products");
            singular = root.Children.Single(x => x.Name == "Profile");
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");

            var route = convention
                .Create(collection, new[] { MvcController.Create<EditProductsController>() }, pathSettings)
                .Single();
            
            route.Path.Should().Be("Edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            
            var route = convention
                .Create(collection, new [] { MvcController.Create<CreateProductController>() }, pathSettings)
                .Single();

            route.Path.Should().Be("Create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            
            var route = convention
                .Create(collection, new[] { MvcController.Create<CreateProductController>() }, new UrlPathSettings(CaseStyle.Lower))
                .Single();

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");

            var routes = convention
                .Create(singular, new [] { MvcController.Create<EditProductsController>() }, pathSettings);

            routes.Should().BeEmpty();
        }
    }
}