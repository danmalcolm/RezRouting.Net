using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Products2;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests customisations available at resource level
    /// </summary>
    public class ResourceCustomisationTests
    {
        [Fact]
        public void ShouldUseDefaultConventionForResourceNameAndPathWithMultipleControllers()
        {
            var builder = new RouteMapper();
            builder.Collection(products
                => products.HandledBy<ProductsDisplayController, ProductsEditController>());

            builder.ShouldMapRoutesWithNames("Products.Index", "Products.Show", 
                "Products.New", "Products.Create", "Products.Edit", "Products.Update",
                "Products.Delete");
        }

        [Fact]
        public void ShouldUseCustomNameIfSpecifiedAtResourceLevel()
        {
            var mapper = new RouteMapper();
            mapper.Collection(products =>
            {
                products.HandledBy<ProductsDisplayController, ProductsEditController>();
                products.CustomName("Salamander");
            });

            mapper.ShouldMapRoutesWithNames("Salamanders.Index", "Salamanders.Show",
                "Salamanders.New", "Salamanders.Create", "Salamanders.Edit",
                "Salamanders.Update", "Salamanders.Delete");
        }

        [Fact]
        public void ShouldUseCustomPathInRouteUrlsIfSpecified()
        {
            var builder = new RouteMapper();
            builder.Collection(products =>
            {
                products.CustomUrlPath("salamanders");
                products.HandledBy<ProductsDisplayController, ProductsEditController>();
            });

            builder.ShouldMapRoutesWithUrls("salamanders", "salamanders/{id}",
                "salamanders/new", "salamanders", "salamanders/{id}/edit",
                "salamanders/{id}", "salamanders/{id}");
        }

        [Fact]
        public void ShouldOnlyMapSpecifiedRoutesWhenUsingIncludeFilter()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users =>
            {
                users.Include("Index", "Show");
                users.HandledBy<UsersController>();
            });
            mapper.ShouldMapRoutesWithNames("Users.Index", "Users.Show");
        }

        [Fact]
        public void ShouldOnlyMapSpecifiedRoutesWhenUsingExcludeFilter()
        {
            var mapper = new RouteMapper();
            mapper.Collection(users =>
            {
                users.Exclude("Delete");
                users.HandledBy<UsersController>();
            });
            mapper.ShouldMapRoutesWithNames("Users.Index", "Users.Show", "Users.New", 
                "Users.Create", "Users.Edit", "Users.Update");
        
        }
    }
}