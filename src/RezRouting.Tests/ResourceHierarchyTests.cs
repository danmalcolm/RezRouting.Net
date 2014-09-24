﻿using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting.Tests
{
    public class ResourceHierarchyTests
    {
        private Resource collection;
        private Resource item;
        private Resource nestedCollection;
        private Resource singular;
        private Resource nestedSingular;
        private Resource nestedCollectionItem;

        public ResourceHierarchyTests()
        {
            var mapper = new RouteMapper();
            mapper.Collection("Products", products =>
            {
                products.Items(product => product.Collection("Reviews", reviews =>
                {

                }));
            });
            mapper.Singular("Profile", profile =>
            {
                profile.Singular("User", user => {});
            });
            var resources = mapper.Build().ToList();
            collection = resources.Single(x => x.Name == "Products");
            item = collection.Children.Single(x => x.Name == "Product");
            nestedCollection = item.Children.Single(x => x.Name == "Reviews");
            nestedCollectionItem = nestedCollection.Children.Single(x => x.Name == "Review");
            singular = resources.Single(x => x.Name == "Profile");
            nestedSingular = singular.Children.Single(x => x.Name == "User");
        }

        [Fact]
        public void top_level_resources_should_not_have_ancestors()
        {
            collection.Ancestors.Should().BeEmpty();
            singular.Ancestors.Should().BeEmpty();
        }

        [Fact]
        public void collection_item_should_have_parent_as_ancestor()
        {
            item.Ancestors.Should().Equal(new[] { collection });
        }

        [Fact]
        public void nested_collection_should_have_parent_item_and_collection_as_ancestors()
        {
            nestedCollection.Ancestors.Should().Equal(new[] { item, collection });
        }

        [Fact]
        public void nested_collection_item_should_have_parent_collection_and_its_ancestors()
        {
            nestedCollectionItem.Ancestors.Should().Equal(new[] { nestedCollection, item, collection });
        }

        [Fact]
        public void nested_singular_should_have_parent_as_ancestor()
        {
            nestedSingular.Ancestors.Should().Equal(new[] {singular});
        }
    }
}