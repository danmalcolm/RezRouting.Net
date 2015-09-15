using System.Collections.Generic;
using RezRouting.AspNetMvc.RouteConventions.Display;
using RezRouting.AspNetMvc.Tests.Infrastructure.Assertions;
using RezRouting.AspNetMvc.Tests.RouteConventions.Display.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.RouteConventions.Display.TestControllers.Products.Product;
using RezRouting.AspNetMvc.Tests.RouteConventions.Display.TestControllers.Profile;
using RezRouting.Resources;
using RezRouting.Tests.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Display
{
    public class DisplayRouteConventionsTests : ConfigurationTestsBase
    {
        private static readonly Dictionary<string, Resource> Resources;

        static DisplayRouteConventionsTests()
        {
            Resources = BuildResources(root =>
            {
                root.Extension(new DisplayRouteConventions());
                root.Collection("Products", products =>
                {
                    products.Controller<ProductIndexController>();
                    products.Items(product => product.Controller<ProductDetailsController>());
                });
                root.Singular("Profile", profile => profile.Controller<ProfileDetailsController>());
            });
        }

        [Fact]
        public void should_map_singular_show_route()
        {
            var profile = Resources["Profile"];
            profile.ShouldContainMvcRoute("Show", typeof(ProfileDetailsController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_collection_index_route()
        {
            var products = Resources["Products"];
            products.ShouldContainMvcRoute("Index", typeof(ProductIndexController), "Index", "GET", "");
        }

        [Fact]
        public void should_map_collection_item_show_route()
        {
            var product = Resources["Products.Product"];
            product.ShouldContainMvcRoute("Show", typeof(ProductDetailsController), "Show", "GET", "");
        }
    }
}