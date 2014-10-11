using System.Collections.Generic;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products.Product.Reviews
{
    public class ReviewsIndexModel
    {
        public DataAccess.Product Product { get; set; }
        public List<DataAccess.Review> Reviews { get; set; }
    }
}