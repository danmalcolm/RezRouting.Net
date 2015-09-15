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
        private readonly ConfigurationContext context;
        private readonly ConfigurationOptions options;

        public TaskRouteConventionTests()
        {
            context = new ConfigurationContext(new CustomValueCollection());
            options = new ConfigurationOptions(new UrlPathSettings(), new DefaultIdNameFormatter());
        }

        [Fact]
        public void should_trim_resource_name_from_path()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new[] { typeof(EditProductsController) });
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            
            convention.Extend(resourceData, context, options);

            resourceData.Routes.Should().HaveCount(1);
            var route = resourceData.Routes.Single();
            route.Path.Should().Be("edit");
        }

        [Fact]
        public void should_trim_singular_version_of_collection_resource_name_from_path()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new[] { typeof(CreateProductController) });
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            
            convention.Extend(resourceData, context, options);

            resourceData.Routes.Should().HaveCount(1);
            var route = resourceData.Routes.Single();
            route.Path.Should().Be("create");
        }

        [Fact]
        public void should_format_task_path_using_settings()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new[] { typeof(CreateProductController) });
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            var options2 = new ConfigurationOptions(new UrlPathSettings(CaseStyle.Upper), options.IdNameFormatter);
            
            convention.Extend(resourceData, context, options2);

            resourceData.Routes.Should().HaveCount(1);
            var route = resourceData.Routes.Single();
            route.Path.Should().Be("CREATE");
        }

        [Fact]
        public void should_not_create_route_for_resource_with_different_type()
        {
            var resourceData = new SingularData();
            resourceData.Init("Profile", null);
            resourceData.ExtensionData.AddControllerTypes(new[] { typeof(EditProductsController) });
            var convention = new TaskRouteConvention("CollectionEdit", ResourceType.Collection, "Edit", "GET");
            
            convention.Extend(resourceData, context, options);

            resourceData.Routes.Should().BeEmpty();
        }
    }
}