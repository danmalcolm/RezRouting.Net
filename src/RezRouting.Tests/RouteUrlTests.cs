using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests
{
    public class RouteUrlTests
    {
        [Fact]
        public void route_urls_should_include_parent_resource_url()
        {
            var convention = new TestRouteConvention("Route1", "Action1", "GET", "action1");
            var mapper = new RouteMapper();
            mapper.RouteConventions(convention);
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

            var model = mapper.Build();

            var level1Singular = model.Resources.Single(x => x.Level == ResourceLevel.Singular);
            var level2Singular = level1Singular.Children.Single();
            var level1Collection = model.Resources.Single(x => x.Level == ResourceLevel.Collection);
            var level1Item = level1Collection.Children.Single(x => x.Level == ResourceLevel.CollectionItem);
            var level2Collection = level1Item.Children.Single();
            var level2Item = level2Collection.Children.Single(x => x.Level == ResourceLevel.CollectionItem);

            level1Singular.Routes.Single().Url.Should().Be("profile/action1");
            level2Singular.Routes.Single().Url.Should().Be("profile/user/action1");
            level1Collection.Routes.Single().Url.Should().Be("products/action1");
            level1Item.Routes.Single().Url.Should().Be("products/{id}/action1");
            level2Collection.Routes.Single().Url.Should().Be("products/{productId}/reviews/action1");
            level2Item.Routes.Single().Url.Should().Be("products/{productId}/reviews/{id}/action1");
        }

        [Fact]
        public void route_urls_for_routes_with_empty_path_should_match_parent_resource_url()
        {
            var convention = new TestRouteConvention("Route1", "Action1", "GET", "");

            var mapper = new RouteMapper();
            mapper.RouteConventions(convention);
            mapper.Singular("Profile", profile => profile.HandledBy<TestController>());
            mapper.Collection("Products", products =>
            {
                products.HandledBy<TestController>();
                products.Items(product => product.HandledBy<TestController>());
            });

            var model = mapper.Build();

            var singular = model.Resources.Single(x => x.Level == ResourceLevel.Singular);
            var collection = model.Resources.Single(x => x.Level == ResourceLevel.Collection);
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