using System.Linq;
using FluentAssertions;
using RezRouting2.Options;
using Xunit;

namespace RezRouting2.Tests
{
    public class ResourceUrlTests
    {
        private readonly RouteMappingContext context = new RouteMappingContext(Enumerable.Empty<RouteType>(), new OptionsBuilder().Build());

        [Fact]
        public void top_level_resource_urls_should_be_based_on_directory()
        {
            var singular = new SingularBuilder("Profile").Build(context);
            singular.Url.Should().Be("profile");

            var collection = new CollectionBuilder("Products").Build(context);
            collection.Url.Should().Be("products");
        }

        [Fact]
        public void nested_singular_resource_urls_should_include_ancestor_paths()
        {
            var builder = new SingularBuilder("Profile");
            builder.Singular("User", user =>
            {
                user.Singular("Status", status => { });
            });
            var level0 = builder.Build(context);
            var level1 = level0.Children.Single();
            var level2 = level1.Children.Single();

            level1.Url.Should().Be("profile/user");
            level2.Url.Should().Be("profile/user/status");
        }

        [Fact]
        public void collection_item_urls_should_combine_parent_path_and_id_parameter()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(x => {});
            var collection = builder.Build(context);
            var item = collection.Children.Single();
            item.Url.Should().Be("products/{id}");
        }

        [Fact]
        public void nested_collection_resource_urls_should_include_ancestor_paths()
        {
            var builder = new SingularBuilder("Profile");
            builder.Collection("Logins", logins => logins.Items(items => { }));
            var level0 = builder.Build(context);
            var collection = level0.Children.Single();
            var collectionItem = collection.Children.Single();

            collection.Url.Should().Be("profile/logins");
            collectionItem.Url.Should().Be("profile/logins/{id}");
        }

        [Fact]
        public void child_resources_of_collection_item_should_use_ancestor_id_param_name()
        {
            var builder = new CollectionBuilder("Products");
            builder.Items(product => product.Collection("Reviews", reviews => {}));
            var products = builder.Build(context);
            var productItem = products.Children.Single();
            var reviewsItem = productItem.Children.Single().Children.Single();

            productItem.Url.Should().Be("products/{id}");
            reviewsItem.Url.Should().Be("products/{productId}/reviews/{id}");
        }
    }
}