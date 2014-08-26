using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class CollectionBuilderTests
    {
        private readonly RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<RouteType>());

        [Fact]
        public void should_build_collection_resource()
        {
            var builder = new CollectionBuilder("Products");
            
            var collection = builder.Build(context);
            
            collection.Should().NotBeNull();
            collection.Level.Should().Be(ResourceLevel.Collection);
            collection.Name.Should().Be("Products");
        }

        [Fact]
        public void collection_resource_should_contain_child_item_resource()
        {
            var builder = new CollectionBuilder("Products");

            var collection = builder.Build(context);

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Level.Should().Be(ResourceLevel.CollectionItem);
            item.Parent.Should().Be(collection);
        }

        [Fact]
        public void child_item_resource_name_should_be_singular_of_collection_name()
        {
            var builder = new CollectionBuilder("Products");

            var collection = builder.Build(context);

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("Product");
        }

        [Fact]
        public void should_combine_changes_to_item_configuration()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(items => items.IdName("productCode"));
            builder.Items(items => items.IdName("productId"));

            var collection = builder.Build(context);
            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.UrlPath.Should().Be("Products/{productId}");
        }

        [Fact]
        public void items_should_use_standard_id_name_by_default()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(items => { });

            var collection = builder.Build(context);
            var item = collection.Children.Single();
            item.UrlPath.Should().Be("Products/{id}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name()
        {
            var builder = new CollectionBuilder("Users");
            builder.Items(items => items.IdName("userName"));

            var collection = builder.Build(context);
            var item = collection.Children.Single();
            item.UrlPath.Should().Be("Users/{userName}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name_as_ancestor()
        {
            var builder = new CollectionBuilder("Users");
            builder.Items(items =>
            {
                items.IdNameAsAncestor("parentId");
                items.Collection("Comments", logins => {});
            });

            var collection = builder.Build(context);
            var nestedItem = collection.Children.Single().Children.Single();
            nestedItem.Name.Should().Be("Comments");
            nestedItem.UrlPath.Should().Be("Users/{parentId}/Comments");
        }
    }
}