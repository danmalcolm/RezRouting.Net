using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RouteTests
    {
        private IResourceRouteHandler testHandler = MvcAction.For((TestController x) => x.Action1());

        [Fact]
        public void should_build_route_with_core_properties_configured()
        {
            var route = new Route("Route1", "GET", "test", testHandler);

            route.Should().NotBeNull();
            route.ShouldBeEquivalentTo(new
            {
                Name = "Route1", 
                Handler = new MvcAction(typeof(TestController), "Action1"),
                HttpMethod = "GET",
                Path = "test"
            }, options => options.ExcludingMissingProperties());
        }

        [Fact]
        public void custom_properties_should_be_empty_if_not_configured()
        {
            var route = new Route("Route1", "GET", "test", testHandler);

            route.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_include_copy_of_items_in_custom_properties_if_specified()
        {
            var data = new CustomValueCollection { {"key 1", "value 1" }};
            var route = new Route("Route1", "GET", "test", testHandler, data);

            route.CustomProperties.ShouldBeEquivalentTo(new CustomValueCollection { { "key 1", "value 1" } });
            route.CustomProperties.Should().NotBeSameAs(data);
        }

        [Fact]
        public void additional_route_values_should_be_empty_if_not_configured()
        {
            var route = new Route("Route1", "GET", "test", testHandler);

            route.AdditionalRouteValues.Should().BeEmpty();
        }

        [Fact]
        public void should_include_copy_of_additional_route_values_if_specified()
        {
            var values = new CustomValueCollection { { "key 1", "value 1" } };
            var route = new Route("Route1", "GET", "test", testHandler, additionalRouteValues: values);

            route.AdditionalRouteValues.ShouldBeEquivalentTo(new CustomValueCollection { { "key 1", "value 1" } });
            route.AdditionalRouteValues.Should().NotBeSameAs(values);
        }
        
        private class TestController : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }
        }
    }
}