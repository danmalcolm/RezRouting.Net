using FluentAssertions;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceUrlTests : ConfigurationTestsBase
    {
        [Fact]
        public void root_url_should_be_empty()
        {
            var resources = BuildResources(root => { });

            resources[""].Url.Should().Be("");
        }

        [Fact]
        public void top_level_singular_urls_should_be_based_on_resource_name()
        {
            var resources = BuildResources(root => 
                root.Singular("Profile", x => {})
            );

            resources["Profile"].Url.Should().Be("profile");
        }

        [Fact]
        public void top_level_collection_urls_should_be_based_on_resource_name()
        {
            var resources = BuildResources(root => 
                root.Collection("Products", x => { })
            );

            resources["Products"].Url.Should().Be("products");
        }

        [Fact]
        public void collection_item_urls_should_combine_collection_path_and_id_parameter()
        {
            var resources = BuildResources(root =>
                root.Collection("Products", x => { })
            ); 

            resources["Products.Product"].Url.Should().Be("products/{id}");
        }

        [Fact]
        public void descendants_of_singular_resources_should_include_ancestor_paths()
        {
            var resources = BuildResources(root =>
            {
                root.Singular("Profile", profile =>
                {
                    profile.Singular("User", user =>
                    {
                        user.Singular("Status", status => { });
                    });
                    profile.Collection("Logins", logins => { });
                });
            });

            resources["Profile.User"].Url.Should().Be("profile/user");
            resources["Profile.User.Status"].Url.Should().Be("profile/user/status");
            resources["Profile.Logins"].Url.Should().Be("profile/logins");
            resources["Profile.Logins.Login"].Url.Should().Be("profile/logins/{id}");
        }

        [Fact]
        public void descendants_of_collection_resources_should_include_ancestor_paths()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Collection("Reviews", reviews => {});
                    products.Singular("Top", top => {});
                });
            });

            resources["Products.Reviews"].Url.Should().Be("products/reviews");
            resources["Products.Reviews.Review"].Url.Should().Be("products/reviews/{id}");
            resources["Products.Top"].Url.Should().Be("products/top");
        }
        
        [Fact]
        public void descendants_of_collection_items_should_include_ancestor_id_param_name()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Singular("Status", status => {});
                        product.Collection("Reviews", reviews => { });
                    });
                });
            });

            resources["Products.Product.Status"].Url.Should().Be("products/{productId}/status");
            resources["Products.Product.Reviews"].Url.Should().Be("products/{productId}/reviews");
            resources["Products.Product.Reviews.Review"].Url.Should().Be("products/{productId}/reviews/{id}");
        }

        [Fact]
        public void descendants_of_collection_items_should_use_alternative_ancestor_id_param_name_if_specified()
        {
            var resources = BuildResources(root =>
            {
                root.Collection("Products", products =>
                {
                    products.Items(product =>
                    {
                        product.Singular("Status1", status => { });
                        product.Singular("Status2", status =>
                        {
                            status.AncestorIdName("objectId");
                        });
                        product.Collection("Reviews", reviews => { });
                        product.Collection("Comments", comments =>
                        {
                            comments.AncestorIdName("parentObjectId");
                        });
                    });
                });
            });

            resources["Products.Product.Status1"].Url.Should().Be("products/{productId}/status1");
            resources["Products.Product.Status2"].Url.Should().Be("products/{objectId}/status2");
            
            resources["Products.Product.Reviews"].Url.Should().Be("products/{productId}/reviews");
            resources["Products.Product.Reviews.Review"].Url.Should().Be("products/{productId}/reviews/{id}");

            resources["Products.Product.Comments"].Url.Should().Be("products/{parentObjectId}/comments");
            resources["Products.Product.Comments.Comment"].Url.Should().Be("products/{parentObjectId}/comments/{id}");
        }
    }
}