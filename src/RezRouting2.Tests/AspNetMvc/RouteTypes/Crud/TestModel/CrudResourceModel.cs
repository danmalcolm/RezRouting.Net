using RezRouting2.AspNetMvc.RouteTypes.Crud;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Product;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Products;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Profile;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel
{
    public static class CrudResourceModel
    {
        public static RouteMapper Configure()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
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