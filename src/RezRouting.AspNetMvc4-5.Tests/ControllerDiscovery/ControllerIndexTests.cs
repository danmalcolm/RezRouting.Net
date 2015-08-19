using FluentAssertions;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product.Reviews;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products.Product.Reviews.Review;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers2;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers2.Products;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.ControllerDiscovery
{
    public class ControllerIndexTests
    {
        [Fact]
        public void should_index_controllers_with_key_based_on_namespace()
        {
            var rootController = typeof (HomeController);
            var root = new ControllerRoot(rootController.Assembly, rootController.Namespace);
            var index = ControllerIndex.Create(new [] { root });

            var expectedItems = new[]
            {
                new {Key = "", Type = typeof (HomeController)},
                new {Key = "Products", Type = typeof (ProductIndexController)},
                new {Key = "Products.Product", Type = typeof (ProductDetailsController)},
                new {Key = "Products.Product.Reviews", Type = typeof (ReviewIndexController)},
                new {Key = "Products.Product.Reviews.Review", Type = typeof (ReviewDetailsController)},
            };
            index.Items.ShouldAllBeEquivalentTo(expectedItems);
        }

        [Fact]
        public void should_index_controllers_from_multiple_root_namespaces()
        {
            var root1 = ControllerRoot.From<HomeController>();
            var root2 = ControllerRoot.From<Home2Controller>();
            var index = ControllerIndex.Create(new[] { root1, root2 });

            var expectedItems = new[]
            {
                new {Key = "", Type = typeof (HomeController)},
                new {Key = "", Type = typeof (Home2Controller)},
                new {Key = "Products", Type = typeof (ProductIndexController)},
                new {Key = "Products", Type = typeof (ProductIndex2Controller)},
                new {Key = "Products.Product", Type = typeof (ProductDetailsController)},
                new {Key = "Products.Product.Reviews", Type = typeof (ReviewIndexController)},
                new {Key = "Products.Product.Reviews.Review", Type = typeof (ReviewDetailsController)},
            };
            index.Items.ShouldAllBeEquivalentTo(expectedItems);
        }
    }
}