using System;
using System.Collections.Generic;
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
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => {});
            var root = builder.Build();
            var collection = root.Children.Single(x => x.Name == "Products");
            return collection;
        }

        private CustomValueCollection CreateConventionData(Type controllerType)
        {
            var data = new CustomValueCollection();
            data.AddControllerTypes(new[] {controllerType});
            return data;
        }

        [Fact]
        public void should_format_path_using_options()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "FunkyAction");
            var urlPathSettings = new UrlPathSettings(CaseStyle.Upper, "_");
            var data = CreateConventionData(typeof(TestController));
            
            var route = convention
                .Create(collection, data, urlPathSettings, new CustomValueCollection())
                .Single();

            route.Path.Should().Be("FUNKY_ACTION");
        }

        [Fact]
        public void should_not_build_route_if_action_not_supported()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "UnknownAction", "GET", "FunkyAction");
            var data = CreateConventionData(typeof(TestController));
            
            
            var routes = convention
                .Create(collection, data, new UrlPathSettings(), new CustomValueCollection());

            routes.Should().BeEmpty();
        }
        
        [Fact]
        public void should_build_route_when_required_action_name_set_via_ActionNameAttribute()
        {
            var collection = CreateCollectionResource();
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "Action1");
            var data = CreateConventionData(typeof(TestControllerWithActionNameAttribute));
            
            var route = convention
                .Create(collection, data, new UrlPathSettings(), new CustomValueCollection())
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