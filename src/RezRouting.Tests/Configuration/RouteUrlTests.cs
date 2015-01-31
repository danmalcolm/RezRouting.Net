using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class RouteUrlTests
    {
        [Fact]
        public void route_urls_should_include_parent_resource_url()
        {
            var rootBuilder = RootResourceBuilder.Create("");
            rootBuilder.Singular("Profile", profile =>
            {
                profile.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                profile.Singular("User", user =>
                {
                    user.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                });
            });
            rootBuilder.Collection("Products", products =>
            {
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                products.Items(product =>
                {
                    product.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");
                        reviews.Items(review =>
                        {
                            review.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "action1");                        
                        });
                    });
                });
            });

            var root = rootBuilder.Build(new ResourceOptions());

            var level1Singular = root.Children.Single(x => x.Type == ResourceType.Singular);
            var level2Singular = level1Singular.Children.Single();
            var level1Collection = root.Children.Single(x => x.Type == ResourceType.Collection);
            var level1Item = level1Collection.Children.Single(x => x.Type == ResourceType.CollectionItem);
            var level2Collection = level1Item.Children.Single();
            var level2Item = level2Collection.Children.Single(x => x.Type == ResourceType.CollectionItem);

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
            var rootBuilder = RootResourceBuilder.Create("");
            rootBuilder.Singular("Profile", profile =>
            {
                profile.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "");
            });
            rootBuilder.Collection("Products", products =>
            {
                products.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "");
                products.Items(product =>
                {
                    product.Route("Route1", MvcAction.For((TestController c) => c.Action1()), "GET", "");
                });
            });

            var root = rootBuilder.Build(new ResourceOptions());

            var singular = root.Children.Single(x => x.Type == ResourceType.Singular);
            var collection = root.Children.Single(x => x.Type == ResourceType.Collection);
            var collectionItem = collection.Children.Single(x => x.Type == ResourceType.CollectionItem);

            singular.Routes.Single().Url.Should().Be(singular.Url);
            collection.Routes.Single().Url.Should().Be(collection.Url);
            collectionItem.Routes.Single().Url.Should().Be(collectionItem.Url);
        }

        public class TestController : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }
        }
    }
}