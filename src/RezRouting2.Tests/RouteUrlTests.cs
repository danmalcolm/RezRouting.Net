using System.Linq;
using FluentAssertions;
using Xunit;

namespace RezRouting2.Tests
{
    public class RouteUrlTests
    {
        [Fact]
        public void route_urls_should_include_parent_resource_url()
        {
            var routeType = new RouteType("RouteType1", 
                (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));
            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType);
            mapper.Singular("Profile", profile =>
            {
                profile.HandledBy<TestController>();
                profile.Singular("User", user => user.HandledBy<TestController>());
            });
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController>();
                products.Items(product =>
                {
                    product.HandledBy<TestController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<TestController>();
                        reviews.Items(review => review.HandledBy<TestController>());
                    });
                });
            });

            var resources = mapper.Build().ToList();

            var level1Singular = resources.Single(x => x.Level == ResourceLevel.Singular);
            var level2Singular = level1Singular.Children.Single();
            var level1Collection = resources.Single(x => x.Level == ResourceLevel.Collection);
            var level1Item = level1Collection.Children.Single(x => x.Level == ResourceLevel.CollectionItem);
            var level2Collection = level1Item.Children.Single();
            var level2Item = level2Collection.Children.Single(x => x.Level == ResourceLevel.CollectionItem);

            level1Singular.Routes.Single().Url.Should().Be("Profile/action1");
            level2Singular.Routes.Single().Url.Should().Be("Profile/User/action1");
            level1Collection.Routes.Single().Url.Should().Be("Products/action1");
            level1Item.Routes.Single().Url.Should().Be("Products/{id}/action1");
            level2Collection.Routes.Single().Url.Should().Be("Products/{productId}/Reviews/action1");
            level2Item.Routes.Single().Url.Should().Be("Products/{productId}/Reviews/{id}/action1");
        }

        [Fact]
        public void route_urls_for_routes_with_empty_path_should_match_parent_resource_url()
        {
            var routeType = new RouteType("RouteType2",
                (resource, type, route) => route.Configure("Route1", "Action1", "GET", ""));

            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType);
            mapper.Singular("Profile", profile => profile.HandledBy<TestController>());
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController>();
                products.Items(product => product.HandledBy<TestController>());
            });

            var resources = mapper.Build().ToList();

            var singular = resources.Single(x => x.Level == ResourceLevel.Singular);
            var collection = resources.Single(x => x.Level == ResourceLevel.Collection);
            var collectionItem = collection.Children.Single(x => x.Level == ResourceLevel.CollectionItem);

            singular.Routes.Single().Url.Should().Be(singular.Url);
            collection.Routes.Single().Url.Should().Be(collection.Url);
            collectionItem.Routes.Single().Url.Should().Be(collectionItem.Url);
        }

        private class TestController
        {

        }

    }
}