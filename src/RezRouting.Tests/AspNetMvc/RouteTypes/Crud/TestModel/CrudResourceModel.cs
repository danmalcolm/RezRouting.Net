using System;
using RezRouting.AspNetMvc.RouteTypes.Crud;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Product;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Products;
using RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Profile;

namespace RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel
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
            mapper.RouteTypes(new CrudRouteTypeBuilder().Build());
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