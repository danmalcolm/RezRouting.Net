using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews.Review
{
    public class ApproveReviewRequest
    {
        public int Id { get; set; }

        public ReviewApprovalStatus ApprovalStatus { get; set; }

        public string Comments { get; set; }
    }
}