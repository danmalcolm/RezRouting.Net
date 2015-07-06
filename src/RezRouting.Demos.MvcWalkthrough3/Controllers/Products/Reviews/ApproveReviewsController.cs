using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Reviews
{
    public class ApproveReviewsController : TaskController<ApproveReviewsRequest>
    {
        protected override void PrepareRequest(ApproveReviewsRequest request)
        {
            var reviews = DemoData.Reviews
                .Where(r => r.ApprovalStatus == ReviewApprovalStatus.Pending)
                .OrderByDescending(r => r.ModifiedOn)
                .Take(20);
            var requestItems = reviews
                .Select(r => new ApproveReviewsRequestItem { Id = r.Id, Status = ReviewApprovalStatus.Pending })
                .ToList();
            request.Items = requestItems; 
        }

        protected override TaskModel<ApproveReviewsRequest> CreateModel(ApproveReviewsRequest request)
        {
            // The request contains the editable form values
            // We added supporting information (details of reviews) to the Model
            // so that view can display full details of reviews being approved / rejected
            var reviewIds = request.Items.Select(i => i.Id).ToList();
            var reviews = DemoData.Reviews
                .Where(r => reviewIds.Contains(r.Id))
                .ToList();
            return new ApproveReviewsModel
            {
                Request = request,
                Reviews = reviews
            };
        }

        protected override ActionResult ExecuteTask(ApproveReviewsRequest request)
        {
            int approvedCount = 0;
            int rejectedCount = 0;
            foreach (var item in request.Items.Where(i => i.Status != ReviewApprovalStatus.Pending))
            {
                var review = DemoData.Get<Review>(item.Id);
                review.ApprovalStatus = item.Status;
                review.ApprovalComments = item.Comments;
                if (item.Status == ReviewApprovalStatus.Approved)
                    approvedCount++;
                else
                    rejectedCount++;
            }
            TempData["alert-success"] = string.Format("{0} reviews accepted, {1} reviews rejected", approvedCount, rejectedCount);
            return RedirectToAction("Edit");
        }
    }
}