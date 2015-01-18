using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
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

            var root = new ResourceGraphBuilder("");
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
            var options = new ResourceOptions();
            options.AddRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(options, routes);

            // Allow RezRouting to generate URLs (much) more quickly
            UrlHelperExtensions.IndexRoutes(routes);
        }
    }
}
