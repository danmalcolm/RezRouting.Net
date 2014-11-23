using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Demos.Crud.Controllers.Products;
using RezRouting.Demos.Crud.Controllers.Products.Product;
using RezRouting.Demos.Crud.Controllers.Products.Product.Reviews;
using RezRouting.Demos.Crud.Controllers.Products.Product.Reviews.Review;
using RezRouting.Demos.Crud.Controllers.Session;
using RezRouting.AspNetMvc;

namespace RezRouting.Demos.Crud
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new {Controller = "Home", Action = "Index"});

            var builder = new ResourcesBuilder();
            var conventions = new CrudRouteConventionBuilder();
            builder.RouteConventions(conventions.Build());
            builder.Singular("Session", session => session.HandledBy<SessionController>());
            builder.Collection("Products", products =>
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
            builder.MapMvcRoutes(routes);

            // Allow RezRouting to generate URLs (much) more quickly
            UrlHelperExtensions.IndexRoutes(routes);
        }
    }
}
