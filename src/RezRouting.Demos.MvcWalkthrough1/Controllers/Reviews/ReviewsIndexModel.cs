using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Reviews
{
    public class ReviewsIndexModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
    }
}