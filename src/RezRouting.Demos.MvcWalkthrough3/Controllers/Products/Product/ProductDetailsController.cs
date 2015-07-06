using System;
using System.Linq;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    public class ProductDetailsController : EntityDetailsController<DataAccess.Product, ProductDetailsModel>
    {
        protected override ProductDetailsModel CreateModel(DataAccess.Product entity)
        {
            var reviews = DemoData.Reviews.Where(x => x.Product == entity)
                .OrderByDescending(x => x.ReviewDate)
                .Take(3)
                .ToList();
            var model = new ProductDetailsModel
            {
                Product = entity,
                Reviews = reviews
            };
            return model;
        }
    }
}