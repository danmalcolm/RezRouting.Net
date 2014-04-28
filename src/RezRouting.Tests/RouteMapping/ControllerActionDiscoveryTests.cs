using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests mapping of routes based on action methods available on the controllers
    /// handling a resource's actions
    /// </summary>
    public class ControllerActionDiscoveryTests
    {
        private readonly RouteMapper mapper = new RouteMapper();
            
        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy1Controller()
        {
            mapper.Collection(test => test.HandledBy<Test1Controller>());
            mapper.ShouldMapRoutesWithControllerActions("test1#index", "test1#show");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy2Controllers()
        {
            mapper.Collection(test => test.HandledBy<Test1Controller,Test2Controller>());
            mapper.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy3Controllers()
        {
            mapper.Collection(test => test.HandledBy<Test1Controller, Test2Controller, Test3Controller>());
            mapper.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create", "test3#edit", "test3#update");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy4Controllers()
        {
            mapper.Collection(test => test.HandledBy<Test1Controller, Test2Controller, Test3Controller, Test4Controller>());
            mapper.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create", "test3#edit", "test3#update", "test4#destroy");
        }

        [Fact]
        public void ShouldMapRoutesForActionsWithNameOverrides()
        {
            mapper.Collection(test => test.HandledBy<CustomActionNameController>());
            mapper.ShouldMapRoutesWithControllerActions("customactionname#index", "customactionname#show");
        }

        [Fact]
        public void ShouldMapRouteToFirstControllerWhenSameActionExistsOnMultipleControllers()
        {
            mapper.Collection(test => test.HandledBy<SameActions1Controller,SameActions2Controller>());
            mapper.ShouldMapRoutesWithControllerActions("sameactions1#index");
        }

        [Fact]
        public void ShouldMapToAllControllersIncludedByCustomIncludeFuncWhenSameActionExistsOnMultipleControllers()
        {
            var customRouteType = new RouteType("Custom", new[] {ResourceType.Collection}, CollectionLevel.Item, "Custom",
                "custom", "GET", 10, includeController: (type, index) => index <= 1);
            mapper.Configure(c =>
            {
                c.ClearRouteTypes();
                c.AddRouteType(customRouteType);
            });
            mapper.Collection(test => test.HandledBy<SameActions1Controller, SameActions2Controller, SameActions3Controller>());
            mapper.ShouldMapRoutesWithControllerActions("sameactions1#custom", "sameactions2#custom");
        }

        [Fact]
        public void RouteNameConventionShouldIncludeControllerNameVariationWhenMappingSameRouteToMultipleControllers()
        {
            var customRouteType = new RouteType("Custom", new[] { ResourceType.Collection }, CollectionLevel.Item, "Custom",
                "custom", "GET", 10, includeController: (type, index) => index <= 1);
            mapper.Configure(c =>
            {
                c.ClearRouteTypes();
                c.AddRouteType(customRouteType);
            });
            mapper.Collection(test => test.HandledBy<SameActions1Controller, SameActions2Controller, SameActions3Controller>());
            mapper.ShouldMapRoutesWithNames("SameActions.SameActions1.Custom", "SameActions.SameActions2.Custom");
        }

        public class Test1Controller : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Show(string id)
            {
                return null;
            }
        }

        public class Test2Controller : Controller
        {
            public ActionResult New()
            {
                return null;
            }

            public ActionResult Create(object o)
            {
                return null;
            }
        }

        public class Test3Controller : Controller
        {
            public ActionResult Edit(string id)
            {
                return null;
            }

            public ActionResult Update(string id)
            {
                return null;
            }
        }

        public class Test4Controller : Controller
        {
            public ActionResult Destroy(string id)
            {
                return null;
            }
        }

        public class CustomActionNameController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            [ActionName("Show")]
            public ActionResult Details(string id)
            {
                return null;
            }
        }

        public class SameActions1Controller : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Custom()
            {
                return null;
            }
        }

        public class SameActions2Controller : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Custom()
            {
                return null;
            }

        }

        public class SameActions3Controller : Controller
        {
            public ActionResult Index()
            {
                return null;
            }

            public ActionResult Custom()
            {
                return null;
            }
        }
    }
}



