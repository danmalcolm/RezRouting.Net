﻿using System.Web.Mvc;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public static class TestResourceModel
    {
        public static ISingularConfigurator Configure()
        {
            var root = RootResourceBuilder.Create("");
            var options = new ResourceOptions();
            options.AddRouteConventions(new CrudRouteConventions());
            root.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            root.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            return root;
        }
    }


    public interface ICollectionTaskController {}
    public interface ICollectionItemTaskController {}
    public interface ISingularTaskController {}

    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }

        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create(object input)
        {
            return null;
        }
    }

    public class NewProductController : Controller
    {
        public ActionResult Edit()
        {
            return null; 
        }

        public ActionResult Handle()
        {
            return null; 
        }
    }

    public class ProductController : Controller
    {
        public ActionResult Show(string id)
        {
            return null;
        }

        public ActionResult Edit(string id)
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete(string id)
        {
            return null;
        }
    }

    public class ProfileController : Controller
    {
        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create(object input)
        {
            return null;
        }

        public ActionResult Show()
        {
            return null;
        }

        public ActionResult Edit()
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete()
        {
            return null;
        }
    }
}