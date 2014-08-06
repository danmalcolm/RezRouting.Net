using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.RouteMapping.TestNamespace;
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
        public void ShouldMapRoutesForActionsHandledByAllInScope()
        {
            mapper.Collection(test => test.HandledByAllInScopeOf<NamespaceTest1Controller>());
            mapper.ShouldMapRoutesWithControllerActions("namespacetest1#index", "namespacetest1#show", "namespacetest2#new", "namespacetest2#create", "namespacetest3#edit", "namespacetest3#update", "namespacetest4#destroy");
        }
        
        [Fact]
        public void ShouldMapRoutesForActionsWithNameOverrides()
        {
            mapper.Collection(test => test.HandledBy<CustomActionNameController>());
            mapper.ShouldMapRoutesWithControllerActions("customactionname#index", "customactionname#show");
        }

        [Fact]
        public void ShouldMapRouteToAllControllersWhenSameActionExistsOnMultipleControllers()
        {
            mapper.Collection(test => test.HandledBy<SameActions1Controller,SameActions2Controller>());
            mapper.ShouldMapRoutesWithControllerActions("sameactions1#index", "sameactions2#index");
        }

        [Fact]
        public void ShouldExcludeControllersIgnoredByCustomizeFunctionWhenSameActionExistsOnMultipleControllers()
        {
            var customRouteType = new RouteType("Custom", new[] {ResourceType.Collection}, "Custom", "GET", 10, customize: route =>
                {
                    route.Include = route.ControllerType != typeof (SameActions3Controller);
                    route.PathSegment = "custom";
                });
            mapper.Configure(c =>
            {
                c.ClearRouteTypes();
                c.AddRouteType(customRouteType);
            });
            mapper.Collection(test => test.HandledBy<SameActions1Controller, SameActions2Controller, SameActions3Controller>());
            mapper.ShouldMapRoutesWithControllerActions("sameactions1#custom", "sameactions2#custom");
        }

        [Fact]
        public void RouteNameConventionShouldIncludeControllerNameVariationWhenMappingRouteToMultipleControllers()
        {
            var routeType = new RouteType("Custom", new[] { ResourceType.Collection }, "Custom", "GET", 10, customize: settings => settings.PathSegment = "custom");
            mapper.Configure(c =>
            {
                c.ClearRouteTypes();
                c.AddRouteType(routeType);
            });
            mapper.Collection(test => test.HandledBy<SameActions1Controller, SameActions2Controller>());
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



namespace RezRouting.Tests.RouteMapping.TestNamespace
{
    public class NamespaceTest1Controller : Controller
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

    public class NamespaceTest2Controller : Controller
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

    public class NamespaceTest3Controller : Controller
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

    public class NamespaceTest4Controller : Controller
    {
        public ActionResult Destroy(string id)
        {
            return null;
        }
    }
}