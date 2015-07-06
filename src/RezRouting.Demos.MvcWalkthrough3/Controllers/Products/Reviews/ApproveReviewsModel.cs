using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Reviews
{
    public class ApproveReviewsModel : TaskModel<ApproveReviewsRequest>
    {
        public List<Review> Reviews { get; set; } 
    }
}