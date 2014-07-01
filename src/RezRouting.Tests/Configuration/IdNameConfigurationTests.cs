using System;
using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Products;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    /// <summary>
    /// Tests global configuration around resource id naming
    /// </summary>
    public class IdNameConfigurationTests
    {
        private readonly RouteMapper builder;

        public IdNameConfigurationTests()
        {
            builder = new RouteMapper();
            builder.Collection(products =>
            {
                products.HandledBy<ProductsController>();
                products.Collection(reviews => reviews.HandledBy<ReviewsController>());
            });
        }

        [Fact]
        public void ShouldUseDefaultConventionWithStandardSettingsIfNotSpecified()
        {
            builder.ShouldMapRoutesWithUrls(
                "products", "products/{id}", "products/new", "products", 
                "products/{id}/edit", "products/{id}", "products/{id}",
                "products/{productId}/reviews", "products/{productId}/reviews/{id}",
                "products/{productId}/reviews/new", "products/{productId}/reviews",
                "products/{productId}/reviews/{id}/edit", "products/{productId}/reviews/{id}",
                "products/{productId}/reviews/{id}");
        }

        [Fact]
        public void ShouldUseConfiguredConventionForIdNamesIfSpecified()
        {
            builder.Configure(config => config.CustomiseIdNames(new DefaultIdNameConvention("Code", true)));
            builder.ShouldMapRoutesWithUrls(
                "products", "products/{productCode}", "products/new", "products",
                "products/{productCode}/edit", "products/{productCode}", "products/{productCode}",
                "products/{productCode}/reviews", "products/{productCode}/reviews/{reviewCode}",
                "products/{productCode}/reviews/new", "products/{productCode}/reviews",
                "products/{productCode}/reviews/{reviewCode}/edit", "products/{productCode}/reviews/{reviewCode}",
                "products/{productCode}/reviews/{reviewCode}");
        }


    }
}