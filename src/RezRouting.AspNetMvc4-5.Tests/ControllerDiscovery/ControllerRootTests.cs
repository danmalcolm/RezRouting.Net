using FluentAssertions;
using RezRouting.AspNetMvc.ControllerDiscovery;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers.Products;
using RezRouting.AspNetMvc.Tests.ControllerDiscovery.TestControllers2;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.ControllerDiscovery
{
    public class ControllerRootTests
    {
        [Fact]
        public void should_create_based_on_controller()
        {
            var root = ControllerRoot.From<HomeController>();

            Equals(root.Assembly, typeof(HomeController).Assembly).Should().BeTrue();
            root.Namespace.Should().Be(typeof (HomeController).Namespace);
        }

        [Fact]
        public void should_include_type_with_exact_root_namespace()
        {
            var root = ControllerRoot.From<HomeController>();
            root.Includes(typeof (HomeController)).Should().BeTrue();
        }

        [Fact]
        public void should_include_type_within_child_namespace()
        {
            var root = ControllerRoot.From<HomeController>(); 
            root.Includes(typeof(ProductIndexController)).Should().BeTrue();
        }

        [Fact]
        public void should_not_include_type_within_same_level_namespace_that_starts_with_root()
        {
            var root = ControllerRoot.From<HomeController>();
            root.Includes(typeof(Home2Controller)).Should().BeFalse();
        }
    }
}