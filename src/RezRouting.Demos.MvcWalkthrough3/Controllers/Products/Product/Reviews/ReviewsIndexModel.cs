using System.Collections.Generic;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews
{
    public class ReviewsIndexModel
    {
        public DataAccess.Product Product { get; set; }
        public List<DataAccess.Review> Reviews { get; set; }
    }
}