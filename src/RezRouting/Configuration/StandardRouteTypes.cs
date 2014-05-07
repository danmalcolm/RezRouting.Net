using System.Collections.Generic;

namespace RezRouting.Configuration
{
    internal static class StandardRouteTypes
    {
        internal static IEnumerable<RouteType> Build()
        {
            return new List<RouteType>
            {
                new RouteType("Index", new[] {ResourceType.Collection}, CollectionLevel.Collection, "Index",
                    StandardHttpMethod.Get, 0,
                    customize: settings => settings.PathSegment = ""),
                new RouteType("Show", new[] {ResourceType.Singular, ResourceType.Collection}, CollectionLevel.Item,
                    "Show", StandardHttpMethod.Get, 3),
                new RouteType("New", new[] {ResourceType.Singular, ResourceType.Collection},
                    CollectionLevel.Collection, "New", StandardHttpMethod.Get, 1,
                    customize: settings => settings.PathSegment = "new"),
                new RouteType("Create", new[] {ResourceType.Singular, ResourceType.Collection},
                    CollectionLevel.Collection, "Create", StandardHttpMethod.Post, 4),
                new RouteType("Edit", new[] {ResourceType.Singular, ResourceType.Collection}, CollectionLevel.Item,
                    "Edit", StandardHttpMethod.Get, 2,
                    customize: settings => settings.PathSegment = "edit"),
                new RouteType("Update", new[] {ResourceType.Singular, ResourceType.Collection}, CollectionLevel.Item,
                    "Update", StandardHttpMethod.Put, 5),
                new RouteType("Delete", new[] {ResourceType.Singular, ResourceType.Collection}, CollectionLevel.Item,
                    "Destroy", StandardHttpMethod.Delete, 6)
            };
        }
    }
}