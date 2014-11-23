using System;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Products;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Profile;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel
{
    public static class TestCrudResourceModel
    {
        public static ResourcesBuilder Configure(Action<ResourcesBuilder> customise = null)
        {
            var builder = new ResourcesBuilder();
            if (customise != null)
            {
                customise(builder);
            }
            builder.RouteConventions(new CrudRouteConventionBuilder().Build());
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