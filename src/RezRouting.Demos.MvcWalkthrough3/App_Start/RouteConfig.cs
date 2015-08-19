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
                session.Controller<SignInController>();
                session.Controller<SessionDetailsController>();
            });
            root.Collection("Products", products =>
            {
                products.Controller<ProductIndexController>();
                products.Controller<CreateProductController>();
                products.Items(product =>
                {
                    product.Controller<ProductDetailsController>();
                    product.Controller<EditProductController>();
                    product.Controller<PublishProductController>();
                    product.Controller<ArchiveProductController>();
                    product.Collection("Reviews", reviews =>
                    {
                        reviews.Controller<ReviewsController>();
                        reviews.Items(review =>
                        {
                            review.Controller<ApproveReviewController>();
                            review.Controller<ReviewDetailsController>();
                        });
                    });
                });

                products.Collection("Reviews", reviews => reviews.Controller<ApproveReviewsController>());
            });
            root.Collection("Manufacturers", manufacturers =>
            {
                manufacturers.Controller<ManufacturerIndexController>();
                manufacturers.Controller<CreateManufacturerController>();
                manufacturers.Items(manufacturer =>
                {
                    manufacturer.Controller<ManufacturerDetailsController>();
                    manufacturer.Controller<EditManufacturerController>();
                });
            });
            root.Options(options => options.UrlPaths(new UrlPathSettings(wordSeparator:"-")));
            root.ApplyRouteConventions(new DisplayRouteConventions());
            root.ApplyRouteConventions(new TaskRouteConventions());
            root.MapMvcRoutes(routes);
        }
    }
}
