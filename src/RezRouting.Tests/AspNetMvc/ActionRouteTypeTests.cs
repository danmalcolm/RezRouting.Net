using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteTypes;
using RezRouting.Options;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class ActionRouteTypeTests
    {
        private Resource collection;

        public ActionRouteTypeTests()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products => {});
            var resources = mapper.Build().ToList();
            collection = resources.Single(x => x.Name == "Products");
        }

        [Fact]
        public void should_format_path_using_options()
        {
            var routeType = new ActionRouteType("CollectionEdit", ResourceLevel.Collection, "BulkEdit", "GET", "BulkEdit");
            var formatter = new UrlPathFormatter(new UrlPathSettings(CaseStyle.Upper, "_"));

            var route = routeType.BuildRoute(collection, typeof (BulkEditController), formatter);

            route.Should().NotBeNull();
            route.Path.Should().Be("BULK_EDIT");
        }
    }

    public class BulkEditController : Controller
    {
        public ActionResult BulkEdit()
        {
            return null;
        }
    }
}