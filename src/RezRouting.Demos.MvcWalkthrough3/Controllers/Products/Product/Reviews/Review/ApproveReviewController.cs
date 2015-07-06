using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.Utility;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product.Reviews.Review
{
    public class ApproveReviewController : ItemTaskController<ApproveReviewRequest, DataAccess.Review>
    {
        protected override void PrepareRequest(ApproveReviewRequest request, DataAccess.Review review)
        {
            request.Id = review.Id;
            request.ApprovalStatus = review.ApprovalStatus;
        }

        protected override TaskModel<ApproveReviewRequest> CreateModel(ApproveReviewRequest request, DataAccess.Review review)
        {
            return new ApproveReviewModel
            {
                Request = request,
                Review = review
            };
        }

        protected override ActionResult ExecuteTask(ApproveReviewRequest request, DataAccess.Review review)
        {
            review.ApprovalStatus = request.ApprovalStatus;
            review.ApprovalComments = request.Comments;

            TempData["alert-success"] = "Review " + request.ApprovalStatus.ToString();
            return Redirect(Url.ResourceUrl((ReviewDetailsController c) => c.Show(review.Id)));
        }
    }
}