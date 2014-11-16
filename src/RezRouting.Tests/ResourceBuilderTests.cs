using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests
{
    public class ResourceBuilderTests
    {
        private RouteMappingContext CreateContext()
        {
            return new RouteMappingContext(Enumerable.Empty<RouteType>(), new OptionsBuilder().Build());
        }

        [Fact]
        public void custom_properties_should_be_empty_if_none_configured()
        {
            var context = CreateContext();
            var builder = new SingularBuilder("Profile");
            var resource = builder.Build(context);

            resource.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_combine_all_custom_properties_within_resource()
        {
            var context = CreateContext();
            var builder = new SingularBuilder("Profile");

            builder.CustomProperties(new Dictionary<string, object> { { "key1", "value1" } });
            builder.CustomProperties(new Dictionary<string, object> { { "key2", "value2" } });
            var resource = builder.Build(context);

            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };
            resource.CustomProperties.Should().Equal(expectedData);
        }

        [Fact]
        public void should_build_routes_configured_for_resource()
        {
            var context = CreateContext();
            var builder = new SingularBuilder("Profile");

            builder.Route("Edit", typeof(TestController), "Edit", "GET", "edit");
            builder.Route("Update", typeof(TestController), "Update", "PUT", "");

            var resource = builder.Build(context);
            resource.Routes.Select(x => x.Name).Should().Equal("Edit", "Update");

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.ControllerType.Should().Be(typeof (TestController));
            editRoute.Action.Should().Be("Edit");
            editRoute.HttpMethod.Should().Be("GET");
            editRoute.Path.Should().Be("edit");

            var updateRoute = resource.Routes.Single(x => x.Name == "Update");
            updateRoute.ControllerType.Should().Be(typeof(TestController));
            updateRoute.Action.Should().Be("Update");
            updateRoute.HttpMethod.Should().Be("PUT");
            updateRoute.Path.Should().Be("");
        }

        [Fact]
        public void custom_properties_on_routes_configured_for_resource_should_be_empty_if_not_specified()
        {
            var context = CreateContext();
            var builder = new SingularBuilder("Profile");

            builder.Route("Edit", typeof(TestController), "Edit", "GET", "edit");
            var resource = builder.Build(context);

            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            editRoute.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_configure_custom_properties_on_routes_configured_for_resource()
        {
            var context = CreateContext();
            var builder = new SingularBuilder("Profile");

            builder.Route("Edit", typeof(TestController), "Edit", "GET", "edit", new Dictionary<string, object> {{"key1", "value1"}});
            var resource = builder.Build(context);
            
            var editRoute = resource.Routes.Single(x => x.Name == "Edit");
            var expectedData = new Dictionary<string, object>{{ "key1", "value1" }};
            editRoute.CustomProperties.Should().Equal(expectedData);
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