using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product
{
    public class ProductDetailsModel
    {
        public DataAccess.Product Product { get; set; }

        public List<Review> Reviews { get; set; }
    }
}