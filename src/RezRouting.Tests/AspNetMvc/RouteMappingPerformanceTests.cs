using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteTypes;
using RezRouting.Tests.Infrastructure.Performance;
using RezRouting.Utility;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class RouteMappingPerformanceTests
    {
        private static List<ActionRouteType> RouteTypes;
 
        static RouteMappingPerformanceTests()
        {
            RouteTypes = Enumerable.Range(1, 10)
                .Select(n =>
                {
                    string name = "Action" + n;
                    return new ActionRouteType(name, ResourceLevel.Collection, name, "GET", name);
                }).ToList();
            
        }

        [Fact]
        public void test_model_should_contain_routes()
        {
            var model = BuildModel();

            model.Resources.Count.Should().Be(50);
            model.Resources.SelectMany(x => x.Routes).Count().Should().Be(500);
        }

        [Fact]
        public void should_map_route_model_in_acceptable_time()
        {
            Profiler.Profile("Building resource model", 100, () => BuildModel());
        }

        [Fact]
        public void should_create_routes_in_acceptable_time()
        {
            var model = BuildModel();

            Profiler.Profile("Creating routes", 100, () =>
            {
                var routes = new RouteCollection();
                var creator = new MvcRouteCreator();
                creator.CreateRoutes(model, routes, null);
            });
        }

        private static ResourcesModel BuildModel()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(RouteTypes);

            Enumerable.Range(1, 50).Each(n =>
            {
                string name = "Collection" + n + "s";
                string itemName = "Collection" + n;
                mapper.Collection(name, itemName, collection =>
                {
                    collection.HandledBy<TestController1>();
                    collection.HandledBy<TestController2>();
                    collection.HandledBy<TestController3>();
                    collection.HandledBy<TestController4>();
                    collection.HandledBy<TestController5>();
                });
            });
            var model = mapper.Build();
            return model;
        }

        public class TestController1 : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }

            public ActionResult Action2()
            {
                return null;
            }
        }

        public class TestController2 : Controller
        {
            public ActionResult Action3()
            {
                return null;
            }

            public ActionResult Action4()
            {
                return null;
            }
        }

        public class TestController3 : Controller
        {
            public ActionResult Action5()
            {
                return null;
            }

            public ActionResult Action6()
            {
                return null;
            }
        }

        public class TestController4 : Controller
        {
            public ActionResult Action7()
            {
                return null;
            }

            public ActionResult Action8()
            {
                return null;
            }
        }

        public class TestController5 : Controller
        {
            public ActionResult Action9()
            {
                return null;
            }

            public ActionResult Action10()
            {
                return null;
            }
        }
    }
}