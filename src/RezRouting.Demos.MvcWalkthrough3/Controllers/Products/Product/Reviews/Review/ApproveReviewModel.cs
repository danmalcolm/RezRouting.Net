using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews.Review
{
    /// <summary>
    /// Model used to display screen to approve a review
    /// </summary>
    public class ApproveReviewModel : TaskModel<ApproveReviewRequest>
    {
        public DataAccess.Review Review { get; set; }
    }
}