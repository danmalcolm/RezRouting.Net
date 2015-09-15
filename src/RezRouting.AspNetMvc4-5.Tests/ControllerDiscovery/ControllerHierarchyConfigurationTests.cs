using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product.Reviews;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product.Reviews.Review;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.ControllerDiscovery
{
    public class ControllerHierarchyConfigurationTests : ConfigurationTestsBase
    {
        private static TestRouteConvention Convention;

        static ControllerHierarchyConfigurationTests()
        {
            Convention = new TestRouteConvention();
            BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", review =>
                        {
                            
                        });
                    });
                });
                root.ControllerHierarchy(typeof (HomeController).Assembly, typeof (HomeController).Namespace);
                root.Extension(Convention);
            });
        }

        
        [Fact]
        public void should_add_controllers_in_hierarchy_to_resource_when_applying_conventions()
        {
            Convention.GetControllerTypes("").Should().BeEquivalentTo(typeof (HomeController));
            Convention.GetControllerTypes("Products").Should().BeEquivalentTo(typeof (ProductIndexController));
            Convention.GetControllerTypes("Products.Product").Should().BeEquivalentTo(typeof (ProductDetailsController));
            Convention.GetControllerTypes("Products.Product.Reviews").Should().BeEquivalentTo(typeof (ReviewIndexController));
            Convention.GetControllerTypes("Products.Product.Reviews.Review").Should().BeEquivalentTo(typeof (ReviewDetailsController));
        }

        [Fact]
        public void should_not_include_controllers_belonging_to_child_resource_on_parent()
        {
            Convention.GetControllerTypes("").Should().NotContain(typeof(ProductIndexController));
         
            Convention.GetControllerTypes("Products").Should().NotContain(typeof (ProductDetailsController));
            Convention.GetControllerTypes("Products").Should().NotContain(typeof(ReviewIndexController));
            Convention.GetControllerTypes("Products").Should().NotContain(typeof(ReviewDetailsController));
        }

        private class TestRouteConvention : MvcRouteConvention
        {
            private readonly List<ResourceControllerInfo> controllerInfoList = new List<ResourceControllerInfo>();

            protected override IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options,
                IEnumerable<Type> controllerTypes)
            {
                controllerInfoList.Add(new ResourceControllerInfo { ResourceFullName = resource.FullName, ControllerTypes = controllerTypes.ToList() });
                yield break;
            }

            public IEnumerable<Type> GetControllerTypes(string resource)
            {
                var info = controllerInfoList.SingleOrDefault(x => x.ResourceFullName == resource);
                return info != null ? info.ControllerTypes : Enumerable.Empty<Type>();
            }
        }

        private class ResourceControllerInfo
        {
            public string ResourceFullName { get; set; }

            public List<Type> ControllerTypes { get; set; } 
        }
    }
}