using System.Web.Mvc;
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
        private readonly RouteMapper builder = new RouteMapper();
            
        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy1Controller()
        {
            builder.Collection(test => test.HandledBy<Test1Controller>());
            builder.ShouldMapRoutesWithControllerActions("test1#index", "test1#show");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy2Controllers()
        {
            builder.Collection(test => test.HandledBy<Test1Controller,Test2Controller>());
            builder.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy3Controllers()
        {
            builder.Collection(test => test.HandledBy<Test1Controller, Test2Controller, Test3Controller>());
            builder.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create", "test3#edit", "test3#update");
        }

        [Fact]
        public void ShouldMapRoutesForActionsSupportedBy4Controllers()
        {
            builder.Collection(test => test.HandledBy<Test1Controller, Test2Controller, Test3Controller, Test4Controller>());
            builder.ShouldMapRoutesWithControllerActions("test1#index", "test1#show", "test2#new", "test2#create", "test3#edit", "test3#update", "test4#destroy");
        }

        [Fact]
        public void ShouldMapRoutesForActionsWithNameOverrides()
        {
            builder.Collection(test => test.HandledBy<CustomActionNameController>());
            builder.ShouldMapRoutesWithControllerActions("customactionname#index", "customactionname#show");
        }

        [Fact]
        public void ShouldMapRouteToFirstControllerWhenSameActionExistsOnMultipleContollers()
        {
            builder.Collection(test => test.HandledBy<SameActions1Controller,SameActions2Controller>());
            builder.ShouldMapRoutesWithControllerActions("sameactions1#index");
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
        }

        public class SameActions2Controller : Controller
        {
            public ActionResult Index()
            {
                return null;
            }
        }
    }
}



