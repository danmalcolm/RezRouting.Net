﻿using System.Web.Mvc;

namespace RezRouting.AspNetMvc.Tests.TestModels.Crud.Controllers.Products
{
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
}