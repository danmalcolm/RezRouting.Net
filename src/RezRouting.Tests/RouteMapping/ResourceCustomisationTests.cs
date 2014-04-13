using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.RouteMapping.TestControllers.Products2;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    public class ResourceCustomisationTests
    {
        [Fact]
        public void ShouldUseDefaultConventionForResourceNameAndPathWithMultipleControllers()
        {
            var builder = new RootResourceBuilder();
            builder.Collection(users => users.HandledBy<ProductsDisplayController,ProductsEditController>());
            var routes = builder.MapRoutes();
            var routeNames = new[]
            {
                "Products.Index", "Products.Show", "Products.New", "Products.Create", "Products.Edit", "Products.Update", "Products.Delete"
            };
            routes.OfType<ResourceActionRoute>().Select(x => x.Name).Should().Contain(routeNames);
        }

        [Fact]
        public void ShouldUseCustomNameIfSpecifiedAtResourceLevel()
        {
            var builder = new RootResourceBuilder();
            builder.Collection(products =>
            {
                products.CustomName("Salamanders");
                products.HandledBy<ProductsDisplayController, ProductsEditController>();
            });
            var routes = builder.MapRoutes();
            Console.Write(builder.DebugSummary());
            var routeNames = new[]
            {
                "Salamanders.Index", "Salamanders.Show", "Salamanders.New", "Salamanders.Create", "Salamanders.Edit", "Salamanders.Update", "Salamanders.Delete"
            };
            routes.OfType<ResourceActionRoute>().Select(x => x.Name).Should().Contain(routeNames);
        }

        [Fact]
        public void ShouldUseCustomPathInRouteUrlsIfSpecified()
        {
            var builder = new RootResourceBuilder();
            builder.Collection(products =>
            {
                products.CustomPath("salamanders");
                products.HandledBy<ProductsDisplayController, ProductsEditController>();
            });
            var routes = builder.MapRoutes();
            Console.Write(builder.DebugSummary());
            var expected = new[]
            {
                "salamanders", "salamanders/{id}", "salamanders/new", "salamanders", "salamanders/{id}/edit", "salamanders/{id}", "salamanders/{id}"
            };
            routes.OfType<ResourceActionRoute>().Select(x => x.Url).Should().Contain(expected);
        }
    }
}