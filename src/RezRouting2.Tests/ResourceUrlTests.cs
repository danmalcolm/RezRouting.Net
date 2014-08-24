using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class ResourceUrlTests
    {
        [Fact]
        public void top_level_resource_urls_should_be_based_on_directory()
        {
            var singular = new SingularBuilder("Profile").Build();
            singular.UrlPath.Should().Be("Profile");

            var collection = new CollectionBuilder("Products").Build();
            collection.UrlPath.Should().Be("Products");
        }

        [Fact]
        public void nested_singular_resource_urls_should_include_ancestor_paths()
        {
            var builder = new SingularBuilder("Profile");
            builder.Singular("User", user =>
            {
                user.Singular("Status", status => { });
            });
            var level0 = builder.Build();
            var level1 = level0.Children.Single();
            var level2 = level1.Children.Single();

            level1.UrlPath.Should().Be("Profile/User");
            level2.UrlPath.Should().Be("Profile/User/Status");
        }

        [Fact]
        public void collection_item_urls_should_combine_parent_path_and_id_parameter()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(x => {});
            var collection = builder.Build();
            var item = collection.Children.Single();
            item.UrlPath.Should().Be("Products/{id}");
        }

        [Fact]
        public void nested_collection_resource_urls_should_include_ancestor_paths()
        {
            var builder = new SingularBuilder("Profile");
            builder.Collection("Logins", logins => logins.Items(items => { }));
            var level0 = builder.Build();
            var collection = level0.Children.Single();
            var collectionItem = collection.Children.Single();

            collection.UrlPath.Should().Be("Profile/Logins");
            collectionItem.UrlPath.Should().Be("Profile/Logins/{id}");
        }

        [Fact]
        public void child_resources_of_collection_item_should_use_ancestor_id_param_name()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(item => item.Collection("Reviews", reviews => {}));
            var products = builder.Build();
            var productItem = products.Children.Single();
            var reviewsItem = productItem.Children.Single().Children.Single();

            productItem.UrlPath.Should().Be("Products/{id}");
            reviewsItem.UrlPath.Should().Be("Products/{productId}/Reviews/{id}");
        }

    }
}