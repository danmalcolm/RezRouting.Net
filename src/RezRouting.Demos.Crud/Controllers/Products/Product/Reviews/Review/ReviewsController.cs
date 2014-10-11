﻿using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products.Product.Reviews.Review
{
    public class ReviewController : Controller
    {
        public ActionResult Show(int productId, int id)
        {
            var review = DemoData.Reviews
                .SingleOrDefault(x => x.Product.Id == productId && x.Id == id);
            if (review == null)
            {
                return HttpNotFound();
            }
            
            var model = new ReviewDetailsModel
            {
                Review = review
            };
            return View(model);
        } 
    }
}