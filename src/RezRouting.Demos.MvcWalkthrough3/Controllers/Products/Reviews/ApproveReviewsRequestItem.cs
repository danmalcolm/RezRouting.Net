using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Reviews
{
    public class ApproveReviewsRequestItem
    {
        public int Id { get; set; }

        public ReviewApprovalStatus Status { get; set; }

        public string Comments { get; set; }
    }
}