using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.UrlGeneration
{
    public class RouteModelIndexTests
    {
        [Fact]
        public void should_retrieve_individual_routes_by_controller_type_and_action_name()
        {
            var builder = ConfigureResources();
            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            var index = new RouteModelIndex();
            index.AddRoutes(routes);

            var routeModels = index.GetRoutes(routes, typeof (ProductsController), "Index");
            routeModels.Select(x => x.FullName).Should().Equal("Products.Index");

            routeModels = index.GetRoutes(routes, typeof(ManufacturersController), "Index");
            routeModels.Select(x => x.FullName).Should().Equal("Manufacturers.Index");
        }

        [Fact]
        public void should_retrieve_multiple_routes_sharing_same_controller_type_and_action()
        {
            var builder = ConfigureResources();
            var routes = new RouteCollection();
            builder.MapMvcRoutes(routes);

            var index = new RouteModelIndex();
            index.AddRoutes(routes);

            var routeModels = index.GetRoutes(routes, typeof(AuditLogController), "Index");
            routeModels.Select(x => x.FullName).Should()
                .Equal("Products.AuditLog.Index", "Manufacturers.AuditLog.Index");
        }

        [Fact]
        public void should_not_break_when_indexing_non_resource_routes()
        {
            var routes = new RouteCollection();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var index = new RouteModelIndex();
            index.AddRoutes(routes);
        }

        private IRootResourceBuilder ConfigureResources()
        {
            var builder = RootResourceBuilder.Create();
            builder.Collection("Products", products =>
            {
                products.Route("Index", "GET", "", MvcAction.For((ProductsController x) => x.Index()));
                products.Collection("AuditLog", auditlog =>
                {
                    auditlog.Route("Index", "GET", "", MvcAction.For((AuditLogController x) => x.Index("")));
                });
            });
            builder.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.Route("Index", "GET", "", MvcAction.For((ManufacturersController x) => x.Index()));
                manufacturers.Collection("AuditLog", auditlog =>
                {
                    auditlog.Route("Index", "GET", "", MvcAction.For((AuditLogController x) => x.Index("")));
                });
            });
            return builder;
        }

        public class ProductsController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }
        }

        public class ManufacturersController : Controller
        {
            public ActionResult Index()
            {
                return null;
            }
        }

        public class AuditLogController : Controller
        {
            public ActionResult Index(string objectType)
            {
                return null;
            }
        }

        
         
    }
}