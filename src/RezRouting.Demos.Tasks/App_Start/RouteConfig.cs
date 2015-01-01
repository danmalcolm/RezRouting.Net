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

            var builder = new ResourceGraphBuilder();
            var taskConventions = new TaskRouteConventions();
            builder.ApplyRouteConventions(taskConventions);
            builder.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.Items(product =>
                {
                    product.HandledBy<ShowProductController>();
                    product.HandledBy<DeleteProductController>();
                    product.HandledBy<EditProductController>();
                });
            });
            builder.MapMvcRoutes(routes);

            // Use CRUD for session for now
            builder = new ResourceGraphBuilder();
            var crudConventions = new CrudRouteConventions();
            builder.ApplyRouteConventions(crudConventions);
            builder.Singular("Session", session => session.HandledBy<SessionController>());
            builder.MapMvcRoutes(routes);
        }
    }
}
