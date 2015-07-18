using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.AspNetMvc.RouteConventions.Display;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.AspNetMvc;
using RezRouting.Configuration.Options;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Home;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Manufacturers;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Manufacturers.Manufacturer;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Products;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews.Review;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Reviews;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Session;

namespace RezRouting.Demos.MvcWalkthrough3
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var root = RootResourceBuilder.Create();
            root.Route("Home", "GET", "", MvcAction.For((HomeController c) => c.Show()));
            root.Singular("Session", session =>
            {
                session.HandledBy<SignInController>();
                session.HandledBy<SessionDetailsController>();
            });
            root.Collection("Products", products =>
            {
                products.HandledBy<ProductIndexController>();
                products.HandledBy<CreateProductController>();
                products.Items(product =>
                {
                    product.HandledBy<ProductDetailsController>();
                    product.HandledBy<EditProductController>();
                    product.HandledBy<PublishProductController>();
                    product.HandledBy<ArchiveProductController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.HandledBy<ReviewsController>();
                        reviews.Items(review =>
                        {
                            review.HandledBy<ApproveReviewController>();
                            review.HandledBy<ReviewDetailsController>();
                        });
                    });
                });

                products.Collection("Reviews", reviews => reviews.HandledBy<ApproveReviewsController>());
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.HandledBy<ManufacturerIndexController>();
                manufacturers.HandledBy<CreateManufacturerController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.HandledBy<ManufacturerDetailsController>();
                    manufacturer.HandledBy<EditManufacturerController>();
                });
            });
            root.Options(options => options.UrlPaths(new UrlPathSettings(wordSeparator:"-")));
            root.ApplyRouteConventions(new DisplayRouteConventions());
            root.ApplyRouteConventions(new TaskRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
