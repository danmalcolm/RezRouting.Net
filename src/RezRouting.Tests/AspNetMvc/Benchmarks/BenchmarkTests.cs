using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteTypes;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Tests.AspNetMvc.Benchmarks.Controllers;
using RezRouting.Tests.Infrastructure;
using RezRouting.Tests.Infrastructure.Performance;
using RezRouting.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.Benchmarks
{
    /// <summary>
    /// Contains tests used to give indication of execution times - currently 
    /// used only for manual execution and verification in development environment 
    /// at present. Switch from internal to public visibility to include these
    /// tests.
    /// </summary>
    internal class BenchMarkTests : IDisposable
    {
        [Fact]
        public void test_model_should_contain_routes()
        {
            var model = BuildModel();

            model.Resources.Count.Should().Be(100);
            model.Resources.SelectMany(x => x.Routes).Count().Should().Be(500);
        }

        [Fact]
        public void model_mapping()
        {
            Profiler.Profile("Building resource model", 100, () => BuildModel());
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
            var model = BuildModel();

            Profiler.Profile("Creating routes", 100, () =>
            {
                var routes = new RouteCollection();
                var creator = new MvcRouteCreator();
                creator.CreateRoutes(model, routes, null);
            });
        }

        private static RouteMapper Configure()
        {
            var routeTypes = Enumerable.Range(1, 10)
                .Select(n => "Action" + n)
                .Select(name => new ActionRouteType
                    (name, ResourceLevel.Collection, name, "GET", name))
                .ToList();

            var mapper = new RouteMapper();
            mapper.RouteTypes(routeTypes);

            DemoData.Resources.Each(resourceInfo =>
            {
                string name = resourceInfo.Item1;
                mapper.Collection(name, c => c.HandledBy(resourceInfo.Item2));
            });
            return mapper;
        }

        private static ResourcesModel BuildModel()
        {
            var mapper = Configure();
            var model = mapper.Build();
            return model;
        }

        private static RouteCollection MapRoutes()
        {
            var mapper = Configure();
            var routes = new RouteCollection();
            mapper.MapMvcRoutes(routes);
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