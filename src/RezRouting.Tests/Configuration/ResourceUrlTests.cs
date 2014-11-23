using System;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceUrlTests
    {
        private ResourcesModel BuildResources(Action<ResourcesBuilder> configure)
        {
            var builder = new ResourcesBuilder();
            configure(builder);
            var model = builder.Build();
            return model;
        }

        [Fact]
        public void top_level_singular_urls_should_be_based_on_directory()
        {
            var model = BuildResources(builder => 
                builder.Singular("Profile", x => {})
            );

            var resource = model.Resources.Single();
            resource.Url.Should().Be("profile");
        }

        [Fact]
        public void top_level_collection_urls_should_be_based_on_directory()
        {
            var model = BuildResources(builder => 
                builder.Collection("Products", x => { })
            );

            var collection = model.Resources.Single();
            collection.Url.Should().Be("products");
        }
        
        [Fact]
        public void nested_singular_urls_should_include_ancestor_paths()
        {
            var model = BuildResources(builder =>
            {
                builder.Singular("Profile", profile =>
                {
                    profile.Singular("User", user =>
                    {
                        user.Singular("Status", status => { });
                    });
                });
            });

            var level0 = model.Resources.Single();
            var level1 = level0.Children.Single();
            var level2 = level1.Children.Single();

            level1.Url.Should().Be("profile/user");
            level2.Url.Should().Be("profile/user/status");
        }

        [Fact]
        public void collection_item_urls_should_combine_parent_path_and_id_parameter()
        {
            var model = BuildResources(builder =>
                builder.Collection("Products", x => { })
            ); 
            var collection = model.Resources.Single();
            var item = collection.Children.Single();
            item.Url.Should().Be("products/{id}");
        }

        [Fact]
        public void nested_collection_resource_urls_should_include_ancestor_paths()
        {
            var model = BuildResources(builder =>
            {
                builder.Singular("Profile", profile =>
                {
                    profile.Collection("Logins", logins => { });
                });
            });

            var level0 = model.Resources.Single();
            var collection = level0.Children.Single();
            var collectionItem = collection.Children.Single();

            collection.Url.Should().Be("profile/logins");
            collectionItem.Url.Should().Be("profile/logins/{id}");
        }

        [Fact]
        public void collection_item_ancestor_resources_should_use_ancestor_id_param_name()
        {
            var model = BuildResources(builder =>
            {
                builder.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Collection("Reviews", reviews => { });
                    });
                });
            });

            var collection = model.Resources.Single();
            var collectionItem = collection.Children.Single();
            var nestedItem = collectionItem.Children.Single().Children.Single();

            collectionItem.Url.Should().Be("products/{id}");
            nestedItem.Url.Should().Be("products/{productId}/reviews/{id}");
        }
    }
}