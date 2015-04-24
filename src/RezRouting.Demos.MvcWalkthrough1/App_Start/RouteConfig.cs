using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.AspNetMvc;
using RezRouting.Demos.MvcWalkthrough1.Controllers.Home;
using RezRouting.Demos.MvcWalkthrough1.Controllers.Manufacturers;
using RezRouting.Demos.MvcWalkthrough1.Controllers.Products;
using RezRouting.Demos.MvcWalkthrough1.Controllers.Reviews;
using RezRouting.Demos.MvcWalkthrough1.Controllers.Session;

namespace RezRouting.Demos.MvcWalkthrough1
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
                    product.HandledBy<ProductsController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<ReviewsController>();
                        reviews.Items(review => review.HandledBy<ReviewsController>());
                    });
                });
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.HandledBy<ManufacturersController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.HandledBy<ManufacturersController>();
                });
            });
            root.ApplyRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
