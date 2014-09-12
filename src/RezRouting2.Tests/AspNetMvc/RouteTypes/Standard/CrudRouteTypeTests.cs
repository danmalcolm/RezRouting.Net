using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting2.AspNetMvc.RouteTypes.Standard;
using RezRouting2.Tests.Utility;
using Xunit;
using Xunit.Extensions;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Standard
{
    public class CrudRouteTypeTests
    {
        private static readonly IList<Route> routes;

        static CrudRouteTypeTests()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            mapper.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            var resources = mapper.Build();
            routes = resources.Expand().SelectMany(x => x.Routes).ToList();
        }

        [Theory]
        [InlineData("Products.Index", "GET", "products", typeof(ProductsController), "Index")]
        [InlineData("Products.New", "GET", "products/new", typeof(ProductsController), "New")]
        [InlineData("Products.Create", "POST", "products", typeof(ProductsController), "Create")]
        [InlineData("Products.Product.Show", "GET", "products/{id}", typeof(ProductController), "Show")]
        [InlineData("Products.Product.Edit", "GET", "products/{id}/edit", typeof(ProductController), "Edit")]
        [InlineData("Products.Product.Update", "PUT", "products/{id}", typeof(ProductController), "Update")]
        [InlineData("Products.Product.Delete", "DELETE", "products/{id}", typeof(ProductController), "Delete")]
        [InlineData("Profile.New", "GET", "profile/new", typeof(ProfileController), "New")]
        [InlineData("Profile.Create", "POST", "profile", typeof(ProfileController), "Create")]
        [InlineData("Profile.Show", "GET", "profile", typeof(ProfileController), "Show")]
        [InlineData("Profile.Edit", "GET", "profile/edit", typeof(ProfileController), "Edit")]
        [InlineData("Profile.Update", "PUT", "profile", typeof(ProfileController), "Update")]
        [InlineData("Profile.Delete", "DELETE", "profile", typeof(ProfileController), "Delete")]
        public void should_map_route_for_route_type(string fullName, string httpMethod, 
            string url, Type controllerType, string action)
        {
            routes.Should().ContainSingle(x => x.FullName == fullName);
            var route = routes.Single(x => x.FullName == fullName);
            route.HttpMethod.Should().Be(httpMethod);
            route.Url.Should().Be(url);
            route.Action.Should().Be(action);
            route.ControllerType.Should().Be(controllerType);
        }
        
        [Fact]
        public void should_not_map_resource_level_routes_on_different_level_resources()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
            mapper.Collection("Products", products => products.Items(product => product.HandledBy<ProductsController>()));
            var routes = mapper.Build().Expand().SelectMany(x => x.Routes);
            routes.Should().BeEmpty();
        }

        [Theory]
        [InlineData]
        public void should_match_requests_to_routes()
        {
            
        }
    }

    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }

        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create(object input)
        {
            return null;
        }
    }

    public class ProductController : Controller
    {
        public ActionResult Show(string id)
        {
            return Content("");
        }

        public ActionResult Edit(string id)
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete(string id)
        {
            return null;
        }
    }

    public class ProfileController : Controller
    {
        public ActionResult New()
        {
            return null;
        }
        
        public ActionResult Create(object input)
        {
            return null;
        }

        public ActionResult Show()
        {
            return null;
        }

        public ActionResult Edit()
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete()
        {
            return null;
        }
    }
}