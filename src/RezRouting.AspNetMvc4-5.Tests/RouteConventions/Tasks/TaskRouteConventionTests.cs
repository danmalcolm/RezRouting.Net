using System;
using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Tasks
{
    public class TaskRouteConventionTests
    {
        private readonly Resource collection;
        private readonly Resource singular;
        private readonly UrlPathSettings pathSettings = new UrlPathSettings(CaseStyle.None);

        public TaskRouteConventionTests()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => {});
            builder.Singular("Profile", profile => {});
            var root = builder.Build();
            collection = root.Children.Single(x => x.Name == "Products");
            singular = root.Children.Single(x => x.Name == "Profile");
        }

        private CustomValueCollection CreateConventionData(Type controllerType)
        {
            var data = new CustomValueCollection();
            ConventionDataExtensions.AddControllerTypes(data, new[] { controllerType });
            return data;
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof (EditProductsController));

            var route = convention
                .Create(collection, data, pathSettings, new CustomValueCollection())
                .Single();
            
            route.Path.Should().Be("Edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof(CreateProductController));

            var route = convention
                .Create(collection, data, pathSettings, new CustomValueCollection())
                .Single();

            route.Path.Should().Be("Create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof (CreateProductController));

            var route = convention
                .Create(collection, data, new UrlPathSettings(CaseStyle.Lower), new CustomValueCollection())
                .Single();

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof(EditProductsController));

            var routes = convention
                .Create(singular, data, pathSettings, new CustomValueCollection());

            routes.Should().BeEmpty();
        }
    }
}