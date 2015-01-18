using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceUrlTests
    {
        private Resource BuildResources(Action<ISingularConfigurator> configure)
        {
            var root = new ResourceGraphBuilder("");
            configure(root);
            var resource = root.Build(new ResourceOptions());
            return resource;
        }

        [Fact]
        public void top_level_singular_urls_should_be_based_on_directory()
        {
            var root = BuildResources(builder => 
                builder.Singular("Profile", x => {})
            );

            var resource = root.Children.Single();
            resource.Url.Should().Be("profile");
        }

        [Fact]
        public void top_level_collection_urls_should_be_based_on_directory()
        {
            var root = BuildResources(builder => 
                builder.Collection("Products", x => { })
            );

            var collection = root.Children.Single();
            collection.Url.Should().Be("products");
        }
        
        [Fact]
        public void nested_singular_urls_should_include_ancestor_paths()
        {
            var root = BuildResources(builder =>
            {
                builder.Singular("Profile", profile =>
                {
                    profile.Singular("User", user =>
                    {
                        user.Singular("Status", status => { });
                    });
                });
            });

            var level0 = root.Children.Single();
            var level1 = level0.Children.Single();
            var level2 = level1.Children.Single();

            level1.Url.Should().Be("profile/user");
            level2.Url.Should().Be("profile/user/status");
        }

        [Fact]
        public void collection_item_urls_should_combine_parent_path_and_id_parameter()
        {
            var root = BuildResources(builder =>
                builder.Collection("Products", x => { })
            ); 
            var collection = root.Children.Single();
            var item = collection.Children.Single();
            item.Url.Should().Be("products/{id}");
        }

        [Fact]
        public void nested_collection_resource_urls_should_include_ancestor_paths()
        {
            var root = BuildResources(builder =>
            {
                builder.Singular("Profile", profile =>
                {
                    profile.Collection("Logins", logins => { });
                });
            });

            var level0 = root.Children.Single();
            var collection = level0.Children.Single();
            var collectionItem = collection.Children.Single();

            collection.Url.Should().Be("profile/logins");
            collectionItem.Url.Should().Be("profile/logins/{id}");
        }

        [Fact]
        public void collection_item_ancestor_resources_should_use_ancestor_id_param_name()
        {
            var root = BuildResources(builder =>
            {
                builder.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                    });
                });
            });

            var collection = root.Children.Single();
            var collectionItem = collection.Children.Single();
            var nestedItem = collectionItem.Children.Single().Children.Single();

            collectionItem.Url.Should().Be("products/{id}");
            nestedItem.Url.Should().Be("products/{productId}/reviews/{id}");
        }
    }
}