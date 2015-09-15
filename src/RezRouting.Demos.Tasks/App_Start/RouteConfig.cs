using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Demos.Tasks.Controllers.Products;
using RezRouting.Demos.Tasks.Controllers.Products.Product;
using RezRouting.Demos.Tasks.Controllers.Session;
using RezRouting.AspNetMvc;

namespace RezRouting.Demos.Tasks
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new {Controller = "Home", Action = "Index"});

            var root = RootResourceBuilder.Create("");
            root.Collection("Products", products =>
            {
                products.Controller<ListProductsController>();
                products.Controller<CreateProductController>();
                products.Items(product =>
                {
                    product.Controller<ShowProductController>();
                    product.Controller<DeleteProductController>();
                    product.Controller<EditProductController>();
                });
            });
            root.Extension(new TaskRouteConventions());
            root.MapMvcRoutes(routes);

            // Use CRUD for session for now
            root = RootResourceBuilder.Create("");
            root.Extension(new CrudRoutesScheme());
            root.Singular("Session", session => session.Controller<SessionController>());
            root.MapMvcRoutes(routes);
        }
    }
}
