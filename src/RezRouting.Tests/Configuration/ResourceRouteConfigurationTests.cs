using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceRouteConfigurationTests
    {
        [Fact]
        public void should_add_specified_routes_to_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", new MvcAction(typeof(TestController), "Edit"), "GET", "edit");
                    products.Route("Update", new MvcAction(typeof(TestController), "Update"), "PUT", "");
                });
            });

            resource.Routes.Select(x => x.Name).Should().Equal("Edit", "Update");

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.Handler.Should().Be(new MvcAction(typeof (TestController), "Edit"));
            editRoute.HttpMethod.Should().Be("GET");
            editRoute.Path.Should().Be("edit");

            var updateRoute = resource.Routes.Single(x => x.Name == "Update");
            updateRoute.Handler.Should().Be(new MvcAction(typeof(TestController), "Update"));
            updateRoute.HttpMethod.Should().Be("PUT");
            updateRoute.Path.Should().Be("");
        }

        [Fact]
        public void custom_properties_on_routes_configured_for_resource_should_be_empty_if_not_specified()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", new MvcAction(typeof(TestController), "Edit"), "GET", "edit");
                });
            });

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_include_custom_properties_on_routes_configured_for_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Route("Edit", new MvcAction(typeof(TestController), "Edit"), "GET", "edit", new Dictionary<string, object> { { "key1", "value1" } });
                });
            });

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            var expectedData = new Dictionary<string, object>{{ "key1", "value1" }};
            editRoute.CustomProperties.Should().Equal(expectedData);
        }

        /// <summary>
        /// Configures resources using supplied action and returns the first child resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResource(Action<ResourceGraphBuilder> configure)
        {
            var builder = new ResourceGraphBuilder();
            configure(builder);
            var root = builder.Build(new ResourceOptions());
            return root.Children.Single();
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