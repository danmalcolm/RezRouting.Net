using System;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    public class RouteTypeCustomisationTests
    {
        public RouteCollection MapCustomizedRoutes(Action<CustomRouteSettingsBuilder> customize)
        {
            var routeMapper = new RouteMapper();
            routeMapper.Configure(config =>
            {
                // TODO - applications should specify
                // routes - no standard routes
                config.ClearRouteTypes();
                var routeType = new RouteType("Custom", new[] {ResourceType.Collection, ResourceType.Singular}, "CustomAction", "GET", -1, customize: customize);
                config.AddRouteType(routeType);
            });
            routeMapper.Collection(collection =>
            {
                collection.CustomName("Product");
                collection.HandledBy<RenameController, ArchiveController>();
            });
            return routeMapper.MapRoutes();
        }

        [Fact]
        public void should_include_all_matching_controllers_by_default()
        {
            var routes = MapCustomizedRoutes(settings =>
            {
                settings.IncludeControllerInRouteName = true;
            });
            routes.ShouldContainRoutesWithNames("Products.Rename.Custom", "Products.Archive.Custom");
        }

        [Fact]
        public void should_only_map_specified_routes_if_filtered()
        {
            var routes = MapCustomizedRoutes(settings =>
            {
                settings.Include = settings.ControllerType.Name.StartsWith("Rename");
                settings.IncludeControllerInRouteName = true;
            });
            routes.ShouldContainRoutesWithNames("Products.Rename.Custom");
            routes.ShouldNotContainRoutesWithNames("Products.Archive.Custom");
        }

        [Fact]
        public void should_use_path_segment_in_url_if_specified()
        {
            var routes = MapCustomizedRoutes(settings =>
            {
                settings.CollectionLevel = CollectionLevel.Item;
                settings.IncludeControllerInRouteName = true;
                settings.PathSegment = RouteValueHelper.TrimControllerFromTypeName(settings.ControllerType).ToLowerInvariant();
            });
            routes.ShouldContainRoutesWithUrls("products/{id}/rename", "products/{id}/archive");
        }

        [Fact]
        public void should_map_to_collection_level_specified()
        {
            var routes = MapCustomizedRoutes(settings =>
            {
                settings.CollectionLevel = CollectionLevel.Item;
                settings.IncludeControllerInRouteName = true;
                settings.PathSegment = settings.PathSegment = RouteValueHelper.TrimControllerFromTypeName(settings.ControllerType).ToLowerInvariant();
            });
            routes.ShouldContainRoutesWithUrls("products/{id}/rename", "products/{id}/archive");

            routes = MapCustomizedRoutes(settings =>
            {
                settings.CollectionLevel = CollectionLevel.Collection;
                settings.IncludeControllerInRouteName = true;
                settings.PathSegment = settings.PathSegment = RouteValueHelper.TrimControllerFromTypeName(settings.ControllerType).ToLowerInvariant();
            });
            routes.ShouldContainRoutesWithUrls("products/rename", "products/archive");
        }


        public class RenameController : Controller
        {
            public ActionResult CustomAction()
            {
                return null;
            }
        }

        public class ArchiveController : Controller
        {
            public ActionResult CustomAction()
            {
                return null;
            }
        }

    }
}