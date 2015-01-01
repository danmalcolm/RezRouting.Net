using System;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Products;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Profile;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel
{
    public static class TestCrudResourceModel
    {
        public static ResourceGraphBuilder Configure(Action<ResourceGraphBuilder> customise = null)
        {
            var builder = new ResourceGraphBuilder();
            if (customise != null)
            {
                customise(builder);
            }
            var crudConventions = new CrudRouteConventions();
            builder.ApplyRouteConventions(crudConventions);
            builder.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            builder.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            return builder;
        }
    }
}