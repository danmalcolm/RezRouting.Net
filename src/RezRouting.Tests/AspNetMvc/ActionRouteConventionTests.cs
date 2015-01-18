using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class ActionRouteConventionTests
    {
        public Resource CreateCollectionResource()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products => {});
            var root = builder.Build(new ResourceOptions());
            var collection = root.Children.Single(x => x.Name == "Products");
            return collection;
        }

        [Fact]
        public void should_format_path_using_options()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "FunkyAction");
            var urlPathSettings = new UrlPathSettings(CaseStyle.Upper, "_");
            
            var route = convention
                .Create(collection, new [] { new MvcController(typeof(TestController)) }, urlPathSettings)
                .Single();

            route.Path.Should().Be("FUNKY_ACTION");
        }

        [Fact]
        public void should_not_build_route_if_action_not_supported()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "UnknownAction", "GET", "FunkyAction");
            
            var routes = convention
                .Create(collection, new[] { new MvcController(typeof(TestController)) }, new UrlPathSettings());

            routes.Should().BeEmpty();
        }
        
        [Fact]
        public void should_build_route_when_required_action_name_set_via_ActionNameAttribute()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "Action1");

            var route = convention
                .Create(collection, new[] { new MvcController(typeof(TestControllerWithActionNameAttribute)) }, new UrlPathSettings())
                .Single();

            route.Should().NotBeNull();
            route.Handler.Should().Be(new MvcAction(typeof (TestControllerWithActionNameAttribute), "FunkyAction"));
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