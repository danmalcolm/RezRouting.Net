using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Products.Product;
using RezRouting.Tests.AspNetMvc.TestModels.Crud.Controllers.Profile;

namespace RezRouting.Tests.AspNetMvc.TestModels.Crud
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