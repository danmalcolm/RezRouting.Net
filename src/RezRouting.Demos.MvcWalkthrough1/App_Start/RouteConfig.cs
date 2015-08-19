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
            root.Singular("Session", session => session.Controller<SessionController>());
            root.Collection("Products", products =>
            {
                products.Controller<ProductsController>();
                products.Items(product =>
                {
                    product.Controller<ProductsController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Controller<ReviewsController>();
                        reviews.Items(review => review.Controller<ReviewsController>());
                    });
                });
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.Controller<ManufacturersController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.Controller<ManufacturersController>();
                });
            });
            root.ApplyRouteConventions(new CrudRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
