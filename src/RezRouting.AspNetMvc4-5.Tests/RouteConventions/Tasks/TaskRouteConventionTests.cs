using System;
using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.AspNetMvc.Tests.RouteConventions.Tasks.TestControllers.Products;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Tasks
{
    public class TaskRouteConventionTests : ConfigurationTestsBase
    {
        private readonly UrlPathSettings pathSettings;

        public TaskRouteConventionTests()
        {
            pathSettings = new UrlPathSettings(CaseStyle.Lower);
        }

        private CustomValueCollection CreateConventionData(Type controllerType)
        {
            var data = new CustomValueCollection();
            data.AddControllerTypes(new[] { controllerType });
            return data;
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof (EditProductsController));

            var route = convention
                .Create(resourceData, new CustomValueCollection(), data, pathSettings, new CustomValueCollection())
                .Single();
            
            route.Path.Should().Be("edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof(CreateProductController));
            var route = convention
                .Create(resourceData, new CustomValueCollection(), data, pathSettings, new CustomValueCollection())
                .Single();

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null); var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof (CreateProductController));
            
            var route = convention
                .Create(resourceData, new CustomValueCollection(), data, new UrlPathSettings(CaseStyle.Lower), new CustomValueCollection())
                .Single();

            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_level()
        {
            var resourceData = new SingularData();
            resourceData.Init("Profile", null); 
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var data = CreateConventionData(typeof(EditProductsController));

            var routes = convention
                .Create(resourceData, new CustomValueCollection(), data, pathSettings, new CustomValueCollection());

            routes.Should().BeEmpty();
        }
    }
}