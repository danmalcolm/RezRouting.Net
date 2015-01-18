using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceHierarchyConfigurationTests
    {
        private Resource root;
        private Resource collection;
        private Resource item;
        private Resource nestedCollection;
        private Resource singular;
        private Resource nestedSingular;
        private Resource nestedCollectionItem;

        public ResourceHierarchyConfigurationTests()
        {
            var builder = new ResourceGraphBuilder("");
            builder.Collection("Products", products =>
            {
                products.Items(product =>
                {
                    product.Collection("Reviews", reviews => { });
                });
            });
            builder.Singular("Profile", profile =>
            {
                profile.Singular("User", user => {});
            });
            root = builder.Build(new ResourceOptions());
            collection = root.Children.Single(x => x.Name == "Products");
            item = collection.Children.Single(x => x.Name == "Product");
            nestedCollection = item.Children.Single(x => x.Name == "Reviews");
            nestedCollectionItem = nestedCollection.Children.Single(x => x.Name == "Review");
            singular = root.Children.Single(x => x.Name == "Profile");
            nestedSingular = singular.Children.Single(x => x.Name == "User");
        }

        [Fact]
        public void root_resource_should_not_have_ancestors()
        {
            root.Ancestors.Should().BeEmpty();
        }

        [Fact]
        public void child_resources_should_have_root_as_ancestor()
        {
            collection.Ancestors.Should().Equal(new [] { root });
            singular.Ancestors.Should().Equal(new[] { root });
        }

        [Fact]
        public void collection_item_should_have_parent_and_root_as_ancestor()
        {
            item.Ancestors.Should().Equal(new[] { collection, root });
        }

        [Fact]
        public void nested_collection_should_have_parent_item_and_collection_as_ancestors()
        {
            nestedCollection.Ancestors.Should().Equal(new[] { item, collection, root });
        }

        [Fact]
        public void nested_collection_item_should_have_parent_collection_and_its_ancestors_as_ancestors()
        {
            nestedCollectionItem.Ancestors.Should().Equal(new[] { nestedCollection, item, collection, root });
        }

        [Fact]
        public void nested_singular_should_have_parent_as_ancestor()
        {
            nestedSingular.Ancestors.Should().Equal(new[] { singular, root });
        }
    }
}