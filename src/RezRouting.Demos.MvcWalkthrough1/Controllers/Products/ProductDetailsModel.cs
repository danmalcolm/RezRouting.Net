using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Products
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }

        public List<Review> Reviews { get; set; }
    }
}