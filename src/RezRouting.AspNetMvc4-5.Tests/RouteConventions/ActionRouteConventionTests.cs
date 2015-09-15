using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions
{
    public class ActionRouteConventionTests
    {
        private readonly ConfigurationContext context = new ConfigurationContext(new CustomValueCollection());
        private readonly ConfigurationOptions options = new ConfigurationOptions(new UrlPathSettings(), new DefaultIdNameFormatter());

        public Resource CreateCollectionResource()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Collection("Products", products => {});
            var root = builder.Build();
            var collection = root.Children.Single(x => x.Name == "Products");
            return collection;
        }

        [Fact]
        public void should_format_path_using_options()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new [] { typeof(TestController)});
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "FunkyAction");
            var urlPathSettings = new UrlPathSettings(CaseStyle.Upper, "_");
            var options2 = new ConfigurationOptions(urlPathSettings, new DefaultIdNameFormatter());
            
            convention.Extend(resourceData, context, options2);

            var route = resourceData.Routes.Single();
            route.Path.Should().Be("FUNKY_ACTION");
        }

        [Fact]
        public void should_not_build_route_if_action_not_supported_by_any_controllers()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new [] { typeof(TestController)});
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "UnknownAction", "GET", "FunkyAction");
            
            convention.Extend(resourceData, context, options);

            resourceData.Routes.Should().BeEmpty();
        }
        
        [Fact]
        public void should_build_route_when_required_action_name_set_via_ActionNameAttribute()
        {
            var resourceData = new CollectionData();
            resourceData.Init("Products", null);
            resourceData.ExtensionData.AddControllerTypes(new[] { typeof(TestControllerWithActionNameAttribute) });
            var convention = new ActionRouteConvention("FunkyAction", ResourceType.Collection, "FunkyAction", "GET", "Action1");
            
            convention.Extend(resourceData, context, options);

            resourceData.Routes.Should().HaveCount(1);
            var route = resourceData.Routes.Single();
            route.Handler.Should().Be(new MvcAction(typeof(TestControllerWithActionNameAttribute), "FunkyAction"));
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