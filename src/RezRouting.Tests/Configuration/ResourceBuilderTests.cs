using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceBuilderTests
    {
        [Fact]
        public void should_create_resources_configured_via_builders()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => {});
            builder.Singular("Profile", profile => {});
            var model = builder.Build();

            model.Resources.Should().HaveCount(2);
            model.Resources.Should().Contain(x => x.Name == "Products" && x.Level == ResourceLevel.Collection);
            model.Resources.Should().Contain(x => x.Name == "Profile" && x.Level == ResourceLevel.Singular);
        }
        
        [Fact]
        public void should_include_base_path_in_all_resource_urls()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => { });

            builder.BasePath("api");
            var model = builder.Build();

            var urls = model.AllResources().Select(x => x.Url);
            urls.Should().BeEquivalentTo("api/products", "api/products/{id}");
        }

        [Fact]
        public void should_include_base_name_in_full_resource_names()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products => { });

            builder.BaseName("Api");
            var model = builder.Build();

            var fullNames = model.AllResources().Select(x => x.FullName);
            fullNames.Should().BeEquivalentTo("Api.Products", "Api.Products.Product");
        }

        [Fact]
        public void should_apply_each_convention_for_each_resource_and_configured_handlers()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.HandledBy<TestController2>();
                products.Items(product =>
                {
                    product.HandledBy<TestController1>();
                    product.HandledBy<TestController2>();
                });
            });
            var actualAttempts = new List<Tuple<TestRouteConvention, Resource, string>>();
            var convention1 = new TestRouteConvention(actualAttempts);
            var convention2 = new TestRouteConvention(actualAttempts);
            var conventions = new TestRouteConventionScheme(convention1, convention2);
            builder.IncludeRouteConventions(conventions);

            var model = builder.Build();

            var collection = model.Resources.Single();
            var collectionItem = collection.Children.Single();
            var expectedAttempts = new List<Tuple<TestRouteConvention, Resource, string>>()
            {
                Tuple.Create(convention1, collectionItem, "TestController1,TestController2"),
                Tuple.Create(convention2, collectionItem, "TestController1,TestController2"),
                Tuple.Create(convention1, collection, "TestController1,TestController2"),
                Tuple.Create(convention2, collection, "TestController1,TestController2")
            };
            actualAttempts
                .Where(x => x.Item2 == collection || x.Item2 == collectionItem)
                .ShouldAllBeEquivalentTo(expectedAttempts);
        }

        public class TestRouteConvention : IRouteConvention
        {
            private readonly List<Tuple<TestRouteConvention, Resource, string>> actualAttempts;

            public TestRouteConvention(List<Tuple<TestRouteConvention, Resource, string>> actualAttempts)
            {
                this.actualAttempts = actualAttempts;
            }

            public virtual IEnumerable<Route> Create(Resource resource, IEnumerable<IResourceHandler> handlers, UrlPathFormatter pathFormatter)
            {
                var controllerTypes = handlers.Cast<MvcController>().Select(x => x.ControllerType);
                string typeNames = string.Join(",", controllerTypes.Select(x => x.Name));
                actualAttempts.Add(Tuple.Create(this, resource, typeNames));
                yield break;
            }
        }


        [Fact]
        public void should_create_routes_specified_by_each_convention()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Items(product => product.HandledBy<TestController2>());
            });

            var convention1 = new Infrastructure.TestRouteConvention("Route1", "Action1", "GET", "action1",
                (r,t) => r.Name == "Products");
            var convention2 = new Infrastructure.TestRouteConvention("Route2", "Action2", "GET", "action2",
                (r,t) => r.Name == "Product");
            var conventions = new TestRouteConventionScheme(convention1, convention2);
            builder.IncludeRouteConventions(conventions);
            var model = builder.Build();

            var resource1 = model.Resources.Single();
            var resource2 = resource1.Children.Single();
            resource1.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route1"});
            resource2.Routes.Select(x => x.Name).ShouldBeEquivalentTo(new [] { "Route2" });
        }

        [Fact]
        public void should_include_routes_specified_on_resource_before_routes_created_by_conventions()
        {
            var builder = new ResourcesBuilder();
            builder.Collection("Products", products =>
            {
                products.HandledBy<TestController1>();
                products.Route("Route2", new MvcAction(typeof(TestController2), "Action2"), "GET", "action2");
            });

            var convention = new Infrastructure.TestRouteConvention("Route1", "Action1", "GET", "action1");
            var conventions = new TestRouteConventionScheme(convention);
            builder.IncludeRouteConventions(conventions);
            var model = builder.Build();

            var resource1 = model.Resources.Single();
            resource1.Routes.Select(x => x.Name).Should().Equal("Route2", "Route1");
        }
        
        public class TestController1 : Controller
        {
            
        }

        public class TestController2 : Controller
        {

        }
    }
}