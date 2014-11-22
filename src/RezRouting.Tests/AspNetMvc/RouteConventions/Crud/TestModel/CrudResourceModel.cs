using System;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Products;
using RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel.Controllers.Profile;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Crud.TestModel
{
    public static class CrudResourceModel
    {
        public static RouteMapper Configure(Action<RouteMapper> customise = null)
        {
            var mapper = new RouteMapper();
            if (customise != null)
            {
                customise(mapper);
            }
            mapper.RouteConventions(new CrudRouteConventionBuilder().Build());
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            mapper.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            return mapper;
        }
    }
}