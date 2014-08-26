using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace RezRouting2.Tests
{
    public class RouteBuilderTests
    {
        public class TestController
        {
            
        }

        private static RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<RouteType>());
        private Resource resource = new CollectionBuilder("Products").Build(context);

        [Fact]
        public void should_not_build_route_if_skipped()
        {
            var builder = new RouteBuilder(typeof(TestController));
            builder.Skip();

            builder.Build().Should().BeNull();
        }

        [Fact]
        public void should_build_route_with_basic_properties()
        {
            var builder = new RouteBuilder(typeof(TestController));
            builder.Name("Route1");
            builder.Action("Action1");
            builder.HttpMethod("GET");
            builder.Path("test");

            var route = builder.Build();

            route.Should().NotBeNull();
            route.ShouldBeEquivalentTo(new
            {
                Name = "Route1", 
                ControllerType = typeof(TestController),
                Action = "Action1",
                HttpMethod = "GET",
                Path = "test"
            }, options => options.ThrowingOnMissingProperties());
        }

        [Theory,
        InlineData("Route1", "Action1", "GET", "test"),
        InlineData(null, "Action1", "GET", "test"),
        InlineData("Route1", null, "GET", "test"),
        InlineData("Route1", "Action1", null, "test"),
        InlineData("Route1", "Action1", "GET", null)
        ]
        public void should_throw_if_key_properties_not_configured(string name, string action, string httpMethod, string path)
        {
            var builder = new RouteBuilder(typeof(TestController));
            if (name != null) builder.Name(name);
            if (action != null) builder.Action(action);
            if (httpMethod != null) builder.HttpMethod(httpMethod);
            if (path != null) builder.Path(path);

            Action a = () => builder.Build();
            a.ShouldThrow<InvalidOperationException>();
        } 
    }
}