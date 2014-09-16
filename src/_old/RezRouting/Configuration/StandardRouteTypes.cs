using System.Collections.Generic;

namespace RezRouting.Configuration
{
    internal static class StandardRouteTypes
    {
        internal static IEnumerable<RouteType> Build()
        {
            return new List<RouteType>
            {
                new RouteType("Index", new[] {ResourceType.Collection}, "Index",
                    StandardHttpMethod.Get, 0,
                    customize: settings => settings.PathSegment = ""),
                new RouteType("Show", new[] {ResourceType.Singular, ResourceType.Collection},
                    "Show", StandardHttpMethod.Get, 3, settings => settings.CollectionLevel = CollectionLevel.Item),
                new RouteType("New", new[] {ResourceType.Singular, ResourceType.Collection}, "New", StandardHttpMethod.Get, 1,
                    customize: settings =>
                    {
                        settings.PathSegment = "new";
                        settings.CollectionLevel = CollectionLevel.Collection;
                    }),
                new RouteType("Create", new[] {ResourceType.Singular, ResourceType.Collection}, "Create", StandardHttpMethod.Post, 4, 
                    customize: settings => settings.CollectionLevel = CollectionLevel.Collection),
                new RouteType("Edit", new[] {ResourceType.Singular, ResourceType.Collection},
                    "Edit", StandardHttpMethod.Get, 2,
                    customize: settings =>
                    {
                        settings.PathSegment = "edit";
                        settings.CollectionLevel = CollectionLevel.Item;
                    }),
                new RouteType("Update", new[] {ResourceType.Singular, ResourceType.Collection},
                    "Update", StandardHttpMethod.Put, 5, customize: settings =>
                    {
                        settings.CollectionLevel = CollectionLevel.Item;
                    }),
                new RouteType("Delete", new[] {ResourceType.Singular, ResourceType.Collection},
                    "Destroy", StandardHttpMethod.Delete, 6, customize: settings =>
                    {
                        settings.CollectionLevel = CollectionLevel.Item;
                    })
            };
        }
    }
}