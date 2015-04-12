using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Demos.Crud.Controllers.Home;
using RezRouting.Demos.Crud.Controllers.Session;
using RezRouting.Demos.Crud.Controllers.Products;
using RezRouting.Demos.Crud.Controllers.Products.Product;
using RezRouting.Demos.Crud.Controllers.Products.Product.Reviews;
using RezRouting.Demos.Crud.Controllers.Products.Product.Reviews.Review;
using RezRouting.Demos.Crud.Controllers.Manufacturers;
using RezRouting.Demos.Crud.Controllers.Manufacturers.Manufacturer;
using RezRouting.AspNetMvc;

namespace RezRouting.Demos.Crud
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
                products.HandledBy<ProductsController>();
                products.Items(product =>
                {
                    product.HandledBy<ProductController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<ReviewsController>();
                        reviews.Items(review => review.HandledBy<ReviewController>());
                    });
                });
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.HandledBy<ManufacturersController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.HandledBy<ManufacturerController>();
                });
            });
            root.ApplyRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
