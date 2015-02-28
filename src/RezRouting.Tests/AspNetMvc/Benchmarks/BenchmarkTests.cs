using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.Benchmarks.Controllers;
using RezRouting.Tests.Infrastructure;
using RezRouting.Tests.Infrastructure.Performance;
using RezRouting.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.Benchmarks
{
    /// <summary>
    /// Contains tests used to give indication of execution times - currently 
    /// used only for manual execution and verification in development environment.
    /// Switch from internal to public visibility to include these tests in test run.
    /// </summary>
    public class BenchMarkTests : IDisposable
    {
        [Fact]
        public void test_model_should_contain_routes()
        {
            var root = BuildResources();

            root.Children.Count.Should().Be(100);
            root.Children.SelectMany(x => x.Routes).Count().Should().Be(1000);
        }

        [Fact]
        public void configuring_resources()
        {
            Profiler.Profile("Building resource model", 100, () => BuildResources());
        }

        [Fact]
        public void building_resources()
        {
            var builder = ConfigureResources();
            Profiler.Profile("Building resource model", 100, () => builder.Build());
        }

        [Fact]
        public void building_resources_with_route_conventions()
        {
            var builder = ConfigureResourcesUsingConventions();
            Profiler.Profile("Creating routes", 100, () =>
            {
                builder.Build();
            });
        }

        [Fact]
        public void route_identification()
        {
            var routes = MapRoutes();

            var httpContext = TestHttpContextBuilder.Create("GET", "/abyssinians/action1");
            routes.GetRouteData(httpContext).Should().NotBeNull();
            Profiler.Profile("Identifying route at start of RouteCollection", 100,
                () => routes.GetRouteData(httpContext));

            httpContext = TestHttpContextBuilder.Create("GET", "/lobsters/action3");
            routes.GetRouteData(httpContext).Should().NotBeNull();
            Profiler.Profile("Identifying route at middle of RouteCollection", 100,
                () => routes.GetRouteData(httpContext));

            httpContext = TestHttpContextBuilder.Create("GET", "/zoos/action5");
            routes.GetRouteData(httpContext).Should().NotBeNull();
            Profiler.Profile("Identifying route at end of RouteCollection", 100,
                () => routes.GetRouteData(httpContext));
        }

        [Fact]
        public void url_generation_using_resource_url_extensions_optimised_to_use_index()
        {
            var helper = CreateUrlHelperWithRoutes();
            UrlHelperExtensions.IndexRoutes(helper.RouteCollection);
            
            helper.ResourceUrl<AbyssiniansController>("Action1", null)
                .Should().Be("/abyssinians/action1");
            Profiler.Profile("Generating URL for route at start of RouteCollection", 100,
                () => helper.ResourceUrl<AbyssiniansController>("Action1", null));

            helper.ResourceUrl<LobstersController>("Action3", null)
                .Should().Be("/lobsters/action3");
            Profiler.Profile("Generating URL for route near middle of RouteCollection", 100,
                () => helper.ResourceUrl<LobstersController>("Action3", null));

            helper.ResourceUrl<ZoosController>("Action5", null)
                .Should().Be("/zoos/action5");
            Profiler.Profile("Generating URL for route at end of RouteCollection", 100,
                () => helper.ResourceUrl<ZoosController>("Action5", null));
        }

        [Fact]
        public void url_generation_using_resource_url_extensions_without_index_optimisations()
        {
            var helper = CreateUrlHelperWithRoutes();
            UrlHelperExtensions.ClearIndexedRoutes();

            helper.ResourceUrl<AbyssiniansController>("Action1", null)
                .Should().Be("/abyssinians/action1");
            Profiler.Profile("Generating URL for route at start of RouteCollection", 100,
                () => helper.ResourceUrl<AbyssiniansController>("Action1", null));

            helper.ResourceUrl<LobstersController>("Action3", null)
                .Should().Be("/lobsters/action3");
            Profiler.Profile("Generating URL for route near middle of RouteCollection", 100,
                () => helper.ResourceUrl<LobstersController>("Action3", null));

            helper.ResourceUrl<ZoosController>("Action5", null)
                .Should().Be("/zoos/action5");
            Profiler.Profile("Generating URL for route at end of RouteCollection", 100,
                () => helper.ResourceUrl<ZoosController>("Action5", null));
        }

        [Fact]
        public void url_generation_using_standard_mvc_url_extensions()
        {
            var helper = CreateUrlHelperWithRoutes();

            helper.RouteUrl(new { controller = "Abyssinians", action = "Action1" })
                .Should().Be("/abyssinians/action1");
            Profiler.Profile("Generating URL for route at start of RouteCollection", 100,
                () => helper.RouteUrl(new { controller = "Abyssinians", action = "Action1" }));

            helper.RouteUrl(new { controller = "Lobsters", action = "Action3" })
                .Should().Be("/lobsters/action3");
            Profiler.Profile("Generating URL for route near middle of RouteCollection", 100,
                () => helper.RouteUrl(new { controller = "Lobsters", action = "Action3" }));

            helper.RouteUrl(new { controller = "Zoos", action = "Action5" })
                .Should().Be("/zoos/action5");
            Profiler.Profile("Generating URL for route at end of RouteCollection", 100,
                () => helper.RouteUrl(new { controller = "Zoos", action = "Action5" }));
        }

        [Fact]
        public void url_generation_using_route_name()
        {
            var helper = CreateUrlHelperWithRoutes();

            helper.RouteUrl("Abyssinians.Action1")
                .Should().Be("/abyssinians/action1");
            Profiler.Profile("Generating URL for route at start of RouteCollection", 100,
                () => helper.RouteUrl("Abyssinians.Action1"));

            helper.RouteUrl("Lobsters.Action3")
                .Should().Be("/lobsters/action3");
            Profiler.Profile("Generating URL for route near middle of RouteCollection", 100,
                () => helper.RouteUrl("Lobsters.Action3"));

            helper.RouteUrl("Zoos.Action5")
                .Should().Be("/zoos/action5");
            Profiler.Profile("Generating URL for route at end of RouteCollection", 100,
                () => helper.RouteUrl("Zoos.Action5"));
        }

        [Fact]
        public void creating_routes_within_route_collection()
        {
            var model = BuildResources();

            Profiler.Profile("Creating routes", 100, () =>
            {
                var routes = new RouteCollection();
                var creator = new MvcRouteCreator();
                creator.CreateRoutes(model, routes, null);
            });
        }
        
        private static IRootResourceBuilder ConfigureResources()
        {
            var builder = RootResourceBuilder.Create("");
            var actionNames = Enumerable.Range(1, 10)
                .Select(n => "Action" + n)
                .ToList();

            DemoData.Resources.Each(resourceInfo =>
            {
                string resourceName = resourceInfo.Item1;
                var controllerType = resourceInfo.Item2;
                builder.Collection(resourceName, collection =>
                {
                    // Add 10 routes (Action1 .. Action10)
                    foreach (var actionName in actionNames)
                    {
                        string path = actionName.ToLowerInvariant();
                        collection.Route(actionName, "GET", path, new MvcAction(controllerType, actionName));
                    }
                });
            });
            return builder;
        }

        private static IRootResourceBuilder ConfigureResourcesUsingConventions()
        {
            var actionNames = Enumerable.Range(1, 10)
                .Select(n => "Action" + n)
                .ToList();
            var conventions = actionNames.Select(name => 
                new ActionRouteConvention(name, ResourceType.Collection, name, "GET", name.ToLowerInvariant()));
            var scheme = new TestRouteConventionScheme(conventions);
            
            var builder = RootResourceBuilder.Create("");
            builder.ApplyRouteConventions(scheme);
            DemoData.Resources.Each(resourceInfo =>
            {
                string resourceName = resourceInfo.Item1;
                var controllerType = resourceInfo.Item2;
                builder.Collection(resourceName, collection =>
                {
                    collection.HandledBy(controllerType);
                });
            });
            return builder;
        }

        private static Resource BuildResources()
        {
            var root = ConfigureResources();
            return root.Build();
        }

        private static RouteCollection MapRoutes()
        {
            var builder = ConfigureResources();
            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);
            return routes;
        }

        private static UrlHelper CreateUrlHelperWithRoutes()
        {
            var routes = MapRoutes();
            var context = TestRequestContextBuilder.Create();
            var helper = new UrlHelper(context, routes);
            return helper;
        }
        
        public void Dispose()
        {
            UrlHelperExtensions.ClearIndexedRoutes();
        }
    }
}