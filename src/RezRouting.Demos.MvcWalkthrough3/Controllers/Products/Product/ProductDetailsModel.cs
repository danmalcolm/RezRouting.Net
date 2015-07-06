using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    public class ProductDetailsModel
    {
        public DataAccess.Product Product { get; set; }

        public List<Review> Reviews { get; set; }
    }
}