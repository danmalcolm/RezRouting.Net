using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.AspNetMvc;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Home;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers.Manufacturer;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Products;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product.Reviews;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product.Reviews.Review;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Session;

namespace RezRouting.Demos.MvcWalkthrough2
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var root = RootResourceBuilder.Create();
            root.Route("Home", "GET", "", MvcAction.For((HomeController c) => c.Show()));
            root.Singular("Session", session => session.Controller<SessionController>());
            root.Collection("Products", products =>
            {
                products.Controller<ProductIndexController>();
                products.Controller<CreateProductController>();
                products.Items(product =>
                {
                    product.Controller<ProductDetailsController>();
                    product.Controller<EditProductController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Controller<ReviewsController>();
                        reviews.Items(review => review.Controller<ReviewDetailsController>());
                    });
                });
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.Controller<ManufacturerIndexController>();
                manufacturers.Controller<CreateManufacturerController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.Controller<ManufacturerDetailsController>();
                    manufacturer.Controller<EditManufacturerController>();
                });
            });
            root.ApplyRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
