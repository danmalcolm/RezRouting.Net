using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
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
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var root = RootResourceBuilder.Create();
            root.Route("Home", "GET", "", MvcAction.For((HomeController c) => c.Show()));
            root.Singular("Session", session => session.HandledBy<SessionController>());
            root.Collection("Products", products =>
            {
                products.HandledBy<ProductIndexController>();
                products.HandledBy<CreateProductController>();
                products.Items(product =>
                {
                    product.HandledBy<ProductDetailsController>();
                    product.HandledBy<EditProductController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<ReviewsController>();
                        reviews.Items(review => review.HandledBy<ReviewDetailsController>());
                    });
                });
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.HandledBy<ManufacturerIndexController>();
                manufacturers.HandledBy<CreateManufacturerController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.HandledBy<ManufacturerDetailsController>();
                    manufacturer.HandledBy<ManufacturerEditController>();
                });
            });
            root.ApplyRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
