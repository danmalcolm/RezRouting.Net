using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.Tests.TestModels.Crud.Controllers.Products;
using RezRouting.AspNetMvc.Tests.TestModels.Crud.Controllers.Products.Product;
using RezRouting.AspNetMvc.Tests.TestModels.Crud.Controllers.Profile;
using RezRouting.Configuration;

namespace RezRouting.AspNetMvc.Tests.TestModels.Crud
{
    public static class TestCrudResourceModel
    {
        public static IRootResourceBuilder Configure()
        {
            var builder = RootResourceBuilder.Create("");
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