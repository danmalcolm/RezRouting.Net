using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class CollectionConfigurationTests
    {
        private readonly ResourceOptions options = new ResourceOptions();

        [Fact]
        public void should_build_collection_resource()
        {
            var builder = new ResourceGraphBuilder();
            builder.Collection("Products", x => {});

            var root = builder.Build(new ResourceOptions());
            root.Children.Should().HaveCount(1);
            var collection = root.Children.Single();
            collection.Type.Should().Be(ResourceType.Collection);
            collection.Name.Should().Be("Products");
        }

        [Fact]
        public void collection_resource_should_contain_child_item_resource()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Type.Should().Be(ResourceType.CollectionItem);
            item.Parent.Should().Be(collection);
        }

        [Fact]
        public void item_name_should_be_based_on_collection_name_if_it_can_be_singularised()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("Product");
        }

        [Fact]
        public void item_name_should_use_alternative_format_if_collection_name_cannot_be_singularised()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Hamburgera", products => { });
            });

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("HamburgeraItem");
        }

        [Fact]
        public void should_use_custom_name_for_child_item_resource_if_specified()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", "AProduct", products => { });
            });

            collection.Children.Should().HaveCount(1);
            var item = collection.Children.Single();
            item.Name.Should().Be("AProduct");
        }

        [Fact]
        public void full_name_of_child_item_resource_should_include_parent_collection_name()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            var item = collection.Children.Single();
            item.FullName.Should().Be("Products.Product");
        }

        [Fact]
        public void full_names_of_nested_resources_should_include_parents()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                        product.Singular("Manufacturer", manufacturer => { });
                    });
                });
            });

            var expectedNames = new[] { "Products", "Products.Product", "Products.Product.Reviews", "Products.Product.Reviews.Review", "Products.Product.Manufacturer"};
            collection.Expand().Select(x => x.FullName).Should().BeEquivalentTo(expectedNames);
        }

        [Fact]
        public void should_combine_changes_from_item_configuration_actions()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(items =>
                    {
                        items.IdName("productCode");
                        items.IdNameAsAncestor("parentProductId");
                    });
                    products.Items(items => items.IdName("productId"));
                });
            });

            var item = collection.Children.Single();
            item.Url.Should().Be("products/{productId}");
            item.UrlAsAncestor.Should().Be("products/{parentProductId}");
        }

        [Fact]
        public void items_should_use_standard_id_name_by_default()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            var item = collection.Children.Single();
            item.Url.Should().Be("products/{id}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name_when_specified()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product => product.IdName("code"));
                });
            });

            var item = collection.Children.Single();
            item.Url.Should().Be("products/{code}");
        }

        [Fact]
        public void should_configure_items_to_use_custom_id_name_as_ancestor_when_specified()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product => product.IdNameAsAncestor("productCode"));
                });
            });

            var item = collection.Children.Single();
            item.UrlAsAncestor.Should().Be("products/{productCode}");
        }

        [Fact]
        public void url_path_should_be_based_on_resource_name_by_default()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            collection.Url.Should().Be("products");
        }

        [Fact]
        public void should_use_custom_url_path_when_specified()
        {
            var collection = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.UrlPath("myproducts");
                });
            });

            collection.Url.Should().Be("myproducts");
        }

        [Fact]
        public void should_throw_if_custom_url_path_invalid()
        {
            Action action = () => BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.UrlPath("prod*()ucts");
                });
            });
            action.ShouldThrow<ArgumentException>()
                .WithMessage("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.*")
                .Which.ParamName.Should().Be("path");
        }

        /// <summary>
        /// Configures resources using supplied action and returns the first child resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResource(Action<ResourceGraphBuilder> configure)
        {
            var builder = new ResourceGraphBuilder();
            configure(builder);
            var root = builder.Build(new ResourceOptions());
            return root.Children.Single();
        }
    }
}