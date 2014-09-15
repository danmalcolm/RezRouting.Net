﻿using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Demos.Tasks.Controllers.Products;
using RezRouting.Demos.Tasks.Controllers.Products.Product;
using RezRouting.Demos.Tasks.Controllers.Session;
using RezRouting2;
using RezRouting2.AspNetMvc;
using RezRouting2.AspNetMvc.RouteTypes.Crud;
using RezRouting2.AspNetMvc.RouteTypes.Tasks;

namespace RezRouting.Demos.Tasks
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new {Controller = "Home", Action = "Index"});

            var mapper = new RouteMapper();
            mapper.RouteTypes(new TaskRouteScheme().RouteTypes);
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ListProductsController>();
                products.HandledBy<CreateProductController>();
                products.Items(product => product.HandledBy<ShowProductController>());
                products.Items(product => product.HandledBy<DeleteProductController>());
                products.Items(product => product.HandledBy<EditProductController>());
            });
            new MvcRouteMapper().CreateRoutes(mapper.Build(), RouteTable.Routes);

            // Use CRUD for session for now
            mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
            mapper.Singular("Session", session => session.HandledBy<SessionController>());
            new MvcRouteMapper().CreateRoutes(mapper.Build(), RouteTable.Routes);
        }
    }
}