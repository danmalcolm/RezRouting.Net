using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Options;
using RezRouting.Tests.Infrastructure;
using RezRouting.Tests.Utility;
using Xunit;

namespace RezRouting.Tests
{
    public class CollectionBuilderTests
    {
        private readonly RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<IRouteConvention>(), new OptionsBuilder().Build());

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
        public void full_name_of_top_level_resource_should_be_resource_name_only()
        {
            var builder = new CollectionBuilder("Products");

            var collection = builder.Build(context);

            collection.FullName.Should().Be("Products");
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
        public void child_item_resource_name_should_be_singular_of_collection_name_by_default()
        {
            var builder = new CollectionBuilder("Products");

            var collection = builder.Build(context);

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("Product");
        }

        [Fact]
        public void child_item_resource_name_should_be_based_on_collection_name_and_item_if_name_cannot_be_singularised()
        {
            var builder = new CollectionBuilder("Hamburgera");

            var collection = builder.Build(context);

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("HamburgeraItem");
        }

        [Fact]
        public void should_use_custom_name_for_child_item_resource_if_specified()
        {
            var builder = new CollectionBuilder("Products", "AProduct");

            var collection = builder.Build(context);

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("AProduct");
        }

        [Fact]
        public void full_name_of_child_item_resource_should_include_parent_collection()
        {
            var builder = new CollectionBuilder("Products");

            var collection = builder.Build(context);

            var item = collection.Children.Single();
            item.FullName.Should().Be("Products.Product");
        }

        [Fact]
        public void full_name_of_nested_resources_should_include_parents()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(product =>
            {
                product.Collection("Reviews", reviews => { });
                product.Singular("Manufacturer", manufacturer => { });
            });

            var collection = builder.Build(context);

            var expectedNames = new[] { "Products", "Products.Product", "Products.Product.Reviews", "Products.Product.Reviews.Review", "Products.Product.Manufacturer"};
            collection.Expand().Select(x => x.FullName).Should().BeEquivalentTo(expectedNames);
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
            item.Url.Should().Be("products/{productId}");
        }

        [Fact]
        public void items_should_use_standard_id_name_by_default()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(items => { });

            var collection = builder.Build(context);
            var item = collection.Children.Single();
            item.Url.Should().Be("products/{id}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name()
        {
            var builder = new CollectionBuilder("Users");
            builder.Items(items => items.IdName("userName"));

            var collection = builder.Build(context);
            var item = collection.Children.Single();
            item.Url.Should().Be("users/{userName}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name_as_ancestor()
        {
            var builder = new CollectionBuilder("Users");
            builder.Items(items =>
            {
                items.IdNameAsAncestor("parentId");
                items.Collection("Comments", comments => {});
            });

            var collection = builder.Build(context);
            var nestedItem = collection.Children.Single().Children.Single();
            nestedItem.Name.Should().Be("Comments");
            nestedItem.Url.Should().Be("users/{parentId}/comments");
        }

        [Fact]
        public void should_default_to_url_path_based_on_resource_name()
        {
            var builder = new CollectionBuilder("Products");

            var resource = builder.Build(context);

            resource.Url.Should().Be("products");
        }

        [Fact]
        public void should_use_customised_url_path()
        {
            var builder = new CollectionBuilder("Products");

            builder.UrlPath("myproducts");
            var resource = builder.Build(context);

            resource.Url.Should().Be("myproducts");
        }

        [Fact]
        public void should_throw_if_custom_url_path_invalid()
        {
            var builder = new CollectionBuilder("Products");

            Action action = () => builder.UrlPath("prod*()ucts");
            action.ShouldThrow<ArgumentException>()
                .WithMessage("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.*")
                .Which.ParamName.Should().Be("path");
        }
    }
}