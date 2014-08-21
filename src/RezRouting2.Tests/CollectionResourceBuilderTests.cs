using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class CollectionResourceBuilderTests
    {
        [Fact]
        public void should_build_collection_resource()
        {
            var builder = new CollectionResourceBuilder("Products");
            
            var resource = builder.Build();
            
            resource.Should().NotBeNull();
            resource.Level.Should().Be(ResourceLevel.Collection);
        }

        [Fact]
        public void should_build_collection_resource_with_child_for_collection_items()
        {
            var builder = new CollectionResourceBuilder("Products");
            builder.Items(items => {});

            var resource = builder.Build();

            resource.Children.Should().HaveCount(1);
            var itemLevelResource = resource.Children.Single();
            itemLevelResource.Level.Should().Be(ResourceLevel.CollectionItem);
        }
    }
}