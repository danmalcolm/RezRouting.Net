using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class NestedResourceConfigurationTests
    {
        [Fact]
        public void singular_resource_should_not_have_children_if_none_configured()
        {
            var resource = BuildResource(root =>
            {
                root.Singular("Profile", profile => { });
            });

            resource.Children.Should().BeEmpty();
        }

        [Fact]
        public void collection_resource_should_only_contain_item_resource_if_no_children_configured()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products => { });
            });

            resource.Children.Should().HaveCount(1);
            resource.Children.Single().Type.Should().Be(ResourceType.CollectionItem);
        }
        
        [Fact]
        public void should_add_child_singular_resource_to_parent_singular_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Singular("Profile", profile =>
                {
                    profile.Singular("User", user => {});
                });
            });

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Type.Should().Be(ResourceType.Singular);
            child.Name.Should().Be("User");
            child.Parent.Should().Be(resource);
        }

        [Fact]
        public void should_add_child_collection_resource_to_parent_singular_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Singular("Profile", profile =>
                {
                    profile.Collection("Logins", logins => { });
                });
            });

            resource.Children.Should().HaveCount(1);
            var child = resource.Children.Single();
            child.Should().NotBeNull();
            child.Type.Should().Be(ResourceType.Collection);
            child.Name.Should().Be("Logins");
            child.Parent.Should().Be(resource);
        }

        [Fact]
        public void should_add_child_singular_resource_to_parent_collection_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Singular("Top", top => { });
                });
            });

            var children = resource.Children.Where(x => x.Type != ResourceType.CollectionItem).ToList();
            children.Should().HaveCount(1);
            var child = children.Single();
            child.Should().NotBeNull();
            child.Type.Should().Be(ResourceType.Singular);
            child.Name.Should().Be("Top");
            child.Parent.Should().Be(resource);
        }

        [Fact]
        public void should_add_child_collection_resource_to_parent_collection_resource()
        {
            var resource = BuildResource(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Collection("Reviews", reviews => { });
                });
            });

            var children = resource.Children.Where(x => x.Type != ResourceType.CollectionItem).ToList();
            children.Should().HaveCount(1);
            var child = children.Single();
            child.Should().NotBeNull();
            child.Type.Should().Be(ResourceType.Collection);
            child.Name.Should().Be("Reviews");
            child.Parent.Should().Be(resource);
        }

        /// <summary>
        /// Configures resources using supplied action and returns the first child resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResource(Action<IRootResourceBuilder> configure)
        {
            var builder = RootResourceBuilder.Create();
            configure(builder);
            var root = builder.Build();
            return root.Children.Single();
        }
    }
}