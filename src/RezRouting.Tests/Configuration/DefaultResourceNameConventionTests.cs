using FluentAssertions;
using RezRouting.Configuration;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class DefaultResourceNameConventionTests
    {
        private DefaultResourceNameConvention convention = new DefaultResourceNameConvention();

        [Fact]
        public void ShouldUseSingularNameFromControllerForSingularResource()
        {
            convention.GetResourceName(new[] {typeof (ThingController)}, ResourceType.Singular)
                .Should().Be("Thing");
        }
        
        [Fact]
        public void ShouldSingularizePluralNameFromControllerForSingularResource()
        {
            convention.GetResourceName(new[] { typeof(ThingsController) }, ResourceType.Singular)
                .Should().Be("Thing");
        }

        [Fact]
        public void ShouldUseCommonStartFromMultipleControllersForSingularResource()
        {
            var types = new[] { typeof(ThingDisplayController), typeof(ThingEditController) };
            convention.GetResourceName(types, ResourceType.Singular)
                .Should().Be("Thing");
        }


        [Fact]
        public void ShouldUsePluralNameInControllerForCollectionResource()
        {
            convention.GetResourceName(new[] { typeof(ThingsController) }, ResourceType.Collection)
                .Should().Be("Things");
        }

        [Fact]
        public void ShouldPluralizeSingularNameInControllerForCollectionResource()
        {
            convention.GetResourceName(new[] { typeof(ThingController) }, ResourceType.Collection)
                .Should().Be("Things");
        }

        [Fact]
        public void ShouldUseCommonStartFromMultipleControllersForCollectionResource()
        {
            var types = new[] { typeof(ThingDisplayController), typeof(ThingEditController) };
            convention.GetResourceName(types, ResourceType.Collection)
                .Should().Be("Things");
        }

        [Fact]
        public void ShouldNotFindNameFromControllersWithoutCommonStart()
        {
            var types = new[] { typeof(ThingController), typeof(SchmingController) };
            convention.GetResourceName(types, ResourceType.Collection)
                .Should().Be("");
        }

        public class ThingController
        {
        }

        public class ThingsController
        {
            
        }

        public class ThingEditController
        {
            
        }

        public class ThingDisplayController
        {
            
        }

        public class SchmingController
        {
        }
    }
}