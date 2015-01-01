﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;
using Route = RezRouting.Resources.Route;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcResourceBuilderExtensionsTests
    {
        [Fact]
        public void when_mapping_MVC_routes_should_map_routes_based_on_resources_model()
        {
            var builder = CreateBuilder();

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["RouteModel"] as Route)
                .Select(x => x.FullName)
                .ShouldBeEquivalentTo(new[] { "Products.Route1", "Products.Route2" });
        }

        [Fact]
        public void when_mapping_MVC_routes_should_map_routes_for_area_specified()
        {
            var builder = CreateBuilder();

            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes, area: "Area1");

            routes.Cast<System.Web.Routing.Route>()
                .Select(x => x.DataTokens["area"] as string)
                .ShouldBeEquivalentTo(new[] { "Area1", "Area1" });
        }

        [Fact]
        public void when_mapping_MVC_routes_should_execute_action_with_model_used_to_create_routes()
        {
            var builder = CreateBuilder();

            ResourceGraphModel model = null;
            builder.MapMvcRoutes(new RouteCollection(), modelAction: x => model = x);

            model.Should().NotBeNull();
            model.Resources.Should().HaveCount(1);
            model.Resources.Should().ContainSingle(x => x.Name == "Products");
        }
        
        private ResourceGraphBuilder CreateBuilder()
        {
            var builder = new ResourceGraphBuilder();
            builder.Collection("Products", products =>
            {
                products.Route("Route1", new MvcAction(typeof(TestController), "Action1"), "GET", "action1");
                products.Route("Route2", new MvcAction(typeof(TestController), "Action2"), "GET", "action2");
            });
            return builder;
        }

        public class TestController : Controller
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
    }
}