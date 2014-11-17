using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class ActionRouteConventionTests
    {
        public Resource CreateCollectionResource()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products => {});
            var model = mapper.Build();
            var resources = model.Resources;
            var collection = resources.Single(x => x.Name == "Products");
            return collection;
        }

        [Fact]
        public void should_format_path_using_options()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceLevel.Collection, "FunkyAction", "GET", "FunkyAction");
            var formatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.Upper, "_"));
            
            var route = convention
                .Create(collection, new [] { typeof (TestController) }, formatter)
                .Single();

            route.Path.Should().Be("FUNKY_ACTION");
        }

        [Fact]
        public void should_not_build_route_if_action_not_supported()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceLevel.Collection, "UnknownAction", "GET", "FunkyAction");
            
            var routes = convention
                .Create(collection, new[] { typeof(TestController) }, new UrlPathFormatter());

            routes.Should().BeEmpty();
        }
        
        [Fact]
        public void should_build_route_when_required_action_name_set_via_ActionNameAttribute()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceLevel.Collection, "FunkyAction", "GET", "Action1");

            var route = convention
                .Create(collection, new[] { typeof(TestControllerWithActionNameAttribute) }, new UrlPathFormatter())
                .Single();

            route.Should().NotBeNull();
            route.Action.Should().Be("FunkyAction");
        }
    }

    public class TestController : Controller
    {
        public ActionResult FunkyAction()
        {
            return null;
        }
    }

    public class TestControllerWithActionNameAttribute : Controller
    {
        [ActionName("FunkyAction")]
        public ActionResult DifferentName()
        {
            return null;
        }
    }
}