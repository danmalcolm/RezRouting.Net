using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteTypes.Crud;
using RezRouting.Demos.Crud.Controllers.Products;
using RezRouting.Demos.Crud.Controllers.Products.Product;
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

            var mapper = new RouteMapper();
            var routeTypeBuilder = new CrudRouteTypeBuilder();
            // TODO - configure some common options
            mapper.RouteTypes(routeTypeBuilder.Build());
            mapper.Singular("Session", session => session.HandledBy<SessionController>());
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            new MvcRouteMapper().CreateRoutes(mapper.Build(), RouteTable.Routes);
        }
    }
}
