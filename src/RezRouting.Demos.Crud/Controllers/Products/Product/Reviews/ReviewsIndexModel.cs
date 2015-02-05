using System.Collections.Generic;

namespace RezRouting.Demos.Crud.Controllers.Products.Product.Reviews
{
    public class ReviewsIndexModel
    {
        public DataAccess.Product Product { get; set; }
        public List<DataAccess.Review> Reviews { get; set; }
    }
}