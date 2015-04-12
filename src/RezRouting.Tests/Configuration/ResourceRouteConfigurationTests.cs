using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceRouteConfigurationTests : ConfigurationTestsBase
    {
        [Fact]
        public void should_add_specified_routes_to_resource()
        {
            var handler1 = new TestRouteHandler();
            var handler2 = new TestRouteHandler();
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", "GET", "edit", handler1);
                    products.Route("Update", "PUT", "", handler2);
                });
            });

            var resource = resources["Products"];
            resource.Routes.Select(x => x.Name).Should().Equal("Edit", "Update");

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.Handler.Should().Be(handler1);
            editRoute.HttpMethod.Should().Be("GET");
            editRoute.Path.Should().Be("edit");

            var updateRoute = resource.Routes.Single(x => x.Name == "Update");
            updateRoute.Handler.Should().Be(handler2);
            updateRoute.HttpMethod.Should().Be("PUT");
            updateRoute.Path.Should().Be("");
        }

        [Fact]
        public void full_name_of_route_should_include_full_name_of_resource()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Route("Edit", "GET", "edit", new TestRouteHandler());
                    });
                });
            });

            var resource = resources["Products.Product"];
            var route = resource.Routes.Single();
            route.FullName.Should().Be("Products.Product.Edit");
        }

        [Fact]
        public void full_name_of_route_should_not_include_prefix_if_root_resource_with_empty_name()
        {
            var resources = BuildResources(root =>
            {
                root.Route("Home", "GET", "", new TestRouteHandler());
            });

            var resource = resources.Values.Single();
            var route = resource.Routes.Single();
            route.FullName.Should().Be("Home");
        }

        [Fact]
        public void custom_properties_on_routes_configured_for_resource_should_be_empty_if_not_specified()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", "GET", "edit", new TestRouteHandler());
                });
            });

            var resource = resources["Products"];
            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_include_custom_properties_on_routes_configured_for_resource()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", "GET", "edit", new TestRouteHandler(), new CustomValueCollection { { "key1", "value1" } });
                });
            });
            
            var resource = resources["Products"];
            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            var expectedData = new Dictionary<string, object>{{ "key1", "value1" }};
            editRoute.CustomProperties.Should().Equal(expectedData);
        }

        [Fact]
        public void additional_route_values_should_be_empty_if_not_specified()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", "GET", "edit", new TestRouteHandler());
                });
            });

            var resource = resources["Products"];
            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.AdditionalRouteValues.Should().BeEmpty();
        }

        [Fact]
        public void should_include_additional_route_values_on_route()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", "GET", "edit", new TestRouteHandler(), additionalRouteValues: new CustomValueCollection { { "key1", "value1" } });
                });
            });

            var resource = resources["Products"];
            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            var expectedData = new Dictionary<string, object> { { "key1", "value1" } };
            editRoute.AdditionalRouteValues.Should().Equal(expectedData);
        }
        
        public class TestController : Controller
        {
            public ActionResult Edit()
            {
                return null;
            }

            public ActionResult Update()
            {
                return null;
            }
        }
    }
}